using Mirea.Freelance.backend.data;
using Microsoft.EntityFrameworkCore;

public class ProfileService
{
    private readonly AppDbContext _context;

    public ProfileService(AppDbContext context)
    {
        _context = context;
    }

    // Получение профиля по ID
    public async Task<Profile> GetProfileByIdAsync(int userId)
    {
        return await _context.Profiles.FindAsync(userId);
    }

    // Обновление профиля пользователя
    public async Task<(bool success, string message)> UpdateProfileAsync(int userId, Profile updatedProfile)
    {
        var profile = await _context.Profiles.FindAsync(userId);

        if (profile == null)
        {
            return (false, "Профиль не найден");
        }

        // Обновляем данные профиля
        profile.FirstName = updatedProfile.FirstName ?? profile.FirstName;
        profile.LastName = updatedProfile.LastName ?? profile.LastName;
        profile.Age = updatedProfile.Age > 0 ? updatedProfile.Age : profile.Age;
        profile.Gender = updatedProfile.Gender ?? profile.Gender;
        profile.Rating = updatedProfile.Rating > 0 ? updatedProfile.Rating : profile.Rating;

        await _context.SaveChangesAsync();

        return (true, "Профиль успешно обновлён");
    }
}
