using CIDRS.Identity.Domain.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Identity.Infrastructure.Configurations
{
    /// <summary>
    ///  The class that contains configure method Application User
    /// </summary>
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        /// <summary>
        /// The method configure Application User
        /// </summary>
        /// <param name="builder">builder</param>
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("ApplicationUsers", "identity"); // Table name ApplicationUsers

            builder.HasKey(s => s.Id);

        }
    }
}
