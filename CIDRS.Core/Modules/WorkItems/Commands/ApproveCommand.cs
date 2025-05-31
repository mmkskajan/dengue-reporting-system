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
    public class ApproveCommand : IRequest<ResponseResult<bool>>
    {
        public int Id { get; set; }
    }

    public class ApproveCommandHandler : IRequestHandler<ApproveCommand, ResponseResult<bool>>
    {
        private readonly IWorkItemService _workItemService;

        public ApproveCommandHandler(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }
        public async Task<ResponseResult<bool>> Handle(ApproveCommand request, CancellationToken cancellationToken)
        {
            return await _workItemService.ApproveAsync(request.Id);
        }
    }
}
