using CIDRS.Shared.Common.Pagination.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Core.Modules.ChiefOccupants.Models.Request
{
    public class ChiefOccupantSearchRequest
    {
        public string BasicSearchValue { get; set; }
        public int? DistrictId { get; set; }
        public int? MohAreaId { get; set; }
        public int? PhiId { get; set; }
        public PaginationOption PaginationOption { get; set; }
    }
}
