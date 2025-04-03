namespace Mirea.freelance.backend.dto;

public abstract class ProfileDto
{
    public int UserId { get; set; }
}

public class StudentProfileDto : ProfileDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Patronymic { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Telegram { get; set; } = string.Empty;
    public decimal Rating { get; set; }
    public string SphereOfDevelopment { get; set; } = string.Empty;
}

public class MentorProfileDto : ProfileDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Patronymic { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Telegram { get; set; } = string.Empty;
    public string SphereOfDevelopment { get; set; } = string.Empty;
    public string OfficeAddress { get; set; } = string.Empty;
}

public class CompanyProfileDto : ProfileDto
{
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyAddress { get; set; } = string.Empty;
    public string TaxId { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public List<CompanyContactDto> Contacts { get; set; } = new List<CompanyContactDto>();
}