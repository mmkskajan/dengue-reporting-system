using CIDRS.Domain.Enums;
using CIDRS.Domain.Models.Entity.Base;
using CIDRS.Domain.Models.Entity.ChiefOccupants;
using CIDRS.Domain.Models.Entity.WorkItems;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Domain.Models.Entity.Applications
{
    public class ReportingApplication : BaseEntity
    {
        public int ChiefOccupantId { get; set; }
        public ChiefOccupant ChiefOccupant { get; set; }
        public ApplicationType Type { get; set; }
        public List<SurroundingSet> SurroundingSets { get; set; }
        public ApplicationStatus Status { get; set; }
        public WorkItem WorkItem { get; set; }

    }
}
