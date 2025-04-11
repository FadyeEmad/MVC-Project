using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MVC_Project.Models
{
    public class SystemDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = . ; Database = System ; Trusted_Connection= true ; Encrypt =true; TrustServerCertificate= true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Students> Students { get; set; }
        public DbSet<Departments> Departments { get; set; }
    }
}
