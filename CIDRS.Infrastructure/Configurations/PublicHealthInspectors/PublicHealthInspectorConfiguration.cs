using CIDRS.Domain.Models.Entity.PublicHealthInspectors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Infrastructure.Configurations.PublicHealthInspectors
{
    public class PublicHealthInspectorConfiguration : IEntityTypeConfiguration<PublicHealthInspector>
    {
        public void Configure(EntityTypeBuilder<PublicHealthInspector> builder)
        {
            builder.ToTable("PublicHealthInspectors", "moh");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.District).WithMany(x => x.PublicHealthInspectors).HasForeignKey(x => x.DistrictId);
            builder.HasOne(x => x.MohArea).WithMany(x => x.PublicHealthInspectors).HasForeignKey(x => x.MohAreaId);
        }
    }
}
