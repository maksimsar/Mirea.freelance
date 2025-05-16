using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mirea.freelance.backend.models;


namespace Mirea.freelance.backend.data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Profile> Profiles { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Feedback> Feedbacks { get; set; } = null!;
        public DbSet<CompanyContact> CompanyContacts { get; set; } = null!;
        public DbSet<StudentProfile> StudentProfiles { get; set; } = null!;
        public DbSet<MentorProfile> MentorProfiles { get; set; } = null!;
        public DbSet<CompanyProfile> CompanyProfiles { get; set; } = null!;
        public DbSet<UserRole> UserRoles { get; set; } = null!;

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
            base.OnModelCreating(modelBuilder);
            //Настройка сущности User
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.Property(u => u.UserName).HasColumnName("Login").IsRequired();
                entity.Property(u => u.NormalizedUserName).HasColumnName("NormalizedUserName");
                entity.Property(u => u.PasswordHash).IsRequired();
                entity.Property(u => u.RegistrationDate).IsRequired();
                entity.Ignore(u => u.Login);
            });

            //Настройка таблицы Roles
            modelBuilder.Entity<IdentityRole<int>>(entity =>
            {
                entity.ToTable("Roles");
                entity.Property(r => r.Name).IsRequired();
                entity.Property(r => r.NormalizedName).IsRequired();
            });

            // Настройка таблицы UserRoles для ASP.NET Identity
            modelBuilder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.ToTable("IdentityUserRoles"); // Изменено: Переименовал таблицу, чтобы не конфликтовала с твоей UserRole
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });
                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(ur => ur.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne<IdentityRole<int>>()
                    .WithMany()
                    .HasForeignKey(ur => ur.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Настройка твоей кастомной таблицы UserRole
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRoles"); // Без изменений: Твоя таблица UserRole
                entity.HasKey(ur => ur.Id);
                entity.Property(ur => ur.UserId).IsRequired();
                entity.Property(ur => ur.RoleId).IsRequired();
                entity.Property(ur => ur.AssignedDate).IsRequired();
                entity.HasOne(ur => ur.User)
                    .WithMany()
                    .HasForeignKey(ur => ur.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(ur => ur.Role)
                    .WithMany()
                    .HasForeignKey(ur => ur.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Дополнительные таблицы Identity
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");

            //Настройка базовой сущности Profile
            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("Profiles");

                entity.HasKey(p => p.UserId);

                entity.HasOne(p => p.User)
                    .WithOne()
                    .HasForeignKey<Profile>(p => p.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

            });

            //Настройка наследуемой сущности StudentProfile
            modelBuilder.Entity<StudentProfile>(entity =>
            {
                entity.ToTable("StudentProfiles");

                entity.Property(s => s.FirstName).IsRequired();
                entity.Property(s => s.LastName).IsRequired();
                entity.Property(s => s.Patronymic);

                entity.Property(s => s.Age).IsRequired();
                entity.Property(s => s.Gender).IsRequired();
                entity.Property(s => s.Phone).IsRequired();
                entity.Property(s => s.Telegram).IsRequired();
                entity.Property(s => s.Rating).IsRequired();
                entity.Property(s => s.SphereOfDevelopment).IsRequired();
            });

            //Настройка наследуемой сущности MentorProfile
            modelBuilder.Entity<MentorProfile>(entity =>
            {
                entity.ToTable("MentorProfiles");

                entity.Property(m => m.FirstName).IsRequired();
                entity.Property(m => m.LastName).IsRequired();
                entity.Property(m => m.Patronymic);
                entity.Property(m => m.Age).IsRequired();
                entity.Property(m => m.Gender).IsRequired();
                entity.Property(m => m.Phone).IsRequired();
                entity.Property(m => m.Telegram).IsRequired();
                entity.Property(m => m.SphereOfDevelopment).IsRequired();
                entity.Property(m => m.OfficeAddress).IsRequired();
            });

            //Настройка наследуемой сущности CompanyProfile
            modelBuilder.Entity<CompanyProfile>(entity =>
            {
                entity.ToTable("CompanyProfiles");

                entity.Property(cp => cp.CompanyName).IsRequired();
                entity.Property(cp => cp.CompanyAddress).IsRequired();
                entity.Property(cp => cp.TaxId).IsRequired();
                entity.Property(cp => cp.Website);

                entity.HasMany(cp => cp.Contacts)
                    .WithOne(cc => cc.CompanyProfile)
                    .HasForeignKey(cc => cc.CompanyProfileId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            
            //Настройка  сущности CompanyContact
            modelBuilder.Entity<CompanyContact>(entity =>
            {
                entity.ToTable("CompanyContacts");

                entity.HasKey(cc => cc.Id);

                entity.Property(cc => cc.Name).IsRequired();
                entity.Property(cc => cc.Phone).IsRequired();
                entity.Property(cc => cc.Telegram).IsRequired();
                entity.Property(cc => cc.Email);

                entity.HasOne(cc => cc.CompanyProfile)
                    .WithMany(cp => cp.Contacts)
                    .HasForeignKey(cc => cc.CompanyProfileId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
            });
            
            //Настройка сущности Order
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders");

                entity.HasKey(o => o.Id);

                entity.Property(o => o.Title).IsRequired();
                entity.Property(o => o.Description).IsRequired();
                entity.Property(o => o.Status).IsRequired();
                entity.Property(o => o.Budget).IsRequired();
                entity.Property(o => o.CreatedDate).IsRequired();
                entity.Property(o => o.Deadline).IsRequired();

                // Настройка связи "один ко многим": один CompanyProfile имеет много Orders.
                entity.HasOne(o => o.CompanyProfile)
                    .WithMany(cp => cp.Orders)  // Свойство Orders в CompanyProfile
                    .HasForeignKey(o => o.CompanyProfileId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(o => o.FreelancerProfiles)
                    .WithMany(sp => sp.Orders)
                    .UsingEntity<Dictionary<string, object>>(
                        "OrderFreelancer", // имя промежуточной таблицы
                        j => j
                            .HasOne<StudentProfile>()
                            .WithMany()
                            .HasForeignKey("FreelancerProfileId")
                            .IsRequired(false) // делаем FK nullable
                            .OnDelete(DeleteBehavior.SetNull),
                        j => j
                            .HasOne<Order>()
                            .WithMany()
                            .HasForeignKey("OrderId")
                            .OnDelete(DeleteBehavior.Cascade));
            });
            
            //Настройка сущности FeedBack
            modelBuilder.Entity<Feedback>(entity =>
            {
                // Указываем имя таблицы
                entity.ToTable("Feedbacks");

                // Задаём первичный ключ
                entity.HasKey(f => f.Id);

                // Настраиваем свойства
                entity.Property(f => f.OrderId).IsRequired();
                entity.Property(f => f.AuthorProfileId).IsRequired();
                entity.Property(f => f.RecipientProfileId).IsRequired();
                entity.Property(f => f.Rating).IsRequired();
                entity.Property(f => f.Comment)
                    .IsRequired()
                    .HasMaxLength(500);
                entity.Property(f => f.CreatedDate)
                    .IsRequired()
                    .HasColumnType("timestamp without time zone");

                // Настраиваем связь между Feedback и Order
                // Предполагаем, что в Order есть коллекция обратных навигационных свойств (например, Feedbacks)
                entity.HasOne(f => f.Order)
                    .WithMany(o => o.Feedbacks) // добавьте это свойство в Order: public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
                    .HasForeignKey(f => f.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Настраиваем связь между Feedback и AuthorProfile
                // Если в Profile нет коллекционного свойства для authored feedback, используем WithMany() без параметра
                entity.HasOne(f => f.AuthorProfile)
                    .WithMany()
                    .HasForeignKey(f => f.AuthorProfileId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Настраиваем связь между Feedback и RecipientProfile
                entity.HasOne(f => f.RecipientProfile)
                    .WithMany()
                    .HasForeignKey(f => f.RecipientProfileId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

        }
    }
}
