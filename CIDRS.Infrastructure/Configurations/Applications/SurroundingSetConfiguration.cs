using CIDRS.Domain.Models.Entity.Applications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Infrastructure.Configurations.Applications
{
    public class SurroundingSetConfiguration : IEntityTypeConfiguration<SurroundingSet>
    {
        public void Configure(EntityTypeBuilder<SurroundingSet> builder)
        {
            builder.ToTable("SurroundingSets", "app");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Application).WithMany(x => x.SurroundingSets)
                   .HasForeignKey(x => x.ReportingApplicationId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.RelativeSurroundingSet).WithOne()
                   .HasForeignKey<SurroundingSet>(x => x.RelativeId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
