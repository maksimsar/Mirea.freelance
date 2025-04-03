using Mirea.freelance.backend.models;

namespace Mirea.freelance.backend.repositories;

public interface IProfileRepository
{
    Task<StudentProfile?> GetStudentProfileByUserIdAsync(int userId);
    Task<MentorProfile?> GetMentorProfileByUserIdAsync(int userId);
    Task<CompanyProfile?> GetCompanyProfileByUserIdAsync(int userId);
    Task AddStudentProfileAsync(StudentProfile profile);
    Task AddMentorProfileAsync(MentorProfile profile);
    Task AddCompanyProfileAsync(CompanyProfile profile);
    Task UpdateStudentProfileAsync(StudentProfile profile);
    Task UpdateMentorProfileAsync(MentorProfile profile);
    Task UpdateCompanyProfileAsync(CompanyProfile profile);
    Task DeleteProfileAsync(int userId);
}