using AutoMapper;
using CIDRS.Core.Mappings;
using CIDRS.Domain.Enums;
using CIDRS.Domain.Models.Entity.Applications;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Core.Modules.Applications.ViewModels
{
    public class ReportingApplicationVM : IMapFrom<ReportingApplication>
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

        public ApplicationType Type { get; set; }
        public List<SurroundingSetVM> SurroundingSets { get; set; }
        public ApplicationStatus Status { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<ReportingApplication, ReportingApplicationVM>();
               
        }
    }
}
