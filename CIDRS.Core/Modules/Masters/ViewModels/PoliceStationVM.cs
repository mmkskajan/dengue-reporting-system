using AutoMapper;
using CIDRS.Core.Mappings;
using CIDRS.Domain.Models.Entity.Masters;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Core.Modules.Masters.ViewModels
{
    public class PoliceStationVM : IMapFrom<PoliceStation>
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
        /// <summary>
        /// Police Station Name
        /// </summary>
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PoliceStation, PoliceStationVM>();            
        }
    }
}
