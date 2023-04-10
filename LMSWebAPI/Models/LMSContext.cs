using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace LMSWebAPI.Models
{
    public class LMSContext : DbContext
    {
        public LMSContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Course> Courses{ get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Module> Modules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Modules)
                .WithOne(m => m.Course)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Module>()
                .HasMany(m => m.Assignments)
                .WithOne(a => a.Module)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
