using CIDRS.Domain.Models.Entity.WorkItems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CIDRS.Infrastructure.Configurations.WorkItems
{
    public class WorkItemRemarkConfiguration : IEntityTypeConfiguration<WorkItemRemark>
    {
        public void Configure(EntityTypeBuilder<WorkItemRemark> builder)
        {
            builder.ToTable("WorkItemRemarks", "wkitm");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.WorkItem).WithMany(x => x.WorkItemRemarks).HasForeignKey(x => x.WorkItemId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
