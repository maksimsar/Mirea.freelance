namespace Mirea.freelance.backend.dto;

public class UserRoleResponseDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RoleId { get; set; }
    public DateTime AssignedDate { get; set; }
}