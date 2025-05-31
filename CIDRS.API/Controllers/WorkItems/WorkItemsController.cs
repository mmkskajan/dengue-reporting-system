using CIDRS.API.Contracts.V1;
using CIDRS.API.Controllers.Base;
using CIDRS.Core.Modules.WorkItems.Commands;
using CIDRS.Core.Modules.WorkItems.Models.Response;
using CIDRS.Core.Modules.WorkItems.ViewModels;
using CIDRS.Domain.Enums;
using CIDRS.Shared.Common.Api.Models;
using CIDRS.Shared.Common.Pagination.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIDRS.API.Controllers.WorkItems
{

    /// <summary>
    /// WorkItem Endpoints
    /// </summary>
    public class WorkItemsController : ApiController
    {

        /// <summary>
        /// Index WorkItems
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.WorkItem.IndexWorkItems)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<PaginationVM<WorkItemVM>> IndexWorkItemsAsync(bool? isActive = null, WorkItemType? type = null, int? assigneeId = null, DateTime? startDate = null, DateTime? endDate = null, int page = 1, int pageSize = 10)
        {
            var command = new IndexWorkItemCommand()
            {
                PaginationOption = new PaginationOption() { Page = page, PageSize = pageSize },
                AsigneeId = assigneeId,
                EndDate = endDate,
                IsActive = isActive,
                StartDate = startDate,
                Type = type
            };
            var result = await Mediator.Send(command);
            return result;
        }

        /// <summary>
        /// Get A WorkItem By Id
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.WorkItem.GetAWorkItem)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ResponseResult<WorkItemVM>> GetWorkItemAsync(int id)
        {            
            var result = await Mediator.Send(new GetWorkItemQuery() { Id= id});
            return result;
        }

        /// <summary>
        /// Re-Assign WorkItem
        /// </summary>
        /// <returns></returns>
        [HttpPost(ApiRoutes.WorkItem.ReAssign)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ResponseResult<bool>> ReAssignWorkItemAsync(ReAssignWorkItemCommand command)
        {
            var result = await Mediator.Send(command);
            return result;
        }

        /// <summary>
        /// Remark WorkItem
        /// </summary>
        /// <returns></returns>
        [HttpPost(ApiRoutes.WorkItem.Remark)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ResponseResult<bool>> RemarkWorkItemAsync(AddWorkItemRemarkCommand command)
        {
            var result = await Mediator.Send(command);
            return result;
        }

        /// <summary>
        /// Approve WorkItem
        /// </summary>
        /// <returns></returns>
        [HttpPost(ApiRoutes.WorkItem.Approve)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ResponseResult<bool>> ApproveAsync(int id)
        {
            var result = await Mediator.Send(new ApproveCommand() { Id = id});
            return result;
        }

        /// <summary>
        /// Reject WorkItem
        /// </summary>
        /// <returns></returns>
        [HttpPost(ApiRoutes.WorkItem.Reject)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ResponseResult<bool>> RejectAsync(int id)
        {
            var result = await Mediator.Send(new RejectCommand() { Id = id });
            return result;
        }
    }
}
