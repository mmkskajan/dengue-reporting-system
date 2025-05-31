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
    public class StartApplicationCommand : IRequest<ResponseResult<ReportingApplicationVM>>
    {
        public int? chiefOccupantId { get; set; }
        public bool isPublicSurroundingApplication { get; set; }
    }
    public class StartApplicationCommandHandler : IRequestHandler<StartApplicationCommand, ResponseResult<ReportingApplicationVM>>
    {
        private readonly IReportingApplicationService _reportingAppliucationService;
        private readonly IMapper _mapper;

        public StartApplicationCommandHandler(IReportingApplicationService reportingAppliucationService, IMapper mapper)
        {
            _reportingAppliucationService = reportingAppliucationService;
            _mapper = mapper;
        }
        public async Task<ResponseResult<ReportingApplicationVM>> Handle(StartApplicationCommand request, CancellationToken cancellationToken)
        {
            var response = await _reportingAppliucationService.StartReportingApplication(request.chiefOccupantId, request.isPublicSurroundingApplication);

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
