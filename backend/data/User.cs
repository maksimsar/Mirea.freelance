using System.ComponentModel.DataAnnotations;   // Для использования [Key]
using System.ComponentModel.DataAnnotations.Schema;  // Для использования [Column]


public class User
{
    [Key]
    [Column("id")]  // Указание имени столбца как "id"
    public int Id { get; set; }

    [Column("login")]
    public string Login { get; set; }

    [Column("passwordhash")]
    public string PasswordHash { get; set; }

    [Column("registrationdate")]
    public DateTime RegistrationDate { get; set; }

}
