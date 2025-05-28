using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mirea.freelance.backend.dto;
using Mirea.freelance.backend.services;

namespace Mirea.freelance.backend.controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Student,Company,Mentor,Admin")]
    public async Task<IActionResult> GetUser(int id)
    {
        var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userRoles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();

        // Фильтрация: пользователь видит только свой профиль
        if (!userRoles.Contains("Admin") && currentUserId != id.ToString())
            return Forbid("У вас нет доступа к этому пользователю.");

        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
            return NotFound("Пользователь не найден.");
        return Ok(user);

    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
    {
        var (success, message, user) = await _userService.CreateUserAsync(dto);
        if (!success)
            return BadRequest(message);
        return CreatedAtAction(nameof(GetUser), new { id = user!.Id }, user);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Student,Company,Mentor,Admin")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto dto)
    {
        var (success, message, user) = await _userService.UpdateUserAsync(id, dto);
        if (!success)
            return BadRequest(message);
        return Ok(user);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var (success, message) = await _userService.DeleteUserAsync(id);
        if (!success)
            return NotFound(message);
        return Ok(message);
    }
}