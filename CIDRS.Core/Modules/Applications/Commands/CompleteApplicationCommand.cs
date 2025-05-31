using AutoMapper;
using CIDRS.Core.Modules.Applications.Services;
using CIDRS.Core.Modules.Applications.ViewModels;
using CIDRS.Shared.Common.Api.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.Applications.Commands
{
    public class CompleteApplicationCommand : IRequest<ResponseResult<ReportingApplicationVM>>
    {
        public int? chiefOccupantId { get; set; }
        public bool IsPublicSurroundingApplication { get; set; }
    }

    public class CompleteApplicationCommandHandler : IRequestHandler<CompleteApplicationCommand, ResponseResult<ReportingApplicationVM>>
    {
        private readonly IReportingApplicationService _reportingAppliucationService;
        private readonly IMapper _mapper;

        public CompleteApplicationCommandHandler(IReportingApplicationService reportingAppliucationService, IMapper mapper)
        {
            _reportingAppliucationService = reportingAppliucationService;
            _mapper = mapper;
        }
        public async Task<ResponseResult<ReportingApplicationVM>> Handle(CompleteApplicationCommand request, CancellationToken cancellationToken)
        {
            var response = await _reportingAppliucationService.CompleteAsync(request.chiefOccupantId, request.IsPublicSurroundingApplication);

            if (!response.Succeeded)
                return new ResponseResult<ReportingApplicationVM>()
                {
                    Errors = response.Errors,
                    Result = null,
                    Succeeded = false
                };

            var viewModel = _mapper.Map<ReportingApplicationVM>(response.Result);

            return new ResponseResult<ReportingApplicationVM>()
            {
                Errors = null,
                Result = viewModel,
                Succeeded = true
            };
        }
    }
}
