namespace Mirea.freelance.backend.models;

public class JwtOptions
{
    public const string SectionName = "Jwt"; // Имя в appsettings.json

    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
}