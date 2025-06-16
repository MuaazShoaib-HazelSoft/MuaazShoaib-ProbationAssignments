using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using UserManagementSystem.Models;

namespace UserManagementSystem.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
         
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
               entity.HasIndex(e => e.Email).IsUnique();
            });
        }
    }
}
