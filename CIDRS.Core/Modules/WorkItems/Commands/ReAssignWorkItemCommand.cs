using CIDRS.Core.Modules.WorkItems.Services;
using CIDRS.Shared.Common.Api.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.WorkItems.Commands
{
    public class ReAssignWorkItemCommand : IRequest<ResponseResult<bool>>
    {
        public int Id { get; set; }
        public int AssigneeId { get; set; }
        public int PoliceId { get; set; }
    }

    public class ReAssignWorkItemCommandHandler : IRequestHandler<ReAssignWorkItemCommand, ResponseResult<bool>>
    {
        private readonly IWorkItemService _workItemService;

        public ReAssignWorkItemCommandHandler(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }
        public async Task<ResponseResult<bool>> Handle(ReAssignWorkItemCommand request, CancellationToken cancellationToken)
        {
            return await _workItemService.ReAsssignAsync(request.Id,request.AssigneeId, request.PoliceId);
        }
    }
}
