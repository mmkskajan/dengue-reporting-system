
using CIDRS.Domain.Models.Entity.ChiefOccupants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CIDRS.Infrastructure.Configurations.ChiefOccupants
{
    public class ChiefOccupantConfiguration : IEntityTypeConfiguration<ChiefOccupant>
    {
        public void Configure(EntityTypeBuilder<ChiefOccupant> builder)
        {
            builder.ToTable("ChiefOccupants", "pub");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.District).WithMany(x => x.ChiefOccupants).HasForeignKey(x => x.DistrictId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.MohArea).WithMany(x => x.ChiefOccupants).HasForeignKey(x => x.MohAreaId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.RespectivePhi).WithMany(x => x.ChiefOccupants).HasForeignKey(x => x.PhiId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.RespectivePolice).WithMany(x => x.ChiefOccupants).HasForeignKey(x => x.PoliceId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
