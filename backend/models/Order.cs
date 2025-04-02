
namespace Mirea.freelance.backend.models;

public class Order
{
    public int Id { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public string Status { get; set; } = "Open";
    
    public decimal Budget { get; set; }
    
    public int ClientProfileId { get; set; }
    
    public CompanyProfile ClientProfile { get; set; } = null!;
    
    public int? FreelancerProfileId { get; set; }
    
    public StudentProfile FreelancerProfile { get; set; } = null!;
    
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    public DateTime? Deadline { get; set; }
}



