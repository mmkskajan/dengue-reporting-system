using CIDRS.Domain.Models.Entity.Masters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Infrastructure.Configurations.Masters
{
    public class PoliceStationConfiguration : IEntityTypeConfiguration<PoliceStation>
    {
        public void Configure(EntityTypeBuilder<PoliceStation> builder)
        {
            builder.ToTable("PoliceStations", "mas");
            builder.HasKey(x => x.Id);
        }
    }
}
