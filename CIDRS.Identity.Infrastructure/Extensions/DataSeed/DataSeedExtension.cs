using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Identity.Infrastructure.Extensions.DataSeed
{
    /// <summary>
    /// The Class that contains seed methods
    /// </summary>
    public static partial class DataSeedExtension
    {
        /// <summary>
        /// The method to seed data, also it sould be call on "OnModelCreating" method of DbContext
        /// </summary>
        /// <param name="modelBuilder">model builder</param>
        /// <param name="seedMasterOnly">if set false it seed all the sample data that implemented to seed</param>
        public static void Seed(this ModelBuilder modelBuilder, bool seedMasterOnly = true)
        {
            if (seedMasterOnly)
                SeedMasters(modelBuilder);
            else
            {
                SeedMasters(modelBuilder);
                SeedSamples(modelBuilder);
            }

        }

        #region private methods
        /// <summary>
        /// Seed Master Data
        /// </summary>
        /// <param name="modelBuilder"></param>
        private static void SeedMasters(ModelBuilder modelBuilder)
        {
        }

        /// <summary>
        /// Seed Sample Data
        /// </summary>
        /// <param name="modelBuilder"></param>
        private static void SeedSamples(ModelBuilder modelBuilder)
        {
            //TO DO: sample data seed need to be Implemented 
        }
        #endregion

    }
}
