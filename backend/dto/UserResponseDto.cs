namespace Mirea.freelance.backend.dto;

public class UserResponseDto
{
    public int Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; }
    // Сюда можно добавить любые поля, которые хотите вернуть клиенту (например, Email, Role и т.д.)
}