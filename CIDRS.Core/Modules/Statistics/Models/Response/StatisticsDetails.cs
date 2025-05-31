using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Core.Modules.Statistics.Models.Response
{
    public class StatisticsDetails
    {
        public Global Global { get; set; }

        public Related Related { get; set; }
        public ChiefOccupantDetailStatistics ChiefOccupantStatistics { get; set; }
    }
}
