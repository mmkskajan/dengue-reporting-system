using AutoMapper;
using CIDRS.Core.Modules.Applications.Extensions;
using CIDRS.Core.Modules.Applications.Services;
using CIDRS.Core.Modules.Applications.ViewModels;
using CIDRS.Shared.Common.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.Applications.Commands
{
    public class AddSurroundingSetCommand : IRequest<ResponseResult<ReportingApplicationVM>>
    {
        public int RelativeId { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
    public class AddSurroundingSetCommandHandler : IRequestHandler<AddSurroundingSetCommand, ResponseResult<ReportingApplicationVM>>
    {
        private readonly IMapper _mapper;
        private readonly IReportingApplicationService _reportingApplicationService;

        public AddSurroundingSetCommandHandler(IReportingApplicationService reportingApplicationService, IMapper mapper)
        {
            _mapper = mapper;
            _reportingApplicationService = reportingApplicationService;
        }
        public async Task<ResponseResult<ReportingApplicationVM>> Handle(AddSurroundingSetCommand command, CancellationToken cancellationToken)
        {
            var request = command.ToServiceRequest();

            var response = await _reportingApplicationService.AddSurroundingSet(request);

            if (!response.Succeeded)
                return new ResponseResult<ReportingApplicationVM>()
                {
                    Succeeded = response.Succeeded,
                    Errors = response.Errors,
                    Result = null
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
