using CIDRS.Domain.Models.Entity.Masters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Infrastructure.Configurations.Masters
{
    public class MohAreaConfiguration : IEntityTypeConfiguration<MohArea>
    {
        public void Configure(EntityTypeBuilder<MohArea> builder)
        {
            builder.ToTable("MohAreas", "mas");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.District).WithMany(x => x.MohAreas).HasForeignKey(x => x.DistrictId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
