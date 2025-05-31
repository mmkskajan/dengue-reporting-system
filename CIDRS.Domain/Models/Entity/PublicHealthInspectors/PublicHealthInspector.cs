using CIDRS.Domain.Models.Entity.Base;
using CIDRS.Domain.Models.Entity.ChiefOccupants;
using CIDRS.Domain.Models.Entity.Masters;
using CIDRS.Domain.Models.Entity.WorkItems;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Domain.Models.Entity.PublicHealthInspectors
{
    public class PublicHealthInspector : BaseEntity
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string IdentityUserId { get; set; }
        public int DistrictId { get; set; }
        public District District { get; set; }
        public int MohAreaId { get; set; }
        public MohArea MohArea { get; set; }
        public List<ChiefOccupant> ChiefOccupants { get; set; }
        public List<WorkItemAction> WorkItemActions { get; set; }
        public PublicHealthInspector()
        {
            ChiefOccupants = new List<ChiefOccupant>();
            WorkItemActions = new List<WorkItemAction>();
        }

    }
}
