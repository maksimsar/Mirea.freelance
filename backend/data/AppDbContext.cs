using Microsoft.EntityFrameworkCore;
using Mirea.freelance.backend.models;


namespace Mirea.freelance.backend.data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        
        public DbSet<Profile> Profiles { get; set; } = null!;
        
        public DbSet<Role> Roles { get; set; } = null!;
        
        public DbSet<UserRole> UserRoles { get; set; } = null!;
        
        public DbSet<Order> Orders { get; set; } = null!;
        
        public DbSet<Feedback> Feedbacks { get; set; } = null!;
        
        public DbSet<CompanyContact>  CompanyContacts { get; set; } = null!;
        
        // OnConfiguring оставлен как запасной вариант, если DI не настроит опции (например, при выполнении миграций)
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Создаём конфигурацию вручную из appsettings.json, который находится в корне приложения
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                // Читаем строку подключения по ключу "DefaultConnection"
                var connectionString = configuration.GetConnectionString("DefaultConnection");

                // Настраиваем подключение к PostgreSQL
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

    }
}
