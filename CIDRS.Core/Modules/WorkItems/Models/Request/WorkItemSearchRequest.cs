using CIDRS.Domain.Enums;
using CIDRS.Shared.Common.Pagination.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Core.Modules.WorkItems.Models.Request
{
    public class WorkItemSearchRequest
    {
        public bool? IsActive { get; set; }
        public WorkItemType? Type { get; set; }
        public int? AsigneeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public PaginationOption PaginationOption { get; set; }
    }
    
}
