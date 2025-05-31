using CIDRS.Domain.Models.Entity.Polices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Infrastructure.Configurations.Polices
{
    public class PoliceConfiguration : IEntityTypeConfiguration<Police>
    {
        public void Configure(EntityTypeBuilder<Police> builder)
        {
            builder.ToTable("Polices", "pub");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.PoliceStation).WithMany().HasForeignKey(x => x.PoliceStationId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
