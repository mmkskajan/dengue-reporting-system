using CIDRS.Domain.Models.Entity.Masters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Infrastructure.Extensions.DataSeed
{
    /// <summary>
    /// The class that contains methods of Country data seed
    /// </summary>
    public partial class DataSeedExtension
    {
        /// <summary>
        /// Seed Master Country
        /// </summary>
        /// <param name="modelBuilder"></param>
        private static void SeedDistricts(ModelBuilder modelBuilder)
        {
            const string Prefix = "D";
            modelBuilder.Entity<District>().HasData(
                 new District { Id = 1, Name = "Ampara", Identifier = Prefix + string.Format("{0:D7}", 1), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021,5,3) },
                 new District { Id = 2, Name = "Anuradhapura", Identifier = Prefix + string.Format("{0:D7}", 2), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021,5,3) },
                 new District { Id = 3, Name = "Badulla", Identifier = Prefix + string.Format("{0:D7}", 3), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021,5,3) },
                 new District { Id = 4, Name = "Batticaloa", Identifier = Prefix + string.Format("{0:D7}", 4), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021,5,3) },
                 new District { Id = 5, Name = "Colombo", Identifier = Prefix + string.Format("{0:D7}", 5), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021,5,3) },
                 new District { Id = 6, Name = "Galle", Identifier = Prefix + string.Format("{0:D7}", 6), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021,5,3) },
                 new District { Id = 7, Name = "Gampaha", Identifier = Prefix + string.Format("{0:D7}", 7), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021,5,3) },
                 new District { Id = 8, Name = "Hambantota", Identifier = Prefix + string.Format("{0:D7}", 8), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021,5,3) },
                 new District { Id = 9, Name = "Jaffna", Identifier = Prefix + string.Format("{0:D7}", 9), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021,5,3) },
                 new District { Id = 10, Name = "Kalutara", Identifier = Prefix + string.Format("{0:D7}",10), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021,5,3) },
                 new District { Id = 11, Name = "Kandy", Identifier = Prefix + string.Format("{0:D7}",11), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021,5,3) },
                 new District { Id = 12, Name = "Kegalle", Identifier = Prefix + string.Format("{0:D7}",12), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021,5,3) },
                 new District { Id = 13, Name = "Kilinochchi", Identifier = Prefix + string.Format("{0:D7}",13), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021,5,3) },
                 new District { Id = 14, Name = "Kurunegala", Identifier = Prefix + string.Format("{0:D7}",14), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021,5,3) },
                 new District { Id = 15, Name = "Mannar", Identifier = Prefix + string.Format("{0:D7}",15), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021,5,3) },
                 new District { Id = 16, Name = "Matale", Identifier = Prefix + string.Format("{0:D7}",16), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021,5,3) },
                 new District { Id = 17, Name = "Matara", Identifier = Prefix + string.Format("{0:D7}",17), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021,5,3) },
                 new District { Id = 18, Name = "Monaragala", Identifier = Prefix + string.Format("{0:D7}",18), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021,5,3) },
                 new District { Id = 19, Name = "Mullaitivu", Identifier = Prefix + string.Format("{0:D7}",19), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021,5,3) },
                 new District { Id = 20, Name = "Nuwara Eliya	", Identifier = Prefix + string.Format("{0:D7}",20), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021,5,3) },
                 new District { Id = 21, Name = "Polonnaruwa", Identifier = Prefix + string.Format("{0:D7}",21), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021,5,3) },
                 new District { Id = 22, Name = "Puttalam", Identifier = Prefix + string.Format("{0:D7}",22), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021,5,3) },
                 new District { Id = 23, Name = "Ratnapura", Identifier = Prefix + string.Format("{0:D7}",23), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021,5,3) },
                 new District { Id = 24, Name = "Trincomalee", Identifier = Prefix + string.Format("{0:D7}",24), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021,5,3) },
                 new District { Id = 25, Name = "Vavuniya", Identifier = Prefix + string.Format("{0:D7}",25), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021,5,3) }
            );
        }
    }
}
