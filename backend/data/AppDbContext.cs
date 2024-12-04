using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Host=localhost;Port=5432;Database=freelance;Username=yourusername;Password=yourpassword";
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Настройки сущностей
        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(f => f.Id);
            entity.Property(f => f.Rating).HasPrecision(3, 2);
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(p => p.UserId);
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasOne(t => t.ClientProfile)
                .WithMany()
                .HasForeignKey(t => t.ClientProfileId);

            entity.HasOne(t => t.FreelancerProfile)
                .WithMany()
                .HasForeignKey(t => t.FreelancerProfileId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
