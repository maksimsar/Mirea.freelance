using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Mirea.freelance.backend.dto.TaskDTO;
using Mirea.freelance.backend.services;
using Mirea.freelance.backend.models;
using Microsoft.EntityFrameworkCore;
using DomainTaskStatus = Mirea.freelance.backend.models.TaskStatus;



namespace Mirea.freelance.backend.controllers;

[ApiController]
[Authorize]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _svc;

    public TasksController(ITaskService svc) => _svc = svc;

    /// <summary>Куратор создаёт задачу</summary>
    [HttpPost]
    [Authorize(Roles = "Mentor, Admin")]
    public async Task<IActionResult> Create([FromBody] CreateTaskDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var task = await _svc.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    /// <summary>Получить задачу по Id (для CreatedAtAction)</summary>
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Student,Company,Mentor,Admin")]                     // любой аутентифицированный
    public async Task<IActionResult> GetById(int id)      // ← имя метода
    {
        var task = await _svc.GetTaskQueryable()
                            .FirstOrDefaultAsync(t => t.Id == id);

        return task is null ? NotFound() : Ok(task);
    }

    /// <summary>Сменить статус задачи</summary>
    [HttpPatch("{id:int}/status")]
    [Authorize(Roles = "Student,Mentor,Admin")] // студент меняет себе на AwaitingReview, куратор — на Done/Rejected
    public async Task<IActionResult> ChangeStatus(int id, [FromBody] ChangeStatusDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var ok = await _svc.ChangeStatusAsync(id, dto.NewStatus);
        return ok ? NoContent() : NotFound();
    }
}
