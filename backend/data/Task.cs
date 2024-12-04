public class Task
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; } = "Open";
    public decimal Budget { get; set; }
    public int ClientProfileId { get; set; }
    public Profile ClientProfile { get; set; }
    public int? FreelancerProfileId { get; set; }
    public Profile FreelancerProfile { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? Deadline { get; set; }
}