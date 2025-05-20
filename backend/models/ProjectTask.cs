using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mirea.freelance.backend.models;        // ← добавь, если нет

namespace Mirea.freelance.backend.models
{
    public enum TaskStatus { Open, AwaitingReview, Done, Rejected }

    public class ProjectTask
    {
        public int Id { get; set; }

        // ← type Order, а имя навигации пусть останется Project
        public int OrderId  { get; set; }
        public Order Order  { get; set; } = null!;

        public int AssigneeStudentId { get; set; }
        public StudentProfile Assignee { get; set; } = null!;

        [MaxLength(100)]  public string Title       { get; set; } = null!;
        [MaxLength(1000)] public string Description { get; set; } = null!;

        public TaskStatus Status { get; set; } = TaskStatus.Open;
        public ICollection<TaskStatusHistory> StatusHistory { get; set; } = new List<TaskStatusHistory>();
    }
}
