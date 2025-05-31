using AutoMapper;
using CIDRS.Core.Modules.Polices.Models.Response;
using CIDRS.Core.Modules.Polices.Services;
using CIDRS.Domain.Models.Entity.Polices;
using CIDRS.Shared.Common.Api.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.Polices.Commands
{
    public class PoliceLookupCommand : IRequest<ResponseResult<List<PoliceVM>>>
    {
        public int mohAreaId { get; set; }
    }

    public class PoliceLookupCommandHandler : IRequestHandler<PoliceLookupCommand, ResponseResult<List<PoliceVM>>>
    {
        private readonly IPoliceService _policeService;
        private readonly IMapper _mapper;

        public PoliceLookupCommandHandler(IPoliceService publicHealthInspectorService, IMapper mapper)
        {
            _policeService = publicHealthInspectorService;
            _mapper = mapper;
        }
        public async Task<ResponseResult<List<PoliceVM>>> Handle(PoliceLookupCommand request, CancellationToken cancellationToken)
        {
            var response = await _policeService.PoliceLookupAsync(request.mohAreaId);

            var viewModels = response.Result.Select(x => _mapper.Map<PoliceVM>(x)).ToList();

            return new ResponseResult<List<PoliceVM>>()
            {
                Succeeded = true,
                Result = viewModels,
                Errors = null
            };
        }
    }
}
