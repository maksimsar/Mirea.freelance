
namespace Mirea.freelance.backend.models;

public class CompanyContact
{
    public int Id { get; set; }

    // Внешний ключ для связи с CompanyProfile
    public int CompanyProfileId { get; set; }
    
    public CompanyProfile CompanyProfile { get; set; }
    
    public string Name { get; set; }
    
    public string Phone { get; set; }
    
    public string Telegram { get; set; }
    
    public string Email { get; set; } // если потребуется
}