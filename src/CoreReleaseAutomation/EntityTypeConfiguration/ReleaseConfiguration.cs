using CoreReleaseAutomation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreReleaseAutomation.EntityTypeConfiguration
{
    public class ReleaseConfiguration : IEntityTypeConfiguration<Release>
    {
        public void Configure(EntityTypeBuilder<Release> modelBuilder)
        {
            modelBuilder.HasKey(p => new { p.ReleaseId });
        }
    }
}