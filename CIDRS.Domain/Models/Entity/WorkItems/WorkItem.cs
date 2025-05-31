using CIDRS.Domain.Enums;
using CIDRS.Domain.Models.Entity.Applications;
using CIDRS.Domain.Models.Entity.Base;
using CIDRS.Domain.Models.Entity.ChiefOccupants;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Domain.Models.Entity.WorkItems
{
    public class WorkItem : BaseEntity
    {
        public WorkItemStatus Status { get; set; }
        public WorkItemType Type { get; set; }
        public int? ChiefOccupantId { get; set; }
        public ChiefOccupant ChiefOccupant { get; set; }
        public int? ReportingApplicationId { get; set; }
        public ReportingApplication Application { get; set; }
        public List<WorkItemAction> WorkItemActions { get; set; }
        public List<WorkItemRemark> WorkItemRemarks { get; set; }

        public WorkItem()
        {
            WorkItemActions = new List<WorkItemAction>();
            WorkItemRemarks = new List<WorkItemRemark>();
        }
    }
}
