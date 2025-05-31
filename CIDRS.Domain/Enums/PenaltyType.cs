using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CIDRS.Domain.Enums
{
    public enum PenaltyType
    {
        [Description("Red Notice")]
        RedNotice = 1,

        [Description("Fee")]
        Fee = 2,
    }
}
