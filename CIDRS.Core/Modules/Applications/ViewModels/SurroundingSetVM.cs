using AutoMapper;
using CIDRS.Core.Mappings;
using CIDRS.Domain.Models.Entity.Applications;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Core.Modules.Applications.ViewModels
{
    public class SurroundingSetVM :IMapFrom<SurroundingSet>
    {
        /// <summary>
        /// Auto generated and auto increment Entity Id
        /// </summary>
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
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public SurroundingSetVM RelativeSurroundingSet { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<SurroundingSet, SurroundingSetVM>();
        }
    }
}
