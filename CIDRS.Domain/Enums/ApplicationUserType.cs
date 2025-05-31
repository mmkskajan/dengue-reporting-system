using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CIDRS.Domain.Enums
{
    public enum ApplicationUserType
    {
        [Description("Admin")]
        Admin = 1,

        [Description("PHI")]
        Phi = 2,

        [Description("Chief Occupant")]
        ChiefOccupant = 3,

        [Description("Other")]
        Default = 4,
    }
}
