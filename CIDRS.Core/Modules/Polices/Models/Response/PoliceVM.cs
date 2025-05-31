using AutoMapper;
using CIDRS.Core.Mappings;
using CIDRS.Core.Modules.Masters.ViewModels;
using CIDRS.Domain.Models.Entity.Polices;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Core.Modules.Polices.Models.Response
{
    public class PoliceVM : IMapFrom<Police>
    {
        public int Id { get; set; }
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
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public PoliceStationVM PoliceStation { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Police, PoliceVM>();
            //    .ForMember(dest => dest.MohAreas,
            //    opt => opt.MapFrom(src => src.MohAreas));
        }
    }
}
