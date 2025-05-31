using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CIDRS.Domain.Enums
{
    public enum WorkItemStatus
    {
        [Description("Active")]
        Active = 1,

        [Description("Rejected")]
        Rejected = 2,

        [Description("Approved")]
        Approved = 3,

        [Description("Abandoned")]
        Abandoned = 4,
    }
}
