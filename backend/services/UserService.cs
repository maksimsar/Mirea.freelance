using Microsoft.EntityFrameworkCore;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    // Метод для создания пользователя
    public async Task<(bool Success, string Message, User? CreatedUser)> CreateUserAsync(string username, string password, string email)
    {
        // Проверяем, существует ли пользователь с таким именем или email
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username || u.Email == email);

        if (existingUser != null)
        {
            return (false, "User already exists", null);
        }

        // Создание нового пользователя
        var newUser = new User
        {
            Username = username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password), // Используйте BCrypt для хэширования пароля
            Email = email
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return (true, "User created", newUser);
    }
}
