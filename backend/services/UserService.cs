using Mirea.Freelance.backend.data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

public class UserService
{
    private readonly AppDbContext _context;

    // Конструктор класса UserService
    public UserService(AppDbContext context)
    {
        _context = context;
    }

    // Метод создания пользователя
public async Task<(bool success, string message, User user)> CreateUserAsync(string login, string password, string email)
{
    var existingUser = await _context.User
        .FirstOrDefaultAsync(u => u.Login == login);

    if (existingUser != null)
    {
        return (false, "Пользователь с таким логином уже существует", null);
    }

    var passwordHash = HashPassword(password);

    var user = new User
    {
        Login = login,
        PasswordHash = passwordHash,
        RegistrationDate = DateTime.UtcNow
    };

    _context.User.Add(user);
    await _context.SaveChangesAsync();  // Сохраняем пользователя, чтобы получить его Id

    // Автоматически создаём профиль для нового пользователя
    var profile = new Profile
    {
        userid = user.Id,  // Присваиваем пользовательский Id из базы данных
        FirstName = "Имя",  // Значение по умолчанию
        LastName = "Фамилия",  // Значение по умолчанию
        Age = 18,  // Значение по умолчанию
        Gender = "Не указано",  // Значение по умолчанию
        Rating = 0
    };

    _context.Profiles.Add(profile);
    await _context.SaveChangesAsync();

    return (true, "Пользователь успешно создан", user);
}



    //Метод поиска пользователя по id
    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _context.User.FirstOrDefaultAsync(u => u.Id == id);
    }

    // Метод хеширования пароля
    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashBytes);
        }
    }

    //Метод удаления пользователя по id
    // Метод удаления пользователя
    public async Task<(bool success, string message)> DeleteUserAsync(int userId)
    {
        var user = await _context.User.FindAsync(userId);

        if (user == null)
        {
            return (false, "Пользователь не найден");
        }

        _context.User.Remove(user);
        await _context.SaveChangesAsync();

        return (true, "Пользователь успешно удалён");
    }

}
