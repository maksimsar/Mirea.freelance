namespace Mirea.freelance.backend.dto;

public class UpdateUserDto
{
    // Поля, которые разрешено обновлять
    public string NewLogin { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}