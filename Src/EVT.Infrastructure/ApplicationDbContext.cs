using EVR.Application;
using EVR.Domain;
using Microsoft.EntityFrameworkCore;

namespace EVT.Infrastructure
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
    {
        public DbSet<Employee> Employee { get; set; }
        public DbSet<VacationPackage> VacationPackage { get; set; }
        public DbSet<Vacation> Vacation { get; set; }
        public DbSet<Team> Team { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Superior)
                .WithMany()
                .HasForeignKey(e => e.SuperiorId);
        }
    }
}
