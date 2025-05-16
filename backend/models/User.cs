using System.ComponentModel.DataAnnotations;   // Для использования [Key]
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;  // Для использования [Column]


namespace Mirea.freelance.backend.models;

public class User: IdentityUser<int>
{
    [Key]
    public override int Id { get; set; } 

//сохраняем поле логин, мапя его на юзернейм
    [Column("Login")]    
    public override string UserName { get; set; } = string.Empty;

//не сохраняется в бд, просто для удобства оставлена
    [NotMapped]
    public string Login{
        get => UserName;
        set=> UserName = value;
    }

    [Column("PasswordHash")]
    public override string PasswordHash { get; set; } = string.Empty;
    
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
}