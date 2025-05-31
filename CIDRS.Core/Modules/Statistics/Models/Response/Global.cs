using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Core.Modules.Statistics.Models.Response
{
    public class Global
    {
        public HomeSurroundingSatatistics HomeSurroundings { get; set; }
        public PublicSurroundingStatistics PublicSurroundings { get; set; }
        public ChiefOccupantsStatistics ChiefOccupants { get; set; }
        public EnvironmentStatistics Environment { get; set; }
        public PenalizationStatistics Penalization { get; set; }
        public OverviewStatistics Overview { get; set; }

    }
}
