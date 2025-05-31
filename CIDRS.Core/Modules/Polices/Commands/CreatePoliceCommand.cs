using AutoMapper;
using CIDRS.Core.Modules.Polices.Extensions;
using CIDRS.Core.Modules.Polices.Models.Response;
using CIDRS.Core.Modules.Polices.Services;
using CIDRS.Infrastructure;
using CIDRS.Shared.Common.Api.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.Polices.Commands
{
    public class CreatePoliceCommand : IRequest<ResponseResult<PoliceVM>>
    {
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public int PoliceStationId { get; set; }
    }

    public class CreatePoliceCommandHandler : IRequestHandler<CreatePoliceCommand, ResponseResult<PoliceVM>>
    {
        private readonly IPoliceService _policeService;
        private readonly IMapper _mapper;
        private readonly ApplicationDataContext _dataContext;

        public CreatePoliceCommandHandler(IPoliceService policeService, IMapper mapper, ApplicationDataContext dataContext)
        {
            _policeService = policeService;
            _mapper = mapper;
            _dataContext = dataContext;
        }
        public async Task<ResponseResult<PoliceVM>> Handle(CreatePoliceCommand command, CancellationToken cancellationToken)
        {
            var request = command.ToServiceRequest();
            var response = await _policeService.CreatePoliceAsync(request);
            if(response.Succeeded)
            {
                var police = response.Result;
                var policeStation = await _dataContext.PoliceStations.FirstOrDefaultAsync(x => x.Id == police.PoliceStationId);
                police.PoliceStation = policeStation;

                var policeVM = _mapper.Map<PoliceVM>(police);
                return new ResponseResult<PoliceVM>
                {
                    Errors = null,
                    Result = policeVM,
                    Succeeded = true
                };
            }

            return new ResponseResult<PoliceVM>
            {
                Errors = response.Errors,
                Result = null,
                Succeeded = false
            };

        }
    }
}
