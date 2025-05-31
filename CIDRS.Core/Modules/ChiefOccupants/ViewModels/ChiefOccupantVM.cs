using AutoMapper;
using CIDRS.Core.Common.Models;
using CIDRS.Core.Mappings;
using CIDRS.Core.Modules.Applications.ViewModels;
using CIDRS.Core.Modules.Masters.ViewModels;
using CIDRS.Core.Modules.Polices.Models.Response;
using CIDRS.Core.Modules.PublicHealthInspectors.ViewModels;
using CIDRS.Domain.Models.Entity.ChiefOccupants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CIDRS.Core.Modules.ChiefOccupants.ViewModels
{
    public class ChiefOccupantVM : IMapFrom<ChiefOccupant>
    {
        public DistrictVM District { get; set; }
        public MohAreaVM MohArea { get; set; }

        public int Id { get; set; }
        /// <summary>
        /// Identifier
        /// </summary>
        public string Identifier { get; set; }
        /// <summary>
        /// Created Date and Time
        /// </summary>
        public Nullable<DateTime> CreatedAt { get; set; }
        /// <summary>
        /// Updated Date and Time
        /// </summary>
        public Nullable<DateTime> UpdatedAt { get; set; }
        /// <summary>
        /// Deleted Date and Time
        /// </summary>
        public Nullable<DateTime> ArchivedAt { get; set; }
        /// <summary>
        /// Full Name
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Phone Number
        /// </summary>
        public string PhoneNumber { get; set; }

        public UserVM User { get; set; }

        public string Address { get; set; }

        public PublicHealthInspectorVM RespectivePhi { get; set; }
        public PoliceVM RespectivePolice { get; set; }
        public List<ReportingApplicationVM> ReportingApplications { get; set; }
        public List<PenaltyVM> Penalties { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<ChiefOccupant, ChiefOccupantVM>();
        }
    }
}
