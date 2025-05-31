using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CIDRS.Domain.Enums
{
    public enum ApplicationStatus
    {
        [Description("Pending")]
        Pending = 1,
        [Description("Completed")]
        Completed = 2,
    }
}
