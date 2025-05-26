using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Mirea.freelance.backend.dto.TaskDTO;
using Mirea.freelance.backend.services;
using DomainTaskStatus = Mirea.freelance.backend.models.TaskStatus;

namespace Mirea.freelance.backend.controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _svc;

    public TasksController(ITaskService svc) => _svc = svc;

    // ───────────────────────────────────────────────────────────────
    /// <summary>Куратор создаёт задачу</summary>
    [HttpPost]
    [Authorize(Roles = "Mentor")]
    public async Task<IActionResult> Create([FromBody] CreateTaskDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var task = await _svc.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    // ───────────────────────────────────────────────────────────────
    /// <summary>Получить задачу по Id</summary>
    [HttpGet("{id:int}")]
    [Authorize]                              // любой вошедший
    public async Task<IActionResult> GetById(int id)
    {
        var task = await _svc.GetTaskQueryable()
                             .FirstOrDefaultAsync(t => t.Id == id);
        return task is null ? NotFound() : Ok(task);
    }

    // ───────────────────────────────────────────────────────────────
    /// <summary>Сменить статус задачи</summary>
    [HttpPatch("{id:int}/status")]
    [Authorize]                              // студент или куратор
    public async Task<IActionResult> ChangeStatus(int id,
        [FromBody] ChangeStatusDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        bool ok = await _svc.ChangeStatusAsync(id, dto.NewStatus);
        return ok ? NoContent() : BadRequest("Недопустимый переход статуса или нет прав");
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> List([FromQuery] string role = "student",
                                        [FromQuery] int page = 1,
                                        [FromQuery] int pageSize = 20)
    {
        page      = page < 1  ? 1  : page;
        pageSize  = pageSize is < 5 or > 50 ? 20 : pageSize;

        var result = await _svc.GetListAsync(role, User, page, pageSize);
        return Ok(result);
    }

    // ───────────────────────────────────────────────────────────────
    /// <summary>История смены статусов задачи</summary>
    [HttpGet("{id:int}/history")]
    [Authorize]
    public async Task<IActionResult> GetHistory(int id)
    {
        var history = await _svc.GetHistoryAsync(id);
        return history.Count == 0 ? NotFound() : Ok(history);
    }
}
