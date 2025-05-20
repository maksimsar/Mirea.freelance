namespace Mirea.freelance.backend.models;

public class ProjectStudent
{
    public int OrderId  { get; set; }            // ← Order = «проект»
    public Order Order  { get; set; } = null!;

    public int StudentId { get; set; }
    public StudentProfile Student { get; set; } = null!;

    public DateTime AssignedDate { get; set; } = DateTime.UtcNow;
}

