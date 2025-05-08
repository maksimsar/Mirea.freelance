using System;
using Microsoft.AspNetCore.Identity;


namespace Mirea.freelance.backend.models;

public class UserRole
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    
    public User User { get; set; } = null!;
    
    public int RoleId { get; set; }
    
    public IdentityRole<int> Role { get; set; } = null!;
    
    public DateTime AssignedDate { get; set; } = DateTime.Now;
}