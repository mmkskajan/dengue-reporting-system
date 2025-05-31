using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CIDRS.Domain.Enums
{
    public enum WorkItemActionType
    {
        [Description("New")]
        New = 1,

        [Description("Reject")]
        Reject = 2,

        [Description("Approve")]
        Approve = 3,

        [Description("Abandon")]
        Abandon = 4,

        [Description("Assign")]
        Assign = 5,

    }
}
