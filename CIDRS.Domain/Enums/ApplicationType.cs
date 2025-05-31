using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CIDRS.Domain.Enums
{
    public enum ApplicationType
    {
        [Description("Base")]
        Base = 1,

        [Description("HomeSurroundingAllerts")]
        HomeSurroundingAllerts = 2,

        [Description("Public Surrounding Complaints")]
        PublicSurroundingComplaints = 3,
        
    }
}
