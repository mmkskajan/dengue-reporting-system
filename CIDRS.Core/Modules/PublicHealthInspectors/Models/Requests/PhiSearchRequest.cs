using CIDRS.Shared.Common.Pagination.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Core.Modules.PublicHealthInspectors.Models.Requests
{
    public class PhiSearchRequest
    {
        public string BasicSearchValue { get; set; }
        public int? DistrictId { get; set; }
        public int? MohAreaId { get; set; }
        public PaginationOption PaginationOption { get; set; }
    }
}
