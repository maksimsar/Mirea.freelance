using Mirea.freelance.backend.models;
using Mirea.freelance.backend.repositories;
using Mirea.freelance.backend.data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Mirea.freelance.backend.dto;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Mirea.freelance.backend.services;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;
    private readonly AppDbContext _dbContext;
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    private readonly JwtService _jwtService;

    public UserService(IUserRepository userRepository, UserManager<User> userManager, JwtService jwtService, AppDbContext dbContext, RoleManager<IdentityRole<int>> roleManager)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _jwtService = jwtService;
        _dbContext = dbContext;
        _roleManager = roleManager;
    }

    // Получить пользователя по Id
    public async Task<UserResponseDto?> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return null;

        var roles = await _userManager.GetRolesAsync(user);
        return new UserResponseDto
        {
            Id = user.Id,
            Login = user.Login,
            RegistrationDate = user.RegistrationDate,
            Roles = roles
        };
    }

    // Получить всех пользователей
    public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        var result = new List<UserResponseDto>();
        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            result.Add(new UserResponseDto
            {
                Id = user.Id,
                Login = user.Login,
                RegistrationDate = user.RegistrationDate,
                Roles = roles
            });
        }
        return result;
    }

    // Создать пользователя (регистрация)
    public async Task<(bool success, string message, UserResponseDto? user)> CreateUserAsync(CreateUserDto dto)
    {
        // Проверим, не занят ли логин
        var existingUser = await _userManager.FindByNameAsync(dto.Login); // Изменено: Используем UserManager для проверки логина
        if (existingUser != null)
        {
            return (false, "Логин уже занят.", null);
        }

        if (!await _roleManager.RoleExistsAsync(dto.Role))
        {
            return (false, $"Роль '{dto.Role}' не существует.", null);
        }

        // Создаём сущность пользователя
        var newUser = new User
        {
            Login = dto.Login,
            UserName = dto.Login,
            //пароль хранится как хэш 
            RegistrationDate = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(newUser, dto.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return (false, $"Ошибка при создании пользователя: {errors}", null);
        }

        // Назначаем роль через UserManager
        var roleResult = await _userManager.AddToRoleAsync(newUser, dto.Role); // Изменено: Добавляем роль в IdentityUserRoles
        if (!roleResult.Succeeded)
        {
            await _userManager.DeleteAsync(newUser); // Откатываем создание пользователя
            var roleErrors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
            return (false, $"Ошибка при назначении роли: {roleErrors}", null);
        }

        // Добавляем запись в кастомную таблицу UserRoles с AssignedDate
        var role = await _roleManager.FindByNameAsync(dto.Role);
        var userRole = new UserRole
        {
            UserId = newUser.Id,
            RoleId = role.Id,
            AssignedDate = DateTime.UtcNow
        };
        _dbContext.Set<UserRole>().Add(userRole);
        await _dbContext.SaveChangesAsync();

        var roles = await _userManager.GetRolesAsync(newUser);
        var userResponse = new UserResponseDto
        {
            Id = newUser.Id,
            Login = newUser.Login,
            RegistrationDate = newUser.RegistrationDate,
            Roles = roles
        };
            
        return (true, "Пользователь успешно создан.", userResponse);
    }

    // Обновить пользователя
    public async Task<(bool success, string message, UserResponseDto? user)> UpdateUserAsync(int id, UpdateUserDto dto)
    {
        var existingUser = await _userRepository.GetByIdAsync(id);
        if (existingUser == null)
            return (false, "Пользователь не найден.", null);

        // Предположим, нужно обновить логин и пароль
        // Обновляем логин
        if (!string.IsNullOrEmpty(dto.NewLogin) && existingUser.Login != dto.NewLogin)
        {
            if (await _userRepository.IsLoginTakenAsync(dto.NewLogin))
            {
                return (false, "Новый логин уже занят.", null);
            }
            existingUser.UserName = dto.NewLogin;
            existingUser.Login = dto.NewLogin;
        }

        // Обновляем пароль, если указан
        if (!string.IsNullOrEmpty(dto.NewPassword))
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(existingUser);
            var result = await _userManager.ResetPasswordAsync(existingUser, token, dto.NewPassword);
            if (!result.Succeeded)
            {
                return (false, $"Ошибка при обновлении пароля: {string.Join(", ", result.Errors.Select(e => e.Description))}", null);
            }
        }

        await _userRepository.UpdateAsync(existingUser);

        var roles = await _userManager.GetRolesAsync(existingUser);

        var updatedUser = new UserResponseDto
        {
            Id = existingUser.Id,
            Login = existingUser.Login,
            RegistrationDate = existingUser.RegistrationDate,
            Roles = roles
        };

        return (true, "Пользователь обновлен.", updatedUser);
    }

    // Удалить пользователя
    public async Task<(bool success, string message)> DeleteUserAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return (false, "Пользователь не найден.");
        }

        await _userRepository.DeleteAsync(id);
        return (true, "Пользователь удален успешно.");
    }

    //аутентификация
    public async Task<(bool success, string message, UserResponseDto? user, string? token)> AuthenticateAsync(string login, string password)
    {
        var user = await _userManager.FindByNameAsync(login);
        if (user == null || !await _userManager.CheckPasswordAsync(user, password))
        {
            return (false, "Неверный логин или пароль.", null, null);
        }

        var roles = await _userManager.GetRolesAsync(user);


        var userResponse = new UserResponseDto
        {
            Id = user.Id,
            Login = user.Login,
            RegistrationDate = user.RegistrationDate,
            Roles = roles
        };

        var token = await _jwtService.GenerateJwtToken(user);
        return (true, "Аутентификация успешна.", userResponse, token);
    }
}