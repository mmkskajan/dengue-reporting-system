using CIDRS.Domain.Models.Entity.Applications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Infrastructure.Configurations.Applications
{
    public class ReportingApplicationConfiguration : IEntityTypeConfiguration<ReportingApplication>
    {
        public void Configure(EntityTypeBuilder<ReportingApplication> builder)
        {
            builder.ToTable("ReportingApplications","app");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.ChiefOccupant).WithMany(x => x.ReportingApplications)
                    .HasForeignKey(x => x.ChiefOccupantId).OnDelete(DeleteBehavior.NoAction);


        }
    }
}
