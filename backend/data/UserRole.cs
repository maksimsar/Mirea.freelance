using System;

public class UserRole
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }  // Убедитесь, что класс User доступен

    public int RoleId { get; set; }
    public Role Role { get; set; }

    public DateTime AssignedDate { get; set; } = DateTime.Now;
}
