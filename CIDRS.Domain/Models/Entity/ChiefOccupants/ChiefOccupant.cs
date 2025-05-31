using CIDRS.Domain.Models.Entity.Applications;
using CIDRS.Domain.Models.Entity.Base;
using CIDRS.Domain.Models.Entity.Masters;
using CIDRS.Domain.Models.Entity.Polices;
using CIDRS.Domain.Models.Entity.PublicHealthInspectors;
using CIDRS.Domain.Models.Entity.WorkItems;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Domain.Models.Entity.ChiefOccupants
{
    public class ChiefOccupant : BaseEntity
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string IdentityUserId { get; set; }
        public string Address { get; set; }
        public int ApplicationRejectedCount { get; set; }
        public int DistrictId { get; set; }
        public District District { get; set; }
        public int MohAreaId { get; set; }
        public MohArea MohArea { get; set; }
        public int? PhiId { get; set; }
        public PublicHealthInspector RespectivePhi { get; set; }
        public int? PoliceId { get; set; }
        public Police RespectivePolice { get; set; }
        public List<ReportingApplication> ReportingApplications { get; set; }
        public WorkItem WorkItem { get; set; }
        public List<Penalty> Penalties { get; set; }
        public ChiefOccupant()
        {
            ReportingApplications = new List<ReportingApplication>();
            Penalties = new List<Penalty>();
        }
    }
}
