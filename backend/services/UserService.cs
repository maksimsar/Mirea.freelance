using Mirea.freelance.backend.models;
using Mirea.freelance.backend.repositories;
using Mirea.freelance.backend.data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Mirea.freelance.backend.dto;

namespace Mirea.freelance.backend.services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
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
        await _userRepository.AddAsync(newUser);

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
        existingUser.Login = dto.NewLogin;
        existingUser.PasswordHash = dto.NewPassword;

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

    public async Task<(bool success, string message, UserResponseDto? user)> AuthenticateAsync(string login, string password)
{
    var user = await _userRepository.GetByLoginAsync(login);
    if (user == null)
    {
        return (false, "Пользователь не найден", null);
    }

    // Простая проверка пароля (в реале используй хеширование, например, BCrypt)
    if (user.PasswordHash != password) // Замени на реальную проверку хеша
    {
        return (false, "Неверный пароль", null);
    }

    var response = new UserResponseDto
    {
        Id = user.Id,
        Login = user.Login,
        RegistrationDate = user.RegistrationDate
    };

    return (true, "Авторизация успешна", response);
}
}