using Mirea.freelance.backend.models;
using Mirea.freelance.backend.repositories;
using Mirea.freelance.backend.data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Mirea.freelance.backend.dto;
using Microsoft.AspNetCore.Identity;

namespace Mirea.freelance.backend.services;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;
    private readonly JwtService _jwtService;

    public UserService(IUserRepository userRepository, UserManager<User> userManager, JwtService jwtService)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _jwtService = jwtService;
    }

    // Получить пользователя по Id
    public async Task<UserResponseDto?> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return null;

        return new UserResponseDto
        {
            Id = user.Id,
            Login = user.Login,
            RegistrationDate = user.RegistrationDate
        };
    }

    // Получить всех пользователей
    public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        // Преобразуем User в UserResponseDto
        var result = users.Select(u => new UserResponseDto
        {
            Id = u.Id,
            Login = u.Login,
            RegistrationDate = u.RegistrationDate
        });
        return result;
    }

    // Создать пользователя (регистрация)
    public async Task<(bool success, string message, UserResponseDto? user)> CreateUserAsync(CreateUserDto dto)
    {
        // Проверим, не занят ли логин
        bool loginTaken = await _userRepository.IsLoginTakenAsync(dto.Login);
        if (loginTaken)
        {
            return (false, "Логин уже занят.", null);
        }

        // Создаём сущность пользователя
        var newUser = new User
        {
            Login = dto.Login,
            // Допустим, password хранится как хеш
            PasswordHash = dto.Password, 
            RegistrationDate = DateTime.UtcNow
        };

        // Добавим в БД
        try {
            await _userRepository.AddAsync(newUser, dto.Password);
        }
        catch (Exception ex) {
            return(false, $"Ошибка при создании пользователя: {ex.Message}", null);
        }
        
        // Возвращаем UserResponseDto
        var userResponse = new UserResponseDto
        {
            Id = newUser.Id,
            Login = newUser.Login,
            RegistrationDate = newUser.RegistrationDate
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

        var updatedUser = new UserResponseDto
        {
            Id = existingUser.Id,
            Login = existingUser.Login,
            RegistrationDate = existingUser.RegistrationDate
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
        var user = await _userRepository.GetByLoginAsync(login);
        if (user == null){
            return (false, "Пользователь не найден", null, null);
        }  

        var result = await _userManager.CheckPasswordAsync(user, password);
        if (result){
            var token = _jwtService.GenerateJwtToken(user);
            var response = new UserResponseDto
            {
                Id = user.Id,
                Login = user.Login,
                RegistrationDate = user.RegistrationDate
            };
            return (true, "Авторизация успешна", response, token);
        }

        return (false, "Неверный пароль", null, null);
    }
}