using Microsoft.AspNetCore.Mvc;
using Mirea.freelance.backend.dto;
using Mirea.freelance.backend.services;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;

    public AuthController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var (success, message, user, token) = await _userService.AuthenticateAsync(loginDto.Login, loginDto.Password);
        if (!success)
        {
            return Unauthorized(new { Message = message });
        }
        return Ok(new { Token = token, user!.Id, user.Login});
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserDto createUserDto)
    {
        var (success, message, user) = await _userService.CreateUserAsync(createUserDto);
        if (!success)
        {
            return BadRequest(new {Message = message});
        }

        return Ok(new {Message = message, user!.Id, user.Login});
    }
}
