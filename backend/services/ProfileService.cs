using Mirea.freelance.backend.models;
using Mirea.freelance.backend.repositories;

namespace Mirea.freelance.backend.services;

public class ProfileService
{
    private readonly IProfileRepository _profileRepository;

    public ProfileService(IProfileRepository profileRepository)
    {
        _profileRepository = profileRepository;
    }

    public async Task<(bool success, string message, StudentProfile? profile)> CreateStudentProfileAsync(
        int userId, string firstName, string lastName, string patronymic, int age, string gender,
        string phone, string telegram, decimal rating, string sphereOfDevelopment)
    {
        var existingProfile = await _profileRepository.GetStudentProfileByUserIdAsync(userId) ??
                             await _profileRepository.GetMentorProfileByUserIdAsync(userId) ??
                             await _profileRepository.GetCompanyProfileByUserIdAsync(userId);
        if (existingProfile != null)
        {
            return (false, "Profile for this user already exists.", null);
        }

        var profile = new StudentProfile
        {
            UserId = userId,
            FirstName = firstName,
            LastName = lastName,
            Patronymic = patronymic,
            Age = age,
            Gender = gender,
            Phone = phone,
            Telegram = telegram,
            Rating = rating,
            SphereOfDevelopment = sphereOfDevelopment
        };

        await _profileRepository.AddStudentProfileAsync(profile);
        return (true, "Student profile created successfully.", profile);
    }

    public async Task<(bool success, string message, MentorProfile? profile)> CreateMentorProfileAsync(
        int userId, string firstName, string lastName, string patronymic, int age, string gender,
        string phone, string telegram, string sphereOfDevelopment, string officeAddress)
    {
        var existingProfile = await _profileRepository.GetStudentProfileByUserIdAsync(userId) ??
                             await _profileRepository.GetMentorProfileByUserIdAsync(userId) ??
                             await _profileRepository.GetCompanyProfileByUserIdAsync(userId);
        if (existingProfile != null)
        {
            return (false, "Profile for this user already exists.", null);
        }

        var profile = new MentorProfile
        {
            UserId = userId,
            FirstName = firstName,
            LastName = lastName,
            Patronymic = patronymic,
            Age = age,
            Gender = gender,
            Phone = phone,
            Telegram = telegram,
            SphereOfDevelopment = sphereOfDevelopment,
            OfficeAddress = officeAddress
        };

        await _profileRepository.AddMentorProfileAsync(profile);
        return (true, "Mentor profile created successfully.", profile);
    }

    public async Task<(bool success, string message, CompanyProfile? profile)> CreateCompanyProfileAsync(
        int userId, string companyName, string companyAddress, string taxId, string website, List<CompanyContact> contacts)
    {
        var existingProfile = await _profileRepository.GetStudentProfileByUserIdAsync(userId) ??
                             await _profileRepository.GetMentorProfileByUserIdAsync(userId) ??
                             await _profileRepository.GetCompanyProfileByUserIdAsync(userId);
        if (existingProfile != null)
        {
            return (false, "Profile for this user already exists.", null);
        }

        var profile = new CompanyProfile
        {
            UserId = userId,
            CompanyName = companyName,
            CompanyAddress = companyAddress,
            TaxId = taxId,
            Website = website,
            Contacts = contacts
        };

        await _profileRepository.AddCompanyProfileAsync(profile);
        return (true, "Company profile created successfully.", profile);
    }

    public async Task<Profile?> GetProfileByUserIdAsync(int userId)
    {
        var studentProfile = await _profileRepository.GetStudentProfileByUserIdAsync(userId);
        if (studentProfile != null) return studentProfile;

        var mentorProfile = await _profileRepository.GetMentorProfileByUserIdAsync(userId);
        if (mentorProfile != null) return mentorProfile;

        var companyProfile = await _profileRepository.GetCompanyProfileByUserIdAsync(userId);
        return companyProfile;
    }

    public async Task<(bool success, string message, StudentProfile? profile)> UpdateStudentProfileAsync(
        int userId, string firstName, string lastName, string patronymic, int age, string gender,
        string phone, string telegram, decimal rating, string sphereOfDevelopment)
    {
        var profile = await _profileRepository.GetStudentProfileByUserIdAsync(userId);
        if (profile == null)
            return (false, "Student profile not found.", null);

        profile.FirstName = firstName;
        profile.LastName = lastName;
        profile.Patronymic = patronymic;
        profile.Age = age;
        profile.Gender = gender;
        profile.Phone = phone;
        profile.Telegram = telegram;
        profile.Rating = rating;
        profile.SphereOfDevelopment = sphereOfDevelopment;

        await _profileRepository.UpdateStudentProfileAsync(profile);
        return (true, "Student profile updated successfully.", profile);
    }

    public async Task<(bool success, string message, MentorProfile? profile)> UpdateMentorProfileAsync(
        int userId, string firstName, string lastName, string patronymic, int age, string gender,
        string phone, string telegram, string sphereOfDevelopment, string officeAddress)
    {
        var profile = await _profileRepository.GetMentorProfileByUserIdAsync(userId);
        if (profile == null)
            return (false, "Mentor profile not found.", null);

        profile.FirstName = firstName;
        profile.LastName = lastName;
        profile.Patronymic = patronymic;
        profile.Age = age;
        profile.Gender = gender;
        profile.Phone = phone;
        profile.Telegram = telegram;
        profile.SphereOfDevelopment = sphereOfDevelopment;
        profile.OfficeAddress = officeAddress;

        await _profileRepository.UpdateMentorProfileAsync(profile);
        return (true, "Mentor profile updated successfully.", profile);
    }

    public async Task<(bool success, string message, CompanyProfile? profile)> UpdateCompanyProfileAsync(
        int userId, string companyName, string companyAddress, string taxId, string website, List<CompanyContact> contacts)
    {
        var profile = await _profileRepository.GetCompanyProfileByUserIdAsync(userId);
        if (profile == null)
            return (false, "Company profile not found.", null);

        profile.CompanyName = companyName;
        profile.CompanyAddress = companyAddress;
        profile.TaxId = taxId;
        profile.Website = website;
        profile.Contacts = contacts;

        await _profileRepository.UpdateCompanyProfileAsync(profile);
        return (true, "Company profile updated successfully.", profile);
    }

    public async Task<(bool success, string message)> DeleteProfileAsync(int userId)
    {
        var profile = await GetProfileByUserIdAsync(userId);
        if (profile == null)
            return (false, "Profile not found.");

        await _profileRepository.DeleteProfileAsync(userId);
        return (true, "Profile deleted successfully.");
    }
}