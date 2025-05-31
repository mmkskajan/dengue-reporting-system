using CIDRS.Domain.Enums;
using CIDRS.Domain.Models.Entity.Base;
using CIDRS.Domain.Models.Entity.PublicHealthInspectors;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Domain.Models.Entity.WorkItems
{
    public class WorkItemAction : BaseEntity
    {
        public WorkItemActionType Type { get; set; }
        public int? AssignToId { get; set; }
        public PublicHealthInspector AssignTo { get; set; }
        public int WorkItemId { get; set; }
        public WorkItem WorkItem { get; set; }
    }
}
