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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var task = await _svc.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var task = await _svc.GetTaskQueryable()
                             .FirstOrDefaultAsync(t => t.Id == id);
        return task is null ? NotFound() : Ok(task);
    }

    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> ChangeStatus(int id,
        [FromBody] ChangeStatusDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        bool ok = await _svc.ChangeStatusAsync(id, dto.NewStatus);
        return ok ? NoContent() : BadRequest("Недопустимый переход статуса или нет прав");
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] string role = "student",
                                        [FromQuery] int page = 1,
                                        [FromQuery] int pageSize = 20)
    {
        page      = page < 1  ? 1  : page;
        pageSize  = pageSize is < 5 or > 50 ? 20 : pageSize;

        var result = await _svc.GetListAsync(role, User, page, pageSize);
        return Ok(result);
    }

    [HttpGet("{id:int}/history")]
    public async Task<IActionResult> GetHistory(int id)
    {
        var history = await _svc.GetHistoryAsync(id);
        return history.Count == 0 ? NotFound() : Ok(history);
    }

    [HttpGet("/api/projects/{id:int}/tasks")]
    public async Task<IActionResult> GetTasksByProject(int id)
    {
        var tasks = await _svc.GetProjectTasksAsync(id);
        return Ok(tasks);
    }
}
