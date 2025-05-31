using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Core.Modules.Statistics.Models.Request
{
    public enum ApplicationStatisticsType
    {
        /// <summary>
        /// Home Surrounding Application
        /// </summary>
        HomeSurroundingApplication = 1,
        /// <summary>
        /// Public Surrounding Application
        /// </summary>
        PublicSurroundingApplication = 2,
        /// <summary>
        /// Chief Occupant Registration
        /// </summary>
        ChiefOccupantRegistration = 3,
        /// <summary>
        /// Any
        /// </summary>
        Any = 4,

    }
}
