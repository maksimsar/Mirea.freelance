using Microsoft.EntityFrameworkCore;
using System.Linq;
using Mirea.freelance.backend.models;
using Mirea.freelance.backend.repositories;
using Mirea.freelance.backend.data;   
using Mirea.freelance.backend.dto;

namespace Mirea.freelance.backend.services;

public class ProfileService
{
    private readonly IProfileRepository _profileRepository;
    private readonly AppDbContext _dbContext;

    public ProfileService(IProfileRepository profileRepository, AppDbContext dbContext)
    {
        _profileRepository = profileRepository;
        _dbContext = dbContext;
    }

    // Получить профиль студента по UserId
    public async Task<StudentProfileResponseDto?> GetStudentProfileByUserIdAsync(int userId)
    {
        var profile = await _profileRepository.GetStudentProfileByUserIdAsync(userId);
        if (profile == null) return null;

        return new StudentProfileResponseDto
        {
            UserId = profile.UserId,
            FirstName = profile.FirstName,
            LastName = profile.LastName,
            Patronymic = profile.Patronymic,
            Age = profile.Age,
            Gender = profile.Gender,
            Phone = profile.Phone,
            Telegram = profile.Telegram,
            Rating = profile.Rating,
            SphereOfDevelopment = profile.SphereOfDevelopment
        };
    }

    // Получить профиль ментора по UserId
    public async Task<MentorProfileResponseDto?> GetMentorProfileByUserIdAsync(int userId)
    {
        var profile = await _profileRepository.GetMentorProfileByUserIdAsync(userId);
        if (profile == null) return null;

        return new MentorProfileResponseDto
        {
            UserId = profile.UserId,
            FirstName = profile.FirstName,
            LastName = profile.LastName,
            Patronymic = profile.Patronymic,
            Age = profile.Age,
            Gender = profile.Gender,
            Phone = profile.Phone,
            Telegram = profile.Telegram,
            SphereOfDevelopment = profile.SphereOfDevelopment,
            OfficeAddress = profile.OfficeAddress
        };
    }

    // Получить профиль компании по UserId
    public async Task<CompanyProfileResponseDto?> GetCompanyProfileByUserIdAsync(int userId)
    {
        var profile = await _profileRepository.GetCompanyProfileByUserIdAsync(userId);
        if (profile == null) return null;

        var contacts = profile.Contacts.Select(c => new CompanyContactResponseDto
        {
            Id = c.Id,
            CompanyProfileId = c.CompanyProfileId,
            Name = c.Name,
            Phone = c.Phone,
            Telegram = c.Telegram,
            Email = c.Email
        }).ToList();

        return new CompanyProfileResponseDto
        {
            UserId = profile.UserId,
            CompanyName = profile.CompanyName,
            CompanyAddress = profile.CompanyAddress,
            TaxId = profile.TaxId,
            Website = profile.Website,
            Contacts = contacts
        };
    }

    // Создать профиль студента
    public async Task<(bool success, string message, StudentProfileResponseDto? profile)> CreateStudentProfileAsync(CreateStudentProfileDto dto)
    {
        var profile = new StudentProfile
        {
            UserId = dto.UserId,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Patronymic = dto.Patronymic,
            Age = dto.Age,
            Gender = dto.Gender,
            Phone = dto.Phone,
            Telegram = dto.Telegram,
            SphereOfDevelopment = dto.SphereOfDevelopment
        };

        await _profileRepository.AddStudentProfileAsync(profile);

        var response = new StudentProfileResponseDto
        {
            UserId = profile.UserId,
            FirstName = profile.FirstName,
            LastName = profile.LastName,
            Patronymic = profile.Patronymic,
            Age = profile.Age,
            Gender = profile.Gender,
            Phone = profile.Phone,
            Telegram = profile.Telegram,
            Rating = profile.Rating,
            SphereOfDevelopment = profile.SphereOfDevelopment
        };

        return (true, "Профиль студента успешно создан.", response);
    }

    // Создать профиль ментора
    public async Task<(bool success, string message, MentorProfileResponseDto? profile)> CreateMentorProfileAsync(CreateMentorProfileDto dto)
    {
        var profile = new MentorProfile
        {
            UserId = dto.UserId,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Patronymic = dto.Patronymic,
            Age = dto.Age,
            Gender = dto.Gender,
            Phone = dto.Phone,
            Telegram = dto.Telegram,
            SphereOfDevelopment = dto.SphereOfDevelopment,
            OfficeAddress = dto.OfficeAddress
        };

        await _profileRepository.AddMentorProfileAsync(profile);

        var response = new MentorProfileResponseDto
        {
            UserId = profile.UserId,
            FirstName = profile.FirstName,
            LastName = profile.LastName,
            Patronymic = profile.Patronymic,
            Age = profile.Age,
            Gender = profile.Gender,
            Phone = profile.Phone,
            Telegram = profile.Telegram,
            SphereOfDevelopment = profile.SphereOfDevelopment,
            OfficeAddress = profile.OfficeAddress
        };

        return (true, "Профиль ментора успешно создан.", response);
    }

    // Создать профиль компании
    public async Task<(bool success, string message, CompanyProfileResponseDto? profile)> CreateCompanyProfileAsync(CreateCompanyProfileDto dto)
    {
        var profile = new CompanyProfile
        {
            UserId = dto.UserId,
            CompanyName = dto.CompanyName,
            CompanyAddress = dto.CompanyAddress,
            TaxId = dto.TaxId,
            Website = dto.Website,
            Contacts = dto.Contacts.Select(c => new CompanyContact
            {
                CompanyProfileId = dto.UserId,
                Name = c.Name,
                Phone = c.Phone,
                Telegram = c.Telegram,
                Email = c.Email
            }).ToList()
        };

        await _profileRepository.AddCompanyProfileAsync(profile);

        var response = new CompanyProfileResponseDto
        {
            UserId = profile.UserId,
            CompanyName = profile.CompanyName,
            CompanyAddress = profile.CompanyAddress,
            TaxId = profile.TaxId,
            Website = profile.Website,
            Contacts = profile.Contacts.Select(c => new CompanyContactResponseDto
            {
                Id = c.Id,
                CompanyProfileId = c.CompanyProfileId,
                Name = c.Name,
                Phone = c.Phone,
                Telegram = c.Telegram,
                Email = c.Email
            }).ToList()
        };

        return (true, "Профиль компании успешно создан.", response);
    }

    // Обновить профиль студента
    public async Task<(bool success, string message, StudentProfileResponseDto? profile)> UpdateStudentProfileAsync(int userId, UpdateStudentProfileDto dto)
    {
        var existingProfile = await _profileRepository.GetStudentProfileByUserIdAsync(userId);
        if (existingProfile == null)
            return (false, "Профиль студента не найден.", null);

        existingProfile.FirstName = dto.NewFirstName;
        existingProfile.LastName = dto.NewLastName;
        existingProfile.Patronymic = dto.NewPatronymic;
        existingProfile.Age = dto.NewAge;
        existingProfile.Gender = dto.NewGender;
        existingProfile.Phone = dto.NewPhone;
        existingProfile.Telegram = dto.NewTelegram;
        existingProfile.SphereOfDevelopment = dto.NewSphereOfDevelopment;

        await _profileRepository.UpdateStudentProfileAsync(existingProfile);

        var response = new StudentProfileResponseDto
        {
            UserId = existingProfile.UserId,
            FirstName = existingProfile.FirstName,
            LastName = existingProfile.LastName,
            Patronymic = existingProfile.Patronymic,
            Age = existingProfile.Age,
            Gender = existingProfile.Gender,
            Phone = existingProfile.Phone,
            Telegram = existingProfile.Telegram,
            Rating = existingProfile.Rating,
            SphereOfDevelopment = existingProfile.SphereOfDevelopment
        };

        return (true, "Профиль студента обновлен.", response);
    }

    // Обновить профиль ментора
    public async Task<(bool success, string message, MentorProfileResponseDto? profile)> UpdateMentorProfileAsync(int userId, UpdateMentorProfileDto dto)
    {
        var existingProfile = await _profileRepository.GetMentorProfileByUserIdAsync(userId);
        if (existingProfile == null)
            return (false, "Профиль ментора не найден.", null);

        existingProfile.FirstName = dto.NewFirstName;
        existingProfile.LastName = dto.NewLastName;
        existingProfile.Patronymic = dto.NewPatronymic;
        existingProfile.Age = dto.NewAge;
        existingProfile.Gender = dto.NewGender;
        existingProfile.Phone = dto.NewPhone;
        existingProfile.Telegram = dto.NewTelegram;
        existingProfile.SphereOfDevelopment = dto.NewSphereOfDevelopment;
        existingProfile.OfficeAddress = dto.NewOfficeAddress;

        await _profileRepository.UpdateMentorProfileAsync(existingProfile);

        var response = new MentorProfileResponseDto
        {
            UserId = existingProfile.UserId,
            FirstName = existingProfile.FirstName,
            LastName = existingProfile.LastName,
            Patronymic = existingProfile.Patronymic,
            Age = existingProfile.Age,
            Gender = existingProfile.Gender,
            Phone = existingProfile.Phone,
            Telegram = existingProfile.Telegram,
            SphereOfDevelopment = existingProfile.SphereOfDevelopment,
            OfficeAddress = existingProfile.OfficeAddress
        };

        return (true, "Профиль ментора обновлен.", response);
    }

    // Обновить профиль компании
    public async Task<(bool success, string message, CompanyProfileResponseDto? profile)> UpdateCompanyProfileAsync(int userId, UpdateCompanyProfileDto dto)
    {
        var existingProfile = await _profileRepository.GetCompanyProfileByUserIdAsync(userId);
        if (existingProfile == null)
            return (false, "Профиль компании не найден.", null);

        existingProfile.CompanyName = dto.NewCompanyName;
        existingProfile.CompanyAddress = dto.NewCompanyAddress;
        existingProfile.TaxId = dto.NewTaxId;
        existingProfile.Website = dto.NewWebsite;
        existingProfile.Contacts = dto.NewContacts.Select(c => new CompanyContact
        {
            CompanyProfileId = userId,
            Name = c.Name,
            Phone = c.Phone,
            Telegram = c.Telegram,
            Email = c.Email
        }).ToList();

        await _profileRepository.UpdateCompanyProfileAsync(existingProfile);

        var response = new CompanyProfileResponseDto
        {
            UserId = existingProfile.UserId,
            CompanyName = existingProfile.CompanyName,
            CompanyAddress = existingProfile.CompanyAddress,
            TaxId = existingProfile.TaxId,
            Website = existingProfile.Website,
            Contacts = existingProfile.Contacts.Select(c => new CompanyContactResponseDto
            {
                Id = c.Id,
                CompanyProfileId = c.CompanyProfileId,
                Name = c.Name,
                Phone = c.Phone,
                Telegram = c.Telegram,
                Email = c.Email
            }).ToList()
        };

        return (true, "Профиль компании обновлен.", response);
    }

    // Удалить профиль
    public async Task<(bool success, string message)> DeleteProfileAsync(int userId)
    {
        var studentProfile = await _profileRepository.GetStudentProfileByUserIdAsync(userId);
        var mentorProfile = await _profileRepository.GetMentorProfileByUserIdAsync(userId);
        var companyProfile = await _profileRepository.GetCompanyProfileByUserIdAsync(userId);

        if (studentProfile == null && mentorProfile == null && companyProfile == null)
            return (false, "Профиль не найден.");

        await _profileRepository.DeleteProfileAsync(userId);
        return (true, "Профиль успешно удален.");
    }

   // List profiles by role: "student", "mentor", "company"
    public async Task<IEnumerable<object>> GetListByRoleAsync(string role)
    {
        role = role?.Trim().ToLower();
        switch (role)
        {
            case "student":
                return await _dbContext.StudentProfiles
                    .Select(p => new StudentProfileResponseDto {
                        UserId               = p.UserId,
                        FirstName            = p.FirstName,
                        LastName             = p.LastName,
                        Patronymic           = p.Patronymic,
                        Age                  = p.Age,
                        Gender               = p.Gender,
                        Phone                = p.Phone,
                        Telegram             = p.Telegram,
                        Rating               = p.Rating,
                        SphereOfDevelopment  = p.SphereOfDevelopment
                    })
                    .ToListAsync<object>();

            case "mentor":
                return await _dbContext.MentorProfiles
                    .Select(p => new MentorProfileResponseDto {
                        UserId              = p.UserId,
                        FirstName           = p.FirstName,
                        LastName            = p.LastName,
                        Patronymic          = p.Patronymic,
                        Age                 = p.Age,
                        Gender              = p.Gender,
                        Phone               = p.Phone,
                        Telegram            = p.Telegram,
                        SphereOfDevelopment = p.SphereOfDevelopment,
                        OfficeAddress       = p.OfficeAddress
                    })
                    .ToListAsync<object>();

            case "company":
                return await _dbContext.CompanyProfiles
                    .Include(c => c.Contacts)
                    .Select(p => new CompanyProfileResponseDto {
                        UserId         = p.UserId,
                        CompanyName    = p.CompanyName,
                        CompanyAddress = p.CompanyAddress,
                        TaxId          = p.TaxId,
                        Website        = p.Website,
                        Contacts       = p.Contacts
                            .Select(c => new CompanyContactResponseDto {
                                Name  = c.Name,   
                                Email = c.Email,
                                Phone = c.Phone
                            })
                            .ToList()
                    })
                    .ToListAsync<object>();

            default:
                return Enumerable.Empty<object>();
        }
    }

}