using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Domain.Models.Entity.Masters.Pivot
{
    public class MohAreaPoliceStation
    {
        public int MohAreaId { get; set; }
        public MohArea MohArea { get; set; }

        public int PoliceStationId { get; set; }
        public PoliceStation PoliceStation { get; set; }
    }
}
