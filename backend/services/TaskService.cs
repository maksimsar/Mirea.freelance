using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

using Mirea.freelance.backend.data;
using Mirea.freelance.backend.models;
using Mirea.freelance.backend.dto.TaskDTO;
using Mirea.freelance.backend.dto; 

// ─── алиас, чтобы не конфликтовать с System.Threading.Tasks.TaskStatus ───
using DomainTaskStatus = Mirea.freelance.backend.models.TaskStatus;

namespace Mirea.freelance.backend.services;

/// <summary>Сервис работы с задачами проекта</summary>
public interface ITaskService
{
    Task<ProjectTask>  CreateAsync(CreateTaskDto dto);
    Task<bool>         ChangeStatusAsync(int taskId, DomainTaskStatus newStatus);
    IQueryable<ProjectTask> GetTaskQueryable();
    Task<IReadOnlyList<TaskStatusHistory>> GetHistoryAsync(int taskId);
    Task<PagedResult<ProjectTask>> GetListAsync(string role, ClaimsPrincipal user, int page = 1, int pageSize = 20);
}

public class TaskService : ITaskService
{
    private readonly AppDbContext         _db;
    private readonly IHttpContextAccessor _http;

    public TaskService(AppDbContext db, IHttpContextAccessor http)
    {
        _db   = db;
        _http = http;
    }

    /// <summary>Куратор создаёт новую задачу</summary>
    public async Task<ProjectTask> CreateAsync(CreateTaskDto dto)
    {
        var task = new ProjectTask
        {
            OrderId           = dto.OrderId,
            AssigneeStudentId = dto.AssigneeStudentId,
            Title             = dto.Title,
            Description       = dto.Description,
            Status            = DomainTaskStatus.Open
        };

        _db.ProjectTasks.Add(task);
        await _db.SaveChangesAsync();
        return task;
    }

    /// <summary>Сменить статус задачи с учётом роли пользователя</summary>
    public async Task<bool> ChangeStatusAsync(int taskId, DomainTaskStatus newStatus)
    {
        var task = await _db.ProjectTasks
                            .Include(t => t.StatusHistory)
                            .FirstOrDefaultAsync(t => t.Id == taskId);
        if (task is null) return false;

        var user = _http.HttpContext?.User
                   ?? throw new InvalidOperationException("HttpContext unavailable");
        var isMentor  = user.IsInRole("Mentor");
        var isStudent = user.IsInRole("Student");

        bool allowed = task.Status switch
        {
            DomainTaskStatus.Open when
                isStudent && newStatus == DomainTaskStatus.AwaitingReview => true,

            DomainTaskStatus.AwaitingReview when isMentor &&
                (newStatus == DomainTaskStatus.Done ||
                 newStatus == DomainTaskStatus.Rejected) => true,

            _ => false
        };
        if (!allowed) return false;

        task.Status = newStatus;

        task.StatusHistory.Add(new TaskStatusHistory
        {
            Status          = newStatus,
            ChangedAt       = DateTime.UtcNow,
            ChangedByUserId = int.Parse(
                user.FindFirst(ClaimTypes.NameIdentifier)!.Value) // "sub" если так настроено
        });

        await _db.SaveChangesAsync();
        return true;
    }

    /// <summary>Отдаём IQueryable без трекинга — удобно для контроллера/репозитория</summary>
    public IQueryable<ProjectTask> GetTaskQueryable() =>
        _db.ProjectTasks.AsNoTracking();

    /// <summary>История смены статусов конкретной задачи</summary>
    public async Task<IReadOnlyList<TaskStatusHistory>> GetHistoryAsync(int taskId) =>
        await _db.TaskStatusHistories
                 .Where(h => h.ProjectTaskId == taskId)
                 .OrderBy(h => h.ChangedAt)
                 .AsNoTracking()
                 .ToListAsync();

    public async Task<PagedResult<ProjectTask>> GetListAsync(string role,
        ClaimsPrincipal user, int page = 1, int pageSize = 20)
    {
        var query = _db.ProjectTasks.AsNoTracking();

        if (role.Equals("mentor", StringComparison.OrdinalIgnoreCase))
        {
            // куратор → задачи заказов, где он куратор
            int mentorId = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            query = query.Where(t => t.Order.MentorProfile.UserId == mentorId);
        }
        else // student (по умолчанию)
        {
            int studentId = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            query = query.Where(t => t.AssigneeStudentId == studentId);
        }

        int total = await query.CountAsync();

        var items = await query
            .OrderBy(t => t.Status)                 // сортировка любая
            .ThenBy(t => t.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<ProjectTask>(items, page, pageSize, total);
    }

}

