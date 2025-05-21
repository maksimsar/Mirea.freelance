using Microsoft.EntityFrameworkCore;
using Mirea.freelance.backend.data;
using Mirea.freelance.backend.models;
using Mirea.freelance.backend.dto.TaskDTO;
using DomainTaskStatus = Mirea.freelance.backend.models.TaskStatus;


namespace Mirea.freelance.backend.services;

public interface ITaskService
{
    Task<ProjectTask> CreateAsync(CreateTaskDto dto);
    Task<bool> ChangeStatusAsync(int taskId, DomainTaskStatus newStatus);
    IQueryable<ProjectTask> GetTaskQueryable();
}

public class TaskService : ITaskService
{
    private readonly AppDbContext _db;
    public TaskService(AppDbContext db) => _db = db;

    public async Task<ProjectTask> CreateAsync(CreateTaskDto dto)
    {
        var task = new ProjectTask
        {
            OrderId           = dto.OrderId,
            AssigneeStudentId = dto.AssigneeStudentId,
            Title             = dto.Title,
            Description       = dto.Description
        };

        _db.ProjectTasks.Add(task);
        await _db.SaveChangesAsync();
        return task;
    }

    public async Task<bool> ChangeStatusAsync(int taskId, DomainTaskStatus newStatus)
    {
        var task = await _db.ProjectTasks.FindAsync(taskId);
        if (task is null || task.Status == newStatus) return false;

        task.Status = newStatus;
        _db.TaskStatusHistories.Add(new TaskStatusHistory
        {
            ProjectTaskId = taskId,
            Status        = newStatus
        });

        await _db.SaveChangesAsync();
        return true;
    }

    public IQueryable<ProjectTask> GetTaskQueryable() => _db.ProjectTasks.AsNoTracking();

}
