using CIDRS.Core.Common.Models;
using CIDRS.Core.Modules.WorkItems.Models.Request;
using CIDRS.Domain.Enums;
using CIDRS.Domain.Models.Entity.WorkItems;
using CIDRS.Shared.Common.Api.Models;
using CIDRS.Shared.Common.Pagination.Models;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.WorkItems.Services
{
    public interface IWorkItemService
    {
        Task<ResponseResult<WorkItem>> CreateWorkItemAsync(WorkItemType type, int relativeId);
        Task<ResponseResult<WorkItem>> GetWorkitemAsync(int Id);
        Task<PaginationVM<WorkItem>> IndexWorkItemsAsync(WorkItemSearchRequest searchRequest);
        Task<ResponseResult<bool>> ReAsssignAsync(int id, int assigneeId, int policeId);
        Task<ResponseResult<bool>> ReAsssignAsync(int id, int assigneeId);
        Task<ResponseResult<bool>> AddRemarkAsync(int id, string remark);
        Task<ResponseResult<bool>> ApproveCORegistrationAsync(int id);
        Task<ResponseResult<bool>> ApproveAsync(int id);
        Task<ResponseResult<bool>> RejectAsync(int id);
    }
}