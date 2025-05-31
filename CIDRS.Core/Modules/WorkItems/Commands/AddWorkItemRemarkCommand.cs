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
    public class AddWorkItemRemarkCommand : IRequest<ResponseResult<bool>>
    {
        public int Id { get; set; }
        public string Remark { get; set; }
    }

    public class AddWorkItemRemarkCommandHandler : IRequestHandler<AddWorkItemRemarkCommand, ResponseResult<bool>>
    {
        private readonly IWorkItemService _workItemService;

        public AddWorkItemRemarkCommandHandler(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }
        public async Task<ResponseResult<bool>> Handle(AddWorkItemRemarkCommand request, CancellationToken cancellationToken)
        {
            return await _workItemService.AddRemarkAsync(request.Id, request.Remark);
        }
    }
}
