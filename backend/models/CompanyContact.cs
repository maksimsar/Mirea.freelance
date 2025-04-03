
namespace Mirea.freelance.backend.models;

public class CompanyContact
{
    public int Id { get; set; }

    // Внешний ключ для связи с CompanyProfile
    public int CompanyProfileId { get; set; }
    
    public CompanyProfile CompanyProfile { get; set; } = null!;
    
    public string Name { get; set; } = string.Empty;
    
    public string Phone { get; set; } = string.Empty;
    
    public string Telegram { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty; // если потребуется
}