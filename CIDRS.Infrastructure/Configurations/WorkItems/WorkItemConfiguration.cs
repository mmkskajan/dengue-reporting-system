using CIDRS.Domain.Models.Entity.WorkItems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Infrastructure.Configurations.WorkItems
{
    public class WorkItemConfiguration : IEntityTypeConfiguration<WorkItem>
    {
        public void Configure(EntityTypeBuilder<WorkItem> builder)
        {
            builder.ToTable("WorkItems", "wkitm");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.ChiefOccupant).WithOne(x => x.WorkItem).HasForeignKey<WorkItem>(x => x.ChiefOccupantId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Application).WithOne(x => x.WorkItem).HasForeignKey<WorkItem>(x => x.ReportingApplicationId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
