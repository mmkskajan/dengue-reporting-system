using AutoMapper;
using CIDRS.Core.Modules.WorkItems.Extensions;
using CIDRS.Core.Modules.WorkItems.Services;
using CIDRS.Core.Modules.WorkItems.ViewModels;
using CIDRS.Shared.Common.Api.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.WorkItems.Models.Response
{
    public class GetWorkItemQuery : IRequest<ResponseResult<WorkItemVM>>
    {
        public int Id { get; set; }
    }

    public class GetWorkItemQueryHandler : IRequestHandler<GetWorkItemQuery, ResponseResult<WorkItemVM>>
    {
        private readonly IWorkItemService _workItemService;
        private readonly IMapper _mapper;

        public GetWorkItemQueryHandler(IWorkItemService workItemService, IMapper mapper)
        {
            _workItemService = workItemService;
            _mapper = mapper;
        }
        public async Task<ResponseResult<WorkItemVM>> Handle(GetWorkItemQuery request, CancellationToken cancellationToken)
        {
            var response = await _workItemService.GetWorkitemAsync(request.Id);

            if (!response.Succeeded)
                return new ResponseResult<WorkItemVM>()
                {
                    Succeeded = false,
                    Errors = response.Errors,
                    Result = null
                };
            return new ResponseResult<WorkItemVM>()
            {
                Succeeded = true,
                Result = response.Result.ToViewModel(_mapper),
                Errors = null
            };
        }
    }
}
