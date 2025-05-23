using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

using Mirea.freelance.backend.data;
using Mirea.freelance.backend.models;
using Mirea.freelance.backend.dto;
using Mirea.freelance.backend.dto.TaskDTO;

// ── алиас, чтобы не конфликтовать с System.Threading.Tasks.TaskStatus ──
using DomainTaskStatus = Mirea.freelance.backend.models.TaskStatus;

namespace Mirea.freelance.backend.services;

public interface ITaskService
{
    Task<ProjectTask>  CreateAsync(CreateTaskDto dto);
    Task<bool>         ChangeStatusAsync(int taskId, DomainTaskStatus newStatus);

    /// список задач без трекинга – удобно контроллеру
    IQueryable<ProjectTask> GetTaskQueryable();

    /// история по-конкретной задаче
    Task<IReadOnlyList<TaskStatusHistory>> GetHistoryAsync(int taskId);

    /// задачи одного проекта (для ProjectPage)
    Task<IEnumerable<TaskListItemDto>> GetProjectTasksAsync(int projectId);

    /// пагинация «мои задачи» / «задачи моих проектов»
    Task<PagedResult<ProjectTask>> GetListAsync(
        string role, ClaimsPrincipal user, int page = 1, int pageSize = 20);
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

    /* ---------- С О З Д А Т Ь  ---------- */

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

    /* ---------- С М Е Н А   С Т А Т У С А ---------- */

    public async Task<bool> ChangeStatusAsync(int taskId, DomainTaskStatus newStatus)
    {
        var task = await _db.ProjectTasks
                            .Include(t => t.StatusHistory)
                            .FirstOrDefaultAsync(t => t.Id == taskId);
        if (task is null) return false;

        var user = _http.HttpContext?.User
                   ?? throw new InvalidOperationException("HttpContext unavailable");
        bool isMentor  = user.IsInRole("Mentor");
        bool isStudent = user.IsInRole("Student");

        bool allowed = task.Status switch
        {
            DomainTaskStatus.Open
                 when isStudent  && newStatus == DomainTaskStatus.AwaitingReview => true,

            DomainTaskStatus.AwaitingReview
                 when isMentor   &&
                      (newStatus == DomainTaskStatus.Done ||
                       newStatus == DomainTaskStatus.Rejected)                  => true,

            DomainTaskStatus.Rejected
                 when isStudent  && newStatus == DomainTaskStatus.AwaitingReview => true,

            _ => false
        };
        if (!allowed) return false;

        task.Status = newStatus;

        task.StatusHistory.Add(new TaskStatusHistory
        {
            Status          = newStatus,
            ChangedAt       = DateTime.UtcNow,
            ChangedByUserId = int.Parse(
                user.FindFirst(ClaimTypes.NameIdentifier)!.Value)
        });

        await _db.SaveChangesAsync();
        return true;
    }

    /* ---------- Q U E R I E S ---------- */

    public IQueryable<ProjectTask> GetTaskQueryable() =>
        _db.ProjectTasks.AsNoTracking();

    public async Task<IReadOnlyList<TaskStatusHistory>> GetHistoryAsync(int taskId) =>
        await _db.TaskStatusHistories
                 .Where(h => h.ProjectTaskId == taskId)
                 .OrderBy(h => h.ChangedAt)
                 .AsNoTracking()
                 .ToListAsync();


    public async Task<IEnumerable<TaskListItemDto>> GetProjectTasksAsync(int projectId)
    {
        return await _db.ProjectTasks
            .Where(t => t.OrderId == projectId)
            .Select(t => new TaskListItemDto(
                t.Id,
                t.Title,
                t.Description,
                t.Status,
                t.AssigneeStudentId,
                // ----------- было: $"{t.Assignee.LastName} {t.Assignee.FirstName[..1]}."
                t.Assignee.LastName + " " +               // Фамилия
                t.Assignee.FirstName.Substring(0, 1) + "."// Инициал
            ))
            .AsNoTracking()
            .ToListAsync();
    }



    public async Task<PagedResult<ProjectTask>> GetListAsync(
        string role, ClaimsPrincipal user, int page = 1, int pageSize = 20)
    {
        var query = _db.ProjectTasks.AsNoTracking();

        if (role.Equals("mentor", StringComparison.OrdinalIgnoreCase))
        {
            int mentorId = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            query = query.Where(t => t.Order.MentorProfile.UserId == mentorId);
        }
        else               // student
        {
            int studentId = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            query = query.Where(t => t.AssigneeStudentId == studentId);
        }

        int total = await query.CountAsync();

        var items = await query
            .OrderBy(t => t.Status)
            .ThenBy(t => t.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<ProjectTask>(items, page, pageSize, total);
    }
}
