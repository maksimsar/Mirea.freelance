using System.ComponentModel.DataAnnotations;  // Для KeyAttribute
using System.ComponentModel.DataAnnotations.Schema;  // Для ForeignKeyAttribute
using System.Text.Json.Serialization;

public class Profile
{
    [Column("userid")]
    public int userid { get; set; }

    [Column("firstname")]
    public string FirstName { get; set; }
    [Column("lastname")]
    public string LastName { get; set; }
    [Column("age")]
    public int Age { get; set; }
    [Column("gender")]
    public string Gender { get; set; }
    [Column("rating")]
    public decimal Rating { get; set; } = 0;
}


