using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Mirea.freelance.backend.models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Mirea.freelance.backend.services;

public class JwtService
{
    private readonly JwtOptions _jwtOptions;
    private readonly UserManager<User> _userManager;

    public JwtService(IOptions<JwtOptions> JwtOptions, UserManager<User> userManager)
    {
        _jwtOptions = JwtOptions.Value;
        _userManager = userManager;
    }
 
    public async Task<string> GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim("login", user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}