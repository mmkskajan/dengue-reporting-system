using CIDRS.Shared.Common.Api.Models;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.Penalties.Services
{
    public interface IPenaltyService
    {
        Task<bool> ResolveRedNoticessAsync(int chiefOccupantId);
        Task<bool> AddPenaltyAsync(int chiefOccupantId);
        Task<ResponseResult<bool>> ResolveAsync(int id);
    }
}