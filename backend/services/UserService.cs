using Mirea.freelance.backend.models;
using Mirea.freelance.backend.repositories;
using Mirea.freelance.backend.data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Mirea.freelance.backend.services;

public class UserService
    {
        private readonly IUserRepository _userRepository;

        // Внедрение зависимости (Dependency Injection) через конструктор
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Пример: Получить пользователя по Id
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        // Пример: Получить всех пользователей
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        // Пример: Создать пользователя
        // Возвращает (success, message, user?)
        public async Task<(bool success, string message, User? user)> CreateUserAsync(string login, string password)
        {
            // Проверим, не занят ли логин
            bool loginTaken = await _userRepository.IsLoginTakenAsync(login);
            if (loginTaken)
            {
                return (false, "Login is already taken.", null);
            }

            // Создаём сущность пользователя
            var newUser = new User
            {
                Login = login,
                // Предположим, password хранится как хеш
                PasswordHash = password,
                RegistrationDate = System.DateTime.UtcNow
            };

            // Добавим в БД
            await _userRepository.AddAsync(newUser);

            return (true, "User created successfully.", newUser);
        }

        // Пример: Обновить пользователя
        // Возвращает (success, message, user?)
        public async Task<(bool success, string message, User? user)> UpdateUserAsync(int id, string newLogin, string newPassword)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
                return (false, "User not found.", null);

            // Предположим, нужно обновить логин и пароль
            existingUser.Login = newLogin;
            existingUser.PasswordHash = newPassword;

            await _userRepository.UpdateAsync(existingUser);

            return (true, "User updated successfully.", existingUser);
        }

        // Пример: Удалить пользователя
        // Возвращает (success, message)
        public async Task<(bool success, string message)> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return (false, "User not found.");
            }

            await _userRepository.DeleteAsync(id);
            return (true, "User deleted successfully.");
        }
    }