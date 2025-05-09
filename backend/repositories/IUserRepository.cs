using Mirea.freelance.backend.models;

namespace Mirea.freelance.backend.repositories;

public interface IUserRepository
{
    // Получить пользователя по его идентификатору
    Task<User?> GetByIdAsync(int id);

    // Получить всех пользователей
    Task<IEnumerable<User>> GetAllAsync();

    // Добавить нового пользователя
    Task AddAsync(User user, string password);

    // Обновить данные пользователя
    Task UpdateAsync(User user);

    // Удалить пользователя по идентификатору
    Task DeleteAsync(int id);

    // Проверить, занят ли логин (возвращает true, если пользователь с таким логином уже существует)
    Task<bool> IsLoginTakenAsync(string login);

    //Логин пользователя
    Task<User?> GetByLoginAsync(string login);
}