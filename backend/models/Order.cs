
namespace Mirea.freelance.backend.models;

    public enum OrderStatus
    {
        Open,
        Unprocessed,
        Processing,
        ProcessedPositive,
        ProcessedNegative
    }
public class Order
{
    public int Id { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public OrderStatus Status { get; set; } = OrderStatus.Open;
    
    public decimal Budget { get; set; }
    
    public int CompanyProfileId { get; set; }
    
    public CompanyProfile CompanyProfile { get; set; } = null!;
    
    public ICollection<StudentProfile> FreelancerProfiles { get; set; } = new List<StudentProfile>();
    
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    public DateTime? Deadline { get; set; }
    public string RequiredRoles { get; set; } = string.Empty;
    
    public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public ICollection<ProjectStudent> ProjectStudents { get; set; } = new List<ProjectStudent>();

    public ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();

     public string PreferredContactMethods { get; set; } = string.Empty;

}



