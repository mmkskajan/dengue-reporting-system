using AutoMapper;
using CIDRS.Core.Mappings;
using CIDRS.Domain.Models.Entity.WorkItems;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Core.Modules.WorkItems.ViewModels
{
    public class WorkItemRemarkVM : IMapFrom<WorkItemRemark>
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

        public string OwnerName { get; set; }
        public string Remark { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<WorkItemRemark, WorkItemRemarkVM>();
        }
    }
}
