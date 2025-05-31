using CIDRS.Core.Modules.Polices.Models.Request;
using CIDRS.Domain.Models.Entity.Polices;
using CIDRS.Shared.Common.Api.Models;
using CIDRS.Shared.Common.Pagination.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.Polices.Services
{
    public interface IPoliceService
    {
        Task<ResponseResult<Police>> CreatePoliceAsync(CreatePoliceRequest request);
        Task<ResponseResult<Police>> GetPoliceAsync(int id);
        Task<ResponseResult<List<Police>>> PoliceLookupAsync(int mohAreaId);
        Task<PaginationVM<Police>> IndexPolicesAsync(PoliceSearchRequest searchRequest);
    }
}