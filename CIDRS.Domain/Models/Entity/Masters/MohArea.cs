using CIDRS.Domain.Models.Entity.Base;
using CIDRS.Domain.Models.Entity.ChiefOccupants;
using CIDRS.Domain.Models.Entity.Masters.Pivot;
using CIDRS.Domain.Models.Entity.PublicHealthInspectors;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Domain.Models.Entity.Masters
{
    public class MohArea : BaseEntity
    {
        public int DistrictId { get; set; }
        public District District { get; set; }
        public string Name { get; set; }
        public List<MohAreaPoliceStation> MohAreaPoliceStations { get; set; }
        public List<PublicHealthInspector> PublicHealthInspectors { get; set; }
        public List<ChiefOccupant> ChiefOccupants { get; set; }
        public MohArea()
        {
            MohAreaPoliceStations = new List<MohAreaPoliceStation>();
            PublicHealthInspectors = new List<PublicHealthInspector>();
            ChiefOccupants = new List<ChiefOccupant>();
        }

    }
}
