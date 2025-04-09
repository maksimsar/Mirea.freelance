using Microsoft.AspNetCore.Mvc;
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
        var (success, message, user) = await _userService.AuthenticateAsync(loginDto.Login, loginDto.Password);
        if (!success)
        {
            return BadRequest(new { message });
        }
        return Ok(new { user.Id, user.Login });
    }
}

public class LoginDto
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
}