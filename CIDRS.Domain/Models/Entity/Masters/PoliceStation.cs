using CIDRS.Domain.Models.Entity.Base;
using CIDRS.Domain.Models.Entity.Masters.Pivot;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Domain.Models.Entity.Masters
{
    public class PoliceStation : BaseEntity
    {
        public string Name { get; set; }

        public List<MohAreaPoliceStation> MohAreaPoliceStations { get; set; }

        public PoliceStation()
        {
            MohAreaPoliceStations = new List<MohAreaPoliceStation>();
        }
    }
}
