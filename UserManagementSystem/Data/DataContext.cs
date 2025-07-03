using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using UserManagementSystem.Models.CoursesModel;
using UserManagementSystem.Models.UserCourseModel;
using UserManagementSystem.Models.UserModel;

namespace UserManagementSystem.Data
{
    /// <summary>
    /// Class for Database operations.
    /// </summary>
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        // Making on Many to Many Relations.
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // A unique constraint for course name.
            builder.Entity<Course>()
            .HasIndex(c => c.CourseName)
            .IsUnique();

            // To make the course Id auto increment.
            builder.Entity<Course>()
             .Property(c => c.Id)
            .ValueGeneratedOnAdd();

            // UserRole -> User
            builder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            // UserRole -> Role
            builder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);
            
            // Added Composite Key for User Course
            builder.Entity<UserCourse>()
          .HasKey(uc => new { uc.UserId, uc.CourseId });

            // UserCourse -> User
            builder.Entity<UserCourse>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserCourses)
                .HasForeignKey(uc => uc.UserId);

            // UserCourse -> Course
            builder.Entity<UserCourse>()
                .HasOne(uc => uc.Course)
                .WithMany(c => c.UserCourses)
                .HasForeignKey(uc => uc.CourseId);
        }
    }

}
