using Microsoft.AspNetCore.Mvc;
using Mirea.Freelance.backend.data;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.Email))
        {
            return BadRequest(new CreateUserResponse
            {
                Status = "error",
                ErrorMessage = "Invalid parameters"
            });
        }

        var (success, message, createdUser) = await _userService.CreateUserAsync(request.Username, request.Password, request.Email);

        if (!success)
        {
            return Conflict(new CreateUserResponse
            {
                Status = "error",
                ErrorMessage = message
            });
        }

        return CreatedAtAction(nameof(CreateUser), new { id = createdUser!.Id }, new CreateUserResponse
        {
            UserId = createdUser.Id,
            Status = "created"
        });
    }
}
