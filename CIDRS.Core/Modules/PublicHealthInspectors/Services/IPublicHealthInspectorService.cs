using CIDRS.Core.Common.Models;
using CIDRS.Core.Modules.PublicHealthInspectors.Models.Requests;
using CIDRS.Domain.Models.Entity.PublicHealthInspectors;
using CIDRS.Shared.Common.Api.Models;
using CIDRS.Shared.Common.Pagination.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.PublicHealthInspectors.Services
{
    public interface IPublicHealthInspectorService
    {
        Task<ResponseResult<PublicHealthInspector>> RegisterPublicHealthInspectorsAsync(RegisterPhiRequest request);
        Task<PaginationVM<PublicHealthInspector>> IndexPublicHealthInspectorsAsync(PhiSearchRequest searchRequest);
        Task<ResponseResult<PublicHealthInspector>> ViewPublicHealthInspectorsAsync(int id);
        Task<PublicHealthInspector> ViewPublicHealthInspectorsAsync(string identityUserId);
        Task<ResponseResult<List<PublicHealthInspector>>> PhiLookupAsync(int districtId, int mohAreaId);
    }
}
