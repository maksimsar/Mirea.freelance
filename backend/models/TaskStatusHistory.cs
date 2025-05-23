using System;

namespace Mirea.freelance.backend.models;

public class TaskStatusHistory
{
    public int  Id            { get; set; }
    public int  ProjectTaskId { get; set; }
    public TaskStatus Status  { get; set; }
    public DateTime ChangedAt { get; set; }

    public int  ChangedByUserId { get; set; }
    public User ChangedByUser   { get; set; } = null!;

    public ProjectTask ProjectTask { get; set; } = null!;
}

