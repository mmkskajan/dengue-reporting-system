using CIDRS.Shared.Common.Pagination.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Core.Modules.Polices.Models.Request
{
    public class PoliceSearchRequest
    {
        public string BasicSearchValue { get; set; }
        public int? MohAreaId { get; set; }
        public PaginationOption PaginationOption { get; set; }
    }
}
