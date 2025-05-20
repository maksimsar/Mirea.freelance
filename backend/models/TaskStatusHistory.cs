using System;

namespace Mirea.freelance.backend.models;

public class TaskStatusHistory
{
    public int Id { get; set; }

    public int ProjectTaskId { get; set; }
    public ProjectTask ProjectTask { get; set; } = null!;

    public TaskStatus Status { get; set; }
    public DateTime   ChangedAt { get; set; } = DateTime.UtcNow;
}
