using CIDRS.Domain.Models.Entity.WorkItems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Infrastructure.Configurations.WorkItems
{
    public class WorkItemActionConfiguration : IEntityTypeConfiguration<WorkItemAction>
    {
        public void Configure(EntityTypeBuilder<WorkItemAction> builder)
        {
            builder.ToTable("WorkItemActions", "wkitm");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.AssignTo).WithMany(x => x.WorkItemActions).HasForeignKey(x => x.AssignToId).OnDelete( DeleteBehavior.NoAction);
            builder.HasOne(x => x.WorkItem).WithMany(x => x.WorkItemActions).HasForeignKey(x => x.WorkItemId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
