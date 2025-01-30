using CoreReleaseAutomation.EntityTypeConfiguration;
using CoreReleaseAutomation.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreReleaseAutomation.Data
{
    public class ApplicationDataContext : DbContext
    {
        public DbSet<LogVersion> LogVersions { get; set; }
        public DbSet<Release> Releases { get; set; }

        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base (options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLoggerFactory();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("INTEFLOW");

            modelBuilder.ApplyConfiguration(new LogVersionConfiguration());

            modelBuilder.ApplyConfiguration(new ReleaseConfiguration());
        }
    }
}
