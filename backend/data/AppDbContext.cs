using Microsoft.EntityFrameworkCore;


namespace Mirea.Freelance.backend.data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Host=81.177.136.21;Port=5432;Database=mireafreelance;Username=student8;Password=student11122024";
            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("user");  // Указываем, что таблица будет называться 'user'
                
            modelBuilder.Entity<Profile>()
                .ToTable("profile");  // Указываем, что таблица будет называться 'profile'

            modelBuilder.Entity<Profile>()
                .HasKey(p => p.userid);
              // Определяем поведение при удалении (например, удалить профиль, если удаляется пользователь)

            // Дополнительные настройки сущностей (если нужно)
        }

    }
}
