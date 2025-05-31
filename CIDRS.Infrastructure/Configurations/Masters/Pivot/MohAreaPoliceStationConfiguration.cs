using CIDRS.Domain.Models.Entity.Masters.Pivot;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Infrastructure.Configurations.Masters.Pivot
{
    public class MohAreaPoliceStationConfiguration : IEntityTypeConfiguration<MohAreaPoliceStation>
    {
        public void Configure(EntityTypeBuilder<MohAreaPoliceStation> builder)
        {
            builder.ToTable("MohAreaPoliceStations", "mas");
            builder.HasKey(x => new { x.MohAreaId, x.PoliceStationId });

            builder.HasOne(x => x.MohArea).WithMany(x => x.MohAreaPoliceStations).HasForeignKey(x => x.MohAreaId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.PoliceStation).WithMany(x => x.MohAreaPoliceStations).HasForeignKey(x => x.PoliceStationId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
