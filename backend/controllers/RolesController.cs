using Microsoft.AspNetCore.Mvc;
using Mirea.freelance.backend.dto;
using Mirea.freelance.backend.services;

namespace Mirea.freelance.backend.controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly RoleService _roleService;

    public RolesController(RoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRoles()
    {
        var roles = await _roleService.GetAllRolesAsync();
        return Ok(roles);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRole(int id)
    {
        var role = await _roleService.GetRoleByIdAsync(id);
        if (role == null)
            return NotFound("Роль не найдена.");
        return Ok(role);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto dto)
    {
        var (success, message, role) = await _roleService.CreateRoleAsync(dto);
        if (!success)
            return BadRequest(message);
        return CreatedAtAction(nameof(GetRole), new { id = role!.Id }, role);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRole(int id, [FromBody] UpdateRoleDto dto)
    {
        var (success, message, role) = await _roleService.UpdateRoleAsync(id, dto);
        if (!success)
            return BadRequest(message);
        return Ok(role);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(int id)
    {
        var (success, message) = await _roleService.DeleteRoleAsync(id);
        if (!success)
            return NotFound(message);
        return Ok(message);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetRolesByUserId(int userId)
    {
        var userRoles = await _roleService.GetRolesByUserIdAsync(userId);
        return Ok(userRoles);
    }

    [HttpPost("user")]
    public async Task<IActionResult> AssignRoleToUser([FromBody] CreateUserRoleDto dto)
    {
        var (success, message, userRole) = await _roleService.CreateUserRoleAsync(dto);
        if (!success)
            return BadRequest(message);
        return CreatedAtAction(nameof(GetUserRole), new { id = userRole!.Id }, userRole);
    }

    [HttpGet("user/role/{id}")]
    public async Task<IActionResult> GetUserRole(int id)
    {
        var userRole = await _roleService.GetUserRoleByIdAsync(id);
        if (userRole == null)
            return NotFound("Назначение роли не найдено.");
        return Ok(userRole);
    }

    [HttpPut("user/{id}")]
    public async Task<IActionResult> UpdateUserRole(int id, [FromBody] UpdateUserRoleDto dto)
    {
        var (success, message, userRole) = await _roleService.UpdateUserRoleAsync(id, dto);
        if (!success)
            return BadRequest(message);
        return Ok(userRole);
    }

    [HttpDelete("user/{id}")]
    public async Task<IActionResult> DeleteUserRole(int id)
    {
        var (success, message) = await _roleService.DeleteUserRoleAsync(id);
        if (!success)
            return NotFound(message);
        return Ok(message);
    }
}