using AutoMapper;
using CIDRS.Core.Common.Models;
using CIDRS.Core.Mappings;
using CIDRS.Core.Modules.Masters.ViewModels;
using CIDRS.Domain.Models.Entity.PublicHealthInspectors;
using CIDRS.Identity.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Core.Modules.PublicHealthInspectors.ViewModels
{
    public class PublicHealthInspectorVM : IMapFrom<PublicHealthInspector>
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

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PublicHealthInspector, PublicHealthInspectorVM>();
            //    .ForMember(dest => dest.MohAreas,
            //    opt => opt.MapFrom(src => src.MohAreas));
        }
    }
}
