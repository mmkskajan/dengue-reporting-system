using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CIDRS.Domain.Enums
{
    public enum PenaltyStatus
    {
        [Description("Active")]
        Active = 1,

        [Description("Resolved")]
        Resolved = 2,
    }
}
