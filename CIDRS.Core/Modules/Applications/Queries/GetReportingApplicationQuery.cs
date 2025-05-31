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

namespace CIDRS.Core.Modules.Applications.Queries
{
    public class GetReportingApplicationQuery : IRequest<ResponseResult<ReportingApplicationVM>>
    {
        public int Id { get; set; }
    }

    public class GetReportingApplicationQueryHandler : IRequestHandler<GetReportingApplicationQuery, ResponseResult<ReportingApplicationVM>>
    {
        private readonly IReportingApplicationService _reportingApplicationService;
        private readonly IMapper _mapper;

        public GetReportingApplicationQueryHandler(IReportingApplicationService reportingApplicationService, IMapper mapper)
        {
            _reportingApplicationService = reportingApplicationService;
            _mapper = mapper;
        }
        public async Task<ResponseResult<ReportingApplicationVM>> Handle(GetReportingApplicationQuery request, CancellationToken cancellationToken)
        {
            var response = await _reportingApplicationService.GetReportingApplication(request.Id);

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
                Result = viewModel,
                Errors = null,
                Succeeded = true
            };
        }
    }
}
