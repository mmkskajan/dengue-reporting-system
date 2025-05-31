using CIDRS.Domain.Models.Entity.Base;
using CIDRS.Domain.Models.Entity.ChiefOccupants;
using CIDRS.Domain.Models.Entity.Masters;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Domain.Models.Entity.Polices
{
    public class Police : BaseEntity
    {
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public PoliceStation PoliceStation { get; set; }
        public int PoliceStationId { get; set; }
        public List<ChiefOccupant> ChiefOccupants { get; set; }

        public Police()
        {
            ChiefOccupants = new List<ChiefOccupant>();
        }
    }
}
