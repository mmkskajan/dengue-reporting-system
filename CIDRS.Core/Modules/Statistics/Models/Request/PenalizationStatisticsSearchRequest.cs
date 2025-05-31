using CIDRS.Shared.Common.Pagination.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Core.Modules.Statistics.Models.Request
{
    public class PenalizationStatisticsSearchRequest
    {
        public PenalizationStatus Status { get; set; }
        public bool IsRelative { get; set; }
        public PaginationOption PaginationOption { get; set; }
    }
}
