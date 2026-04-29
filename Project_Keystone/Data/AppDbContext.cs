using Microsoft.EntityFrameworkCore;
using Project_Keystone.Models;

namespace Project_Keystone.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<Text> Texts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.UserRole)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProjectTask>()
                .HasOne(pt => pt.Priority)
                .WithMany(p => p.Tasks)
                .HasForeignKey(pt => pt.TaskPriority)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProjectTask>()
                .HasOne(pt => pt.Status)
                .WithMany(s => s.Tasks)
                .HasForeignKey(pt => pt.TaskStatus)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProjectTask>()
                .HasOne(pt => pt.Project)
                .WithMany(p => p.ProjectTasks)
                .HasForeignKey(pt => pt.TaskProjectID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.OwnerUser)
                .WithMany(u => u.OwnedProjects)
                .HasForeignKey(p => p.ProjectOwnerUserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserSkill>()
                .HasKey(us => new { us.UserID, us.SkillID });

            modelBuilder.Entity<UserSkill>()
                .HasOne(us => us.User)
                .WithMany(u => u.UserSkills)
                .HasForeignKey(us => us.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserSkill>()
                .HasOne(us => us.Skill)
                .WithMany(s => s.UserSkills)
                .HasForeignKey(us => us.SkillID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Text>()
                .HasOne(t => t.User)
                .WithMany(u => u.Texts)
                .HasForeignKey(t => t.UserID)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
