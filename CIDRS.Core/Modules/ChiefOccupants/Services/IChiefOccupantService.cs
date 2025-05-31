using CIDRS.Core.Common.Models;
using CIDRS.Core.Modules.ChiefOccupants.Models.Request;
using CIDRS.Domain.Models.Entity.ChiefOccupants;
using CIDRS.Shared.Common.Api.Models;
using CIDRS.Shared.Common.Pagination.Models;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.ChiefOccupants.Services
{
    public interface IChiefOccupantService
    {
        Task<ResponseResult<ChiefOccupant>> RegisterChiefOccupantAsync(RegisterChiefOccupantRequest request);
        Task<ChiefOccupant> GetChiefOccupantByIdentityId(string identityId);
        Task<ResponseResult<ChiefOccupant>> GetChiefOccupantById(int id);
        Task<PaginationVM<ChiefOccupant>> IndexChiefOccupantsAsync(ChiefOccupantSearchRequest searchRequest);
    }
}