using CIDRS.Identity.Domain.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Identity.Infrastructure.Configurations
{
    /// <summary>
    /// The class that contains configure method Refresh Token
    /// </summary>
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        /// <summary>
        ///  The method configure Refresh Token
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens", "identity"); // Table Name RefreshTokens

            builder.HasKey(s => s.Token);

            builder.HasOne(rt => rt.User)
                .WithMany(u => u.RefreshTokens).HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
