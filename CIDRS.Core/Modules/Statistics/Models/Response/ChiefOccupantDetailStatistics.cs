using CIDRS.Core.Modules.Applications.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Core.Modules.Statistics.Models.Response
{
    public class ChiefOccupantDetailStatistics
    {
        public HomeSurroundingAllerts HomeSurroundingAlerts { get; set; }
        public PublicSurroundingComplaints PublicSurroundingComplaints { get; set; }
        public int Rating { get; set; }
        public bool IsPanelized { get; set; }
        public bool IsInDanger { get; set; }
    }

    public class HomeSurroundingAllerts
    {
        public int Approved { get; set; }
        public int Rejected { get; set; }
        public int PendingReview { get; set; }
        public int Total { get; set; }
        public ReportingApplicationVM PendingApplication { get; set; }
    }

    public class PublicSurroundingComplaints
    {
        public int Resolved { get; set; }        
        public int PendingReview { get; set; }
        public int Total { get; set; }
        public ReportingApplicationVM PendingApplication { get; set; }

    }


}
