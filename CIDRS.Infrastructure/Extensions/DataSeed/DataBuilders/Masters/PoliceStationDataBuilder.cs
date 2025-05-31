using CIDRS.Domain.Models.Entity.Masters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Infrastructure.Extensions.DataSeed
{
    /// <summary>
    /// The class that contains methods of Nothern Province data seed
    /// </summary>
    public partial class DataSeedExtension
    {
        /// <summary>
        /// Seed Master Country
        /// </summary>
        /// <param name="modelBuilder"></param>
        private static void SeedPoliceStations(ModelBuilder modelBuilder)
        {
            const string Prefix = "PO";
            modelBuilder.Entity<PoliceStation>().HasData(
                 new PoliceStation { Id = 1, Name = "Kankasanthurai", Identifier = Prefix + string.Format("{0:D7}",1), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 2, Name = "Illawali", Identifier = Prefix + string.Format("{0:D7}",2), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 3, Name = "Achchuweli", Identifier = Prefix + string.Format("{0:D7}",3), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 4, Name = "Thelippalay", Identifier = Prefix + string.Format("{0:D7}",4), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 5, Name = "Palali", Identifier = Prefix + string.Format("{0:D7}",5), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 6, Name = "Point Pedro", Identifier = Prefix + string.Format("{0:D7}",6), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 7, Name = "Velvatithurai", Identifier = Prefix + string.Format("{0:D7}",7), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 8, Name = "Nelliadi", Identifier = Prefix + string.Format("{0:D7}",8), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 9, Name = "Jaffna", Identifier = Prefix + string.Format("{0:D7}",9), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 10, Name = "Manippai", Identifier = Prefix + string.Format("{0:D7}",10), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 11, Name = "Chunnakam", Identifier = Prefix + string.Format("{0:D7}",11), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 12, Name = "Kayts", Identifier = Prefix + string.Format("{0:D7}",12), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 13, Name = "Delft", Identifier = Prefix + string.Format("{0:D7}",13), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 14, Name = "Wadukotte", Identifier = Prefix + string.Format("{0:D7}",14), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 15, Name = "Chawakachcheri", Identifier = Prefix + string.Format("{0:D7}",15), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 16, Name = "Kopai", Identifier = Prefix + string.Format("{0:D7}",16), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 17, Name = "Kodikamam", Identifier = Prefix + string.Format("{0:D7}",17), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 18, Name = "Kanagarayankulam", Identifier = Prefix + string.Format("{0:D7}",18), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 19, Name = "Vavuniya", Identifier = Prefix + string.Format("{0:D7}",19), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 20, Name = "Irattaperiyakulam", Identifier = Prefix + string.Format("{0:D7}",20), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 21, Name = "Nedumkenrni", Identifier = Prefix + string.Format("{0:D7}",21), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 22, Name = "Chettikulam", Identifier = Prefix + string.Format("{0:D7}",22), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 23, Name = "Omantha", Identifier = Prefix + string.Format("{0:D7}",23), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 24, Name = "Puliyankulam", Identifier = Prefix + string.Format("{0:D7}",24), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 25, Name = "Parayanakulam", Identifier = Prefix + string.Format("{0:D7}",25), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 26, Name = "Ulukkulam", Identifier = Prefix + string.Format("{0:D7}",26), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 27, Name = "Ma-Maduwa", Identifier = Prefix + string.Format("{0:D7}",27), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 28, Name = "Puwarasamkulam", Identifier = Prefix + string.Format("{0:D7}",28), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 29, Name = "Echchanmkulam", Identifier = Prefix + string.Format("{0:D7}",29), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 30, Name = "Bogaswewa", Identifier = Prefix + string.Format("{0:D7}",30), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 31, Name = "Kilinochchi", Identifier = Prefix + string.Format("{0:D7}",31), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 32, Name = "Palei", Identifier = Prefix + string.Format("{0:D7}",32), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 33, Name = "Punarin", Identifier = Prefix + string.Format("{0:D7}",33), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 34, Name = "Nachchikudha", Identifier = Prefix + string.Format("{0:D7}",34), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 35, Name = "Mulankavil", Identifier = Prefix + string.Format("{0:D7}",35), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 36, Name = "Dharmapuram", Identifier = Prefix + string.Format("{0:D7}",36), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 37, Name = "Akkarayamkulam ", Identifier = Prefix + string.Format("{0:D7}",37), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 38, Name = "Mannar", Identifier = Prefix + string.Format("{0:D7}",38), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 39, Name = "Silawathura", Identifier = Prefix + string.Format("{0:D7}",39), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 40, Name = "Iluppakadawai", Identifier = Prefix + string.Format("{0:D7}",40), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 41, Name = "Wedithalathiu", Identifier = Prefix + string.Format("{0:D7}",41), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 42, Name = "Thalaimannar", Identifier = Prefix + string.Format("{0:D7}",42), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 43, Name = "Murunkan", Identifier = Prefix + string.Format("{0:D7}",43), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 44, Name = "Madu", Identifier = Prefix + string.Format("{0:D7}",44), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 45, Name = "Wankala", Identifier = Prefix + string.Format("{0:D7}",45), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 46, Name = "Pesale", Identifier = Prefix + string.Format("{0:D7}",46), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 47, Name = "Mankulam", Identifier = Prefix + string.Format("{0:D7}",47), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 48, Name = "Mullativu", Identifier = Prefix + string.Format("{0:D7}",48), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 49, Name = "Pudukuduiruppu", Identifier = Prefix + string.Format("{0:D7}",49), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 50, Name = "Muliyaweli", Identifier = Prefix + string.Format("{0:D7}",50), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 51, Name = "Oddusudan", Identifier = Prefix + string.Format("{0:D7}",51), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 52, Name = "Welioya", Identifier = Prefix + string.Format("{0:D7}",52), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) },
                 new PoliceStation { Id = 53, Name = "Mallavi", Identifier = Prefix + string.Format("{0:D7}",53), CreatedAt = new DateTime(2021,5,3), UpdatedAt = new DateTime(2021, 5, 3) }
                
                 
            );
        }
    }
}
