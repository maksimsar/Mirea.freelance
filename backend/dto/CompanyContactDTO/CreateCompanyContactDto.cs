namespace Mirea.freelance.backend.dto;

public class CreateCompanyContactDto
{
    public int CompanyProfileId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Telegram { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}