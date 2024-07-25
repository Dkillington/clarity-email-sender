using EmailAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace EmailAPI.Data
{
    public class ApplicationDbContext : DbContext // Inherit DbContext class functionality
    {
        // Constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Email> emails { get; set; } // All stored emails
    }
}
