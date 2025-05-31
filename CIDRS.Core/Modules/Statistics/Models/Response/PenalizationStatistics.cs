using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Core.Modules.Statistics.Models.Response
{
    public class PenalizationStatistics
    {
        public Pending Pending { get; set; }
        public Resolved Resolved { get; set; }
    }
}
