using AutoMapper;
using CIDRS.Core.Common.Models;
using CIDRS.Core.Modules.PublicHealthInspectors.Extensions;
using CIDRS.Core.Modules.PublicHealthInspectors.Services;
using CIDRS.Core.Modules.PublicHealthInspectors.ViewModels;
using CIDRS.Identity.Infrastructure;
using CIDRS.Infrastructure;
using CIDRS.Shared.Common.Api.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.PublicHealthInspectors.Queries
{
    public class GetPublicHealthInspectorQuery : IRequest<ResponseResult<PublicHealthInspectorVM>>
    {
        public int Id { get; set; }
    }

    public class GetPublicHealthInspectorQueryHandler : IRequestHandler<GetPublicHealthInspectorQuery, ResponseResult<PublicHealthInspectorVM>>
    {
        private readonly IPublicHealthInspectorService _publicHealthInspectorService;
        private readonly IMapper _mapper;
        private readonly IdentityDataContext _identityDataContect;
        private readonly ApplicationDataContext _dataContext;

        public GetPublicHealthInspectorQueryHandler(IPublicHealthInspectorService publicHealthInspectorService, IMapper mapper, IdentityDataContext identityDataContect, ApplicationDataContext dataContext)
        {
            _publicHealthInspectorService = publicHealthInspectorService;
            _mapper = mapper;
            _identityDataContect = identityDataContect;
            _dataContext = dataContext;
        }
        public async Task<ResponseResult<PublicHealthInspectorVM>> Handle(GetPublicHealthInspectorQuery request, CancellationToken cancellationToken)
        {
            var phiResponse = await _publicHealthInspectorService.ViewPublicHealthInspectorsAsync(request.Id);

            if (phiResponse.Succeeded)
                return new ResponseResult<PublicHealthInspectorVM>()
                {
                    Errors = null,
                    Result = phiResponse.Result.ToViewModelAsync(_mapper, _identityDataContect, _dataContext).Result,
                    Succeeded = true
                };
            else
                return new ResponseResult<PublicHealthInspectorVM>()
                {
                    Errors = phiResponse.Errors,
                     Result = null,
                     Succeeded = false
                };
        }
    }
}
