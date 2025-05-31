using CIDRS.Domain.Models.Entity.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Domain.Models.Entity.Applications
{
    public class SurroundingSet : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int? RelativeId { get; set; }
        public SurroundingSet RelativeSurroundingSet { get; set; }
        public int ReportingApplicationId { get; set; }
        public ReportingApplication Application { get; set; }
    }
}
