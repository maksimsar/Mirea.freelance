using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mirea.freelance.backend.dto;
using Mirea.freelance.backend.services;

namespace Mirea.freelance.backend.controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
    private readonly ProfileService _profileService;

    public ProfilesController(ProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpGet("student/{userId}")]
    [Authorize(Roles = "Student,Company,Mentor,Admin")]
    public async Task<IActionResult> GetStudentProfile(int userId)
    {
        var profile = await _profileService.GetStudentProfileByUserIdAsync(userId);
        if (profile == null)
            return NotFound("Профиль студента не найден.");
        return Ok(profile);
    }

    [HttpGet("mentor/{userId}")]
    [Authorize(Roles = "Student,Company,Mentor,Admin")]
    public async Task<IActionResult> GetMentorProfile(int userId)
    {
        var profile = await _profileService.GetMentorProfileByUserIdAsync(userId);
        if (profile == null)
            return NotFound("Профиль ментора не найден.");
        return Ok(profile);
    }

    [HttpGet("company/{userId}")]
    [Authorize(Roles = "Student,Company,Mentor,Admin")]
    public async Task<IActionResult> GetCompanyProfile(int userId)
    {
        var profile = await _profileService.GetCompanyProfileByUserIdAsync(userId);
        if (profile == null)
            return NotFound("Профиль компании не найден.");
        return Ok(profile);
    }

    [HttpPost("student")]
    [Authorize(Roles = "Student,Admin")]
    public async Task<IActionResult> CreateStudentProfile([FromBody] CreateStudentProfileDto dto)
    {
        var (success, message, profile) = await _profileService.CreateStudentProfileAsync(dto);
        if (!success)
            return BadRequest(message);
        return CreatedAtAction(nameof(GetStudentProfile), new { userId = profile!.UserId }, profile);
    }

    [HttpPost("mentor")]
    [Authorize(Roles = "Mentor,Admin")]
    public async Task<IActionResult> CreateMentorProfile([FromBody] CreateMentorProfileDto dto)
    {
        var (success, message, profile) = await _profileService.CreateMentorProfileAsync(dto);
        if (!success)
            return BadRequest(message);
        return CreatedAtAction(nameof(GetMentorProfile), new { userId = profile!.UserId }, profile);
    }

    [HttpPost("company")]
    [Authorize(Roles = "Company,Admin")]
    public async Task<IActionResult> CreateCompanyProfile([FromBody] CreateCompanyProfileDto dto)
    {
        var (success, message, profile) = await _profileService.CreateCompanyProfileAsync(dto);
        if (!success)
            return BadRequest(message);
        return CreatedAtAction(nameof(GetCompanyProfile), new { userId = profile!.UserId }, profile);
    }

    [HttpPut("student/{userId}")]
    [Authorize(Roles = "Student,Admin")]
    public async Task<IActionResult> UpdateStudentProfile(int userId, [FromBody] UpdateStudentProfileDto dto)
    {
        var (success, message, profile) = await _profileService.UpdateStudentProfileAsync(userId, dto);
        if (!success)
            return BadRequest(message);
        return Ok(profile);
    }

    [HttpPut("mentor/{userId}")]
    [Authorize(Roles = "Mentor,Admin")]
    public async Task<IActionResult> UpdateMentorProfile(int userId, [FromBody] UpdateMentorProfileDto dto)
    {
        var (success, message, profile) = await _profileService.UpdateMentorProfileAsync(userId, dto);
        if (!success)
            return BadRequest(message);
        return Ok(profile);
    }

    [HttpPut("company/{userId}")]
    [Authorize(Roles = "Company,Admin")]
    public async Task<IActionResult> UpdateCompanyProfile(int userId, [FromBody] UpdateCompanyProfileDto dto)
    {
        var (success, message, profile) = await _profileService.UpdateCompanyProfileAsync(userId, dto);
        if (!success)
            return BadRequest(message);
        return Ok(profile);
    }

    [HttpDelete("{userId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProfile(int userId)
    {
        var (success, message) = await _profileService.DeleteProfileAsync(userId);
        if (!success)
            return NotFound(message);
        return Ok(message);
    }
}