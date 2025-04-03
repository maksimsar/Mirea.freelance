using System.ComponentModel.DataAnnotations;   // Для использования [Key]
using System.ComponentModel.DataAnnotations.Schema;  // Для использования [Column]


namespace Mirea.freelance.backend.models;

public class User
{
    public int Id { get; set; }
    
    public string Login { get; set; } = string.Empty;
    
    public string PasswordHash { get; set; } = string.Empty;
    
    public DateTime RegistrationDate { get; set; }
}