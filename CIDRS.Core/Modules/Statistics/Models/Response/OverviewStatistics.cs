using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Core.Modules.Statistics.Models.Response
{
    public class OverviewStatistics
    {
        public int HomeSurroundingAlerts { get; set; }
        public int PublicSurroundingComplaints { get; set; }
        public int RegisteredChiefOccupants { get; set; }
    }
}
