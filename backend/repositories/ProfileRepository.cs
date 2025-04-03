using Microsoft.EntityFrameworkCore;
using Mirea.freelance.backend.data;
using Mirea.freelance.backend.models;

namespace Mirea.freelance.backend.repositories;

public class ProfileRepository : IProfileRepository
{
    private readonly AppDbContext _context;

    public ProfileRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<StudentProfile?> GetStudentProfileByUserIdAsync(int userId)
    {
        return await _context.StudentProfiles.FirstOrDefaultAsync(sp => sp.UserId == userId);
    }

    public async Task<MentorProfile?> GetMentorProfileByUserIdAsync(int userId)
    {
        return await _context.MentorProfiles.FirstOrDefaultAsync(mp => mp.UserId == userId);
    }

    public async Task<CompanyProfile?> GetCompanyProfileByUserIdAsync(int userId)
    {
        return await _context.CompanyProfiles
            .Include(cp => cp.Contacts)
            .FirstOrDefaultAsync(cp => cp.UserId == userId);
    }

    public async Task AddStudentProfileAsync(StudentProfile profile)
    {
        _context.StudentProfiles.Add(profile);
        await _context.SaveChangesAsync();
    }

    public async Task AddMentorProfileAsync(MentorProfile profile)
    {
        _context.MentorProfiles.Add(profile);
        await _context.SaveChangesAsync();
    }

    public async Task AddCompanyProfileAsync(CompanyProfile profile)
    {
        _context.CompanyProfiles.Add(profile);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateStudentProfileAsync(StudentProfile profile)
    {
        _context.StudentProfiles.Update(profile);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateMentorProfileAsync(MentorProfile profile)
    {
        _context.MentorProfiles.Update(profile);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCompanyProfileAsync(CompanyProfile profile)
    {
        // Удаляем старые контакты и добавляем новые, чтобы синхронизировать коллекцию
        var existingProfile = await _context.CompanyProfiles
            .Include(cp => cp.Contacts)
            .FirstOrDefaultAsync(cp => cp.UserId == profile.UserId);
        if (existingProfile != null)
        {
            _context.CompanyContacts.RemoveRange(existingProfile.Contacts);
            existingProfile.Contacts = profile.Contacts;
            _context.CompanyProfiles.Update(existingProfile);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteProfileAsync(int userId)
    {
        var studentProfile = await _context.StudentProfiles.FindAsync(userId);
        if (studentProfile != null)
        {
            _context.StudentProfiles.Remove(studentProfile);
            await _context.SaveChangesAsync();
            return;
        }

        var mentorProfile = await _context.MentorProfiles.FindAsync(userId);
        if (mentorProfile != null)
        {
            _context.MentorProfiles.Remove(mentorProfile);
            await _context.SaveChangesAsync();
            return;
        }

        var companyProfile = await _context.CompanyProfiles
            .Include(cp => cp.Contacts)
            .FirstOrDefaultAsync(cp => cp.UserId == userId);
        if (companyProfile != null)
        {
            _context.CompanyProfiles.Remove(companyProfile);
            await _context.SaveChangesAsync();
        }
    }
}