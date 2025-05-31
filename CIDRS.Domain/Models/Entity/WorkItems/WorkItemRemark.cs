using CIDRS.Domain.Models.Entity.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Domain.Models.Entity.WorkItems
{
    public class WorkItemRemark : BaseEntity
    {
        public int WorkItemId { get; set; }
        public WorkItem WorkItem { get; set; }
        public string OwnerName { get; set; }
        public string Remark { get; set; }
    }
}
