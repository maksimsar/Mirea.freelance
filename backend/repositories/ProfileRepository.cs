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
        return await _context.Profiles
            .OfType<StudentProfile>()
            .FirstOrDefaultAsync(sp => sp.UserId == userId);
    }

    public async Task<MentorProfile?> GetMentorProfileByUserIdAsync(int userId)
    {
        return await _context.Profiles
            .OfType<MentorProfile>()
            .FirstOrDefaultAsync(mp => mp.UserId == userId);
    }

    public async Task<CompanyProfile?> GetCompanyProfileByUserIdAsync(int userId)
    {
        return await _context.Profiles
            .OfType<CompanyProfile>()
            .Include(cp => cp.Contacts)
            .FirstOrDefaultAsync(cp => cp.UserId == userId);
    }

    public async Task AddStudentProfileAsync(StudentProfile profile)
    {
        _context.Profiles.Add(profile);
        await _context.SaveChangesAsync();
    }

    public async Task AddMentorProfileAsync(MentorProfile profile)
    {
        _context.Profiles.Add(profile);
        await _context.SaveChangesAsync();
    }

    public async Task AddCompanyProfileAsync(CompanyProfile profile)
    {
        _context.Profiles.Add(profile);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateStudentProfileAsync(StudentProfile profile)
    {
        _context.Profiles.Update(profile);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateMentorProfileAsync(MentorProfile profile)
    {
        _context.Profiles.Update(profile);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCompanyProfileAsync(CompanyProfile profile)
    {
        // Удаляем старые контакты и добавляем новые, чтобы синхронизировать коллекцию
        var existingProfile = await _context.Profiles
            .OfType<CompanyProfile>()
            .Include(cp => cp.Contacts)
            .FirstOrDefaultAsync(cp => cp.UserId == profile.UserId);
        if (existingProfile != null)
        {
            _context.CompanyContacts.RemoveRange(existingProfile.Contacts);
            existingProfile.Contacts = profile.Contacts;
            _context.Profiles.Update(existingProfile);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteProfileAsync(int userId)
    {
        var profile = await _context.Profiles
            .FirstOrDefaultAsync(p => p.UserId == userId);
        if (profile != null)
        {
            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();
        }
    }
}