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
    public class RejectCommand : IRequest<ResponseResult<bool>>
    {
        public int Id { get; set; }
    }

    public class RejectCommandHandler : IRequestHandler<RejectCommand, ResponseResult<bool>>
    {
        private readonly IWorkItemService _workItemService;

        public RejectCommandHandler(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }
        public async Task<ResponseResult<bool>> Handle(RejectCommand request, CancellationToken cancellationToken)
        {
            return await _workItemService.RejectAsync(request.Id);
        }
    }
}
