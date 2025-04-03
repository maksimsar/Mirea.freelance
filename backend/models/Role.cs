using System.ComponentModel.DataAnnotations;  
using System.ComponentModel.DataAnnotations.Schema;  
using System.Text.Json.Serialization;


namespace Mirea.freelance.backend.models;

public class Role
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
}