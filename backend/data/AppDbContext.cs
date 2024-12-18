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
            
            modelBuilder.Entity<Task>()
                .ToTable("task") // Устанавливаем имя таблицы
                .HasKey(t => t.Id); // Устанавливаем первичный ключ
            
            modelBuilder.Entity<Task>()
                .HasOne(t => t.ClientProfile)
                .WithMany()
                .HasForeignKey(t => t.ClientProfileId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(); // Удаление задачи при удалении профиля клиента

            // Связь Task с FreelancerProfile
            modelBuilder.Entity<Task>()
                .HasOne(t => t.FreelancerProfile)
                .WithMany()
                .HasForeignKey(t => t.FreelancerProfileId)
                .OnDelete(DeleteBehavior.SetNull); 
            // Определяем поведение при удалении (например, удалить профиль, если удаляется пользователь)
            modelBuilder.Entity<Role>()
                .ToTable("role")
                .HasKey(r => r.Id); // Устанавливаем первичный ключ
            
            
            // Дополнительные настройки сущностей (если нужно)
        }

    }
}
