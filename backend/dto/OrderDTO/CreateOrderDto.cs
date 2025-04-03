namespace Mirea.freelance.backend.dto;

public class CreateOrderDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Budget { get; set; }
    public int CompanyProfileId { get; set; }
    public DateTime? Deadline { get; set; } 
}