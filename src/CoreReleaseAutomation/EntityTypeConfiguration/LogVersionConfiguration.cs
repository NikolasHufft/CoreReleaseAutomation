using CoreReleaseAutomation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreReleaseAutomation.EntityTypeConfiguration
{
    public class LogVersionConfiguration : IEntityTypeConfiguration<LogVersion>
    {
        public void Configure(EntityTypeBuilder<LogVersion> modelBuilder)
        {
            modelBuilder.HasKey(p => new { p.Version, p.Patch });
        }
    }

}
