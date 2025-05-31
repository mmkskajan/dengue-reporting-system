using AutoMapper;
using CIDRS.Core.Modules.PublicHealthInspectors.Services;
using CIDRS.Core.Modules.PublicHealthInspectors.ViewModels;
using CIDRS.Shared.Common.Api.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.PublicHealthInspectors.Commands
{
    public class PhiLookupCommand : IRequest<ResponseResult<List<PublicHealthInspectorVM>>>
    {
        public int districtId { get; set; }
        public int mohAreaId { get; set; }
    }

    public class PhiLookupCommandHandler : IRequestHandler<PhiLookupCommand, ResponseResult<List<PublicHealthInspectorVM>>>
    {
        private readonly IPublicHealthInspectorService _publicHealthInspectorService;
        private readonly IMapper _mapper;

        public PhiLookupCommandHandler(IPublicHealthInspectorService publicHealthInspectorService, IMapper mapper)
        {
            _publicHealthInspectorService = publicHealthInspectorService;
            _mapper = mapper;
        }
        public async Task<ResponseResult<List<PublicHealthInspectorVM>>> Handle(PhiLookupCommand request, CancellationToken cancellationToken)
        {
            var response =await _publicHealthInspectorService.PhiLookupAsync(request.districtId, request.mohAreaId);

            var viewModels = response.Result.Select(x => _mapper.Map<PublicHealthInspectorVM>(x)).ToList();

            return new ResponseResult<List<PublicHealthInspectorVM>>()
            {
                Succeeded = true,
                Result = viewModels,
                Errors = null
            };
        }
    }
}
