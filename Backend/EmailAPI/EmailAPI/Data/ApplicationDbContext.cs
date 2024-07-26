using EmailAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace EmailAPI.Data
{
    // Class for accessing SQL database
    public class ApplicationDbContext : DbContext // Inherit DbContext class functionality
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Email> Emails { get; set; } // All stored emails
    }
}
