namespace Mirea.freelance.backend.dto;

// StudentProfile
public class CreateStudentProfileDto
{
    public int UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Patronymic { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Telegram { get; set; } = string.Empty;
    public string SphereOfDevelopment { get; set; } = string.Empty;
}

public class UpdateStudentProfileDto
{
    public string NewFirstName { get; set; } = string.Empty;
    public string NewLastName { get; set; } = string.Empty;
    public string NewPatronymic { get; set; } = string.Empty;
    public int NewAge { get; set; }
    public string NewGender { get; set; } = string.Empty;
    public string NewPhone { get; set; } = string.Empty;
    public string NewTelegram { get; set; } = string.Empty;
    public string NewSphereOfDevelopment { get; set; } = string.Empty;
}

public class StudentProfileResponseDto
{
    public int UserId { get; set; }
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

// MentorProfile
public class CreateMentorProfileDto
{
    public int UserId { get; set; }
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

public class UpdateMentorProfileDto
{
    public string NewFirstName { get; set; } = string.Empty;
    public string NewLastName { get; set; } = string.Empty;
    public string NewPatronymic { get; set; } = string.Empty;
    public int NewAge { get; set; }
    public string NewGender { get; set; } = string.Empty;
    public string NewPhone { get; set; } = string.Empty;
    public string NewTelegram { get; set; } = string.Empty;
    public string NewSphereOfDevelopment { get; set; } = string.Empty;
    public string NewOfficeAddress { get; set; } = string.Empty;
}

public class MentorProfileResponseDto
{
    public int UserId { get; set; }
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

// CompanyProfile
public class CreateCompanyProfileDto
{
    public int UserId { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyAddress { get; set; } = string.Empty;
    public string TaxId { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public List<CreateCompanyContactDto> Contacts { get; set; } = new List<CreateCompanyContactDto>();
}

public class UpdateCompanyProfileDto
{
    public string NewCompanyName { get; set; } = string.Empty;
    public string NewCompanyAddress { get; set; } = string.Empty;
    public string NewTaxId { get; set; } = string.Empty;
    public string NewWebsite { get; set; } = string.Empty;
    public List<CreateCompanyContactDto> NewContacts { get; set; } = new List<CreateCompanyContactDto>();
}

public class CompanyProfileResponseDto
{
    public int UserId { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyAddress { get; set; } = string.Empty;
    public string TaxId { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public List<CompanyContactResponseDto> Contacts { get; set; } = new List<CompanyContactResponseDto>();
}