// backend/Data/AppDbContext.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;           // для [MaxLength]
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mirea.freelance.backend.models;

namespace Mirea.freelance.backend.data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // -------------------- DbSets --------------------
        public DbSet<Profile>             Profiles            { get; set; } = null!;
        public DbSet<Order>               Orders              { get; set; } = null!;
        public DbSet<Feedback>            Feedbacks           { get; set; } = null!;
        public DbSet<CompanyContact>      CompanyContacts     { get; set; } = null!;
        public DbSet<StudentProfile>      StudentProfiles     { get; set; } = null!;
        public DbSet<MentorProfile>       MentorProfiles      { get; set; } = null!;
        public DbSet<CompanyProfile>      CompanyProfiles     { get; set; } = null!;
        public DbSet<UserRole>            UserRoles           { get; set; } = null!;

        public DbSet<ProjectStudent>      ProjectStudents     => Set<ProjectStudent>();
        public DbSet<ProjectTask>         ProjectTasks        => Set<ProjectTask>();
        public DbSet<TaskStatusHistory>   TaskStatusHistories => Set<TaskStatusHistory>();

        // -------------------- fallback OnConfiguring --------------------
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        // -------------------- model configuration --------------------
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ---------- User ----------
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.Property(u => u.UserName).HasColumnName("Login").IsRequired();
                entity.Property(u => u.NormalizedUserName).HasColumnName("NormalizedUserName");
                entity.Property(u => u.PasswordHash).IsRequired();
                entity.Property(u => u.RegistrationDate).IsRequired();
                entity.Ignore(u => u.Login);
            });

            // ---------- Identity roles / claims ----------
            modelBuilder.Entity<IdentityRole<int>>(e =>
            {
                e.ToTable("Roles");
                e.Property(r => r.Name).IsRequired();
                e.Property(r => r.NormalizedName).IsRequired();
            });

            modelBuilder.Entity<IdentityUserRole<int>>(e =>
            {
                e.ToTable("IdentityUserRoles");
                e.HasKey(ur => new { ur.UserId, ur.RoleId });
            });

            modelBuilder.Entity<UserRole>(e =>
            {
                e.ToTable("UserRoles");
                e.HasKey(ur => ur.Id);
                e.Property(ur => ur.AssignedDate).IsRequired();
            });

            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");

            // ---------- Profiles ----------
            modelBuilder.Entity<Profile>(e =>
            {
                e.ToTable("Profiles");
                e.HasKey(p => p.UserId);
                e.HasOne(p => p.User)
                    .WithOne()
                    .HasForeignKey<Profile>(p => p.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<StudentProfile>(e =>
            {
                e.ToTable("StudentProfiles");
                e.Property(s => s.FirstName).IsRequired();
                e.Property(s => s.LastName).IsRequired();
                e.Property(s => s.Age).IsRequired();
                e.Property(s => s.Gender).IsRequired();
                e.Property(s => s.Phone).IsRequired();
                e.Property(s => s.Telegram).IsRequired();
                e.Property(s => s.Rating).IsRequired();
                e.Property(s => s.SphereOfDevelopment).IsRequired();
            });

            modelBuilder.Entity<MentorProfile>(e =>
            {
                e.ToTable("MentorProfiles");
                e.Property(m => m.FirstName).IsRequired();
                e.Property(m => m.LastName).IsRequired();
                e.Property(m => m.Age).IsRequired();
                e.Property(m => m.Gender).IsRequired();
                e.Property(m => m.Phone).IsRequired();
                e.Property(m => m.Telegram).IsRequired();
                e.Property(m => m.SphereOfDevelopment).IsRequired();
                e.Property(m => m.OfficeAddress).IsRequired();
            });

            modelBuilder.Entity<CompanyProfile>(e =>
            {
                e.ToTable("CompanyProfiles");
                e.Property(cp => cp.CompanyName).IsRequired();
                e.Property(cp => cp.CompanyAddress).IsRequired();
                e.Property(cp => cp.TaxId).IsRequired();
                e.HasMany(cp => cp.Contacts)
                    .WithOne(cc => cc.CompanyProfile)
                    .HasForeignKey(cc => cc.CompanyProfileId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CompanyContact>(e =>
            {
                e.ToTable("CompanyContacts");
                e.HasKey(cc => cc.Id);
            });

            // ---------- Order (= Project) ----------
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

                entity.HasOne(o => o.CompanyProfile)
                    .WithMany(cp => cp.Orders)
                    .HasForeignKey(o => o.CompanyProfileId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                entity.HasOne(o => o.MentorProfile)
                    .WithMany()                       // нет ICollection<Order> в MentorProfile
                    .HasForeignKey(o => o.MentorProfileId)
                    .OnDelete(DeleteBehavior.SetNull);

                // старая связь many-to-many (FreelancerProfiles) остаётся как была
            });

            // ---------- NEW: many-to-many Order ↔ StudentProfile ----------
            modelBuilder.Entity<ProjectStudent>().HasKey(ps => new { ps.OrderId, ps.StudentId });

            modelBuilder.Entity<ProjectStudent>()
                .HasOne(ps => ps.Order)
                .WithMany(o => o.ProjectStudents)
                .HasForeignKey(ps => ps.OrderId);

            modelBuilder.Entity<ProjectStudent>()
                .HasOne(ps => ps.Student)
                .WithMany(s => s.ProjectStudents)
                .HasForeignKey(ps => ps.StudentId);

            // ---------- NEW: ProjectTask ----------
            modelBuilder.Entity<ProjectTask>(e =>
            {
                e.ToTable("ProjectTasks");
                e.Property(t => t.Title).IsRequired().HasMaxLength(100);
                e.Property(t => t.Description).IsRequired().HasMaxLength(1000);

                e.HasOne(t => t.Order)
                    .WithMany(o => o.ProjectTasks)
                    .HasForeignKey(t => t.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(t => t.Assignee)
                    .WithMany()                         // пока без обратной коллекции
                    .HasForeignKey(t => t.AssigneeStudentId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ---------- NEW: TaskStatusHistory ----------
            modelBuilder.Entity<TaskStatusHistory>(e =>
            {
                e.ToTable("ProjectTaskStatusHistories");
                e.HasOne(h => h.ProjectTask)
                    .WithMany(t => t.StatusHistory)
                    .HasForeignKey(h => h.ProjectTaskId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TaskStatusHistory>(e =>
            {
                e.HasOne(h => h.ChangedByUser)
                .WithMany()
                .HasForeignKey(h => h.ChangedByUserId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            // ---------- Feedback ----------
            modelBuilder.Entity<Feedback>(e =>
            {
                e.ToTable("Feedbacks");
                e.HasKey(f => f.Id);
                e.Property(f => f.Comment).HasMaxLength(500).IsRequired();

                e.HasOne(f => f.Order)
                    .WithMany(o => o.Feedbacks)
                    .HasForeignKey(f => f.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(f => f.AuthorProfile)
                    .WithMany()
                    .HasForeignKey(f => f.AuthorProfileId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(f => f.RecipientProfile)
                    .WithMany()
                    .HasForeignKey(f => f.RecipientProfileId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
