using CIDRS.Domain.Models.Entity.Masters.Pivot;
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
        /// Seed MohAreaPoliceStation
        /// </summary>
        /// <param name="modelBuilder"></param>
        private static void SeedMohAreaPoliceStation(ModelBuilder modelBuilder)
        {
            const string Prefix = "D";
            modelBuilder.Entity<MohAreaPoliceStation>().HasData(
                 new MohAreaPoliceStation {  PoliceStationId=1, MohAreaId=116 },
                 new MohAreaPoliceStation {  PoliceStationId=1, MohAreaId=114 },
                 new MohAreaPoliceStation {  PoliceStationId=1, MohAreaId=109 },
                 new MohAreaPoliceStation {  PoliceStationId=2, MohAreaId=116 },
                 new MohAreaPoliceStation {  PoliceStationId=2, MohAreaId=115 },
                 new MohAreaPoliceStation {  PoliceStationId=3, MohAreaId=112 },
                 new MohAreaPoliceStation {  PoliceStationId=3, MohAreaId=109},
                 new MohAreaPoliceStation {  PoliceStationId=3, MohAreaId=110},
                 new MohAreaPoliceStation {  PoliceStationId=3, MohAreaId=114},
                 new MohAreaPoliceStation {  PoliceStationId=4, MohAreaId=115},
                 new MohAreaPoliceStation {  PoliceStationId=4, MohAreaId=116},
                 new MohAreaPoliceStation {  PoliceStationId=5, MohAreaId=114},
                 new MohAreaPoliceStation {  PoliceStationId=6, MohAreaId=114},
                 new MohAreaPoliceStation {  PoliceStationId=6, MohAreaId=110},
                 new MohAreaPoliceStation {  PoliceStationId=7, MohAreaId=114},
                 new MohAreaPoliceStation {  PoliceStationId=8, MohAreaId=110},
                 new MohAreaPoliceStation {  PoliceStationId=8, MohAreaId=114},
                 new MohAreaPoliceStation {  PoliceStationId=9, MohAreaId=109},
                 new MohAreaPoliceStation {  PoliceStationId=9, MohAreaId=112},
                 new MohAreaPoliceStation {  PoliceStationId=9, MohAreaId=113},
                 new MohAreaPoliceStation {  PoliceStationId=10, MohAreaId=115},
                 new MohAreaPoliceStation {  PoliceStationId=10, MohAreaId=116},
                 new MohAreaPoliceStation {  PoliceStationId=11, MohAreaId=109},
                 new MohAreaPoliceStation {  PoliceStationId=11, MohAreaId=112},
                 new MohAreaPoliceStation {  PoliceStationId=11, MohAreaId=116},
                 new MohAreaPoliceStation {  PoliceStationId=12, MohAreaId=111},
                 new MohAreaPoliceStation {  PoliceStationId=13, MohAreaId=111},
                 new MohAreaPoliceStation {  PoliceStationId=14, MohAreaId=107},
                 new MohAreaPoliceStation {  PoliceStationId=14, MohAreaId=115},
                 new MohAreaPoliceStation {  PoliceStationId=15, MohAreaId=108},
                 new MohAreaPoliceStation {  PoliceStationId=15, MohAreaId=112},
                 new MohAreaPoliceStation {  PoliceStationId=15, MohAreaId=114},
                 new MohAreaPoliceStation {  PoliceStationId=16, MohAreaId=112},
                 new MohAreaPoliceStation {  PoliceStationId=16, MohAreaId=113},
                 new MohAreaPoliceStation {  PoliceStationId=16, MohAreaId=109},
                 new MohAreaPoliceStation {  PoliceStationId=17, MohAreaId=108},
                 new MohAreaPoliceStation {  PoliceStationId=17, MohAreaId=114}

                
                 
            );
        }
    }
}