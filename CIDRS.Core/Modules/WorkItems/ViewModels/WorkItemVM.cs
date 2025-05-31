using AutoMapper;
using CIDRS.Core.Mappings;
using CIDRS.Core.Modules.Applications.ViewModels;
using ChiefOccupantDetailVM =CIDRS.Core.Modules.ChiefOccupants.ViewModels.ChiefOccupantVM;
using CIDRS.Domain.Enums;
using CIDRS.Domain.Models.Entity.WorkItems;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Core.Modules.WorkItems.ViewModels
{
    public class WorkItemVM : IMapFrom<WorkItem>
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

        public WorkItemStatus Status { get; set; }
        public WorkItemType Type { get; set; }
        public ChiefOccupantDetailVM ChiefOccupant { get; set; }
        public ReportingApplicationVM Application { get; set; }
        public List<WorkItemActionVM> WorkItemActions { get; set; }
        public List<WorkItemRemarkVM> WorkItemRemarks { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<WorkItem, WorkItemVM>();
              
        }
    }
}
