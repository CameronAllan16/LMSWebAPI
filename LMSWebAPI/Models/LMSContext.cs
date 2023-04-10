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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=mydatabase.db");
        }
    }
}
