using Microsoft.AspNetCore.Mvc;
using Mirea.freelance.backend.dto;
using Mirea.freelance.backend.services;
using Microsoft.AspNetCore.Authorization;


namespace Mirea.freelance.backend.controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
    private readonly ProfileService _profileService;

    public ProfilesController(ProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpGet("student/{userId}")]
    public async Task<IActionResult> GetStudentProfile(int userId)
    {
        var profile = await _profileService.GetStudentProfileByUserIdAsync(userId);
        if (profile == null)
            return NotFound("Профиль студента не найден.");
        return Ok(profile);
    }

    [HttpGet("mentor/{userId}")]
    public async Task<IActionResult> GetMentorProfile(int userId)
    {
        var profile = await _profileService.GetMentorProfileByUserIdAsync(userId);
        if (profile == null)
            return NotFound("Профиль ментора не найден.");
        return Ok(profile);
    }

    [HttpGet("company/{userId}")]
    public async Task<IActionResult> GetCompanyProfile(int userId)
    {
        var profile = await _profileService.GetCompanyProfileByUserIdAsync(userId);
        if (profile == null)
            return NotFound("Профиль компании не найден.");
        return Ok(profile);
    }

    [HttpPost("student")]
    public async Task<IActionResult> CreateStudentProfile([FromBody] CreateStudentProfileDto dto)
    {
        var (success, message, profile) = await _profileService.CreateStudentProfileAsync(dto);
        if (!success)
            return BadRequest(message);
        return CreatedAtAction(nameof(GetStudentProfile), new { userId = profile!.UserId }, profile);
    }

    [HttpPost("mentor")]
    public async Task<IActionResult> CreateMentorProfile([FromBody] CreateMentorProfileDto dto)
    {
        var (success, message, profile) = await _profileService.CreateMentorProfileAsync(dto);
        if (!success)
            return BadRequest(message);
        return CreatedAtAction(nameof(GetMentorProfile), new { userId = profile!.UserId }, profile);
    }

    [HttpPost("company")]
    public async Task<IActionResult> CreateCompanyProfile([FromBody] CreateCompanyProfileDto dto)
    {
        var (success, message, profile) = await _profileService.CreateCompanyProfileAsync(dto);
        if (!success)
            return BadRequest(message);
        return CreatedAtAction(nameof(GetCompanyProfile), new { userId = profile!.UserId }, profile);
    }

    [HttpPut("student/{userId}")]
    public async Task<IActionResult> UpdateStudentProfile(int userId, [FromBody] UpdateStudentProfileDto dto)
    {
        var (success, message, profile) = await _profileService.UpdateStudentProfileAsync(userId, dto);
        if (!success)
            return BadRequest(message);
        return Ok(profile);
    }

    [HttpPut("mentor/{userId}")]
    public async Task<IActionResult> UpdateMentorProfile(int userId, [FromBody] UpdateMentorProfileDto dto)
    {
        var (success, message, profile) = await _profileService.UpdateMentorProfileAsync(userId, dto);
        if (!success)
            return BadRequest(message);
        return Ok(profile);
    }

    [HttpPut("company/{userId}")]
    public async Task<IActionResult> UpdateCompanyProfile(int userId, [FromBody] UpdateCompanyProfileDto dto)
    {
        var (success, message, profile) = await _profileService.UpdateCompanyProfileAsync(userId, dto);
        if (!success)
            return BadRequest(message);
        return Ok(profile);
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteProfile(int userId)
    {
        var (success, message) = await _profileService.DeleteProfileAsync(userId);
        if (!success)
            return NotFound(message);
        return Ok(message);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> ListByRole([FromQuery] string role)
    {
        if (string.IsNullOrWhiteSpace(role))
            return BadRequest("Query parameter 'role' is required.");

        var list = await _profileService.GetListByRoleAsync(role);
        return Ok(list);
    }
}