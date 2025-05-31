using CIDRS.Domain.Models.Entity.ChiefOccupants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Infrastructure.Configurations.ChiefOccupants
{
    public class PenaltyConfiguration : IEntityTypeConfiguration<Penalty>
    {
        public void Configure(EntityTypeBuilder<Penalty> builder)
        {
            builder.ToTable("Penalties", "pub");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.ChiefOccupant).WithMany(x => x.Penalties).HasForeignKey(x => x.ChiefOccupantId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
