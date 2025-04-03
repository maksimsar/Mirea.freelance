namespace Mirea.freelance.backend.dto;

public class UserDto
{
    public int Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; }
}