namespace Mirea.freelance.backend.dto;

public class OrderDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = "Open";
    public decimal Budget { get; set; }
    public int CompanyProfileId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? Deadline { get; set; }
    public List<int> FreelancerProfileIds { get; set; } = new List<int>();
}