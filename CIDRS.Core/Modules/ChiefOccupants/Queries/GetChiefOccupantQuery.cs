using AutoMapper;
using CIDRS.Core.Modules.ChiefOccupants.Extensions;
using CIDRS.Core.Modules.ChiefOccupants.Services;
using CIDRS.Core.Modules.ChiefOccupants.ViewModels;
using CIDRS.Identity.Infrastructure;
using CIDRS.Infrastructure;
using CIDRS.Shared.Common.Api.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.ChiefOccupants.Queries
{
    public class GetChiefOccupantQuery : IRequest<ResponseResult<ChiefOccupantVM>>
    {
        public int Id { get; set; }
    }

    public class GetChiefOccupantQueryHandler : IRequestHandler<GetChiefOccupantQuery, ResponseResult<ChiefOccupantVM>>
    {
        private readonly IMapper _mapper;
        private readonly IdentityDataContext _identityContext;
        private readonly ApplicationDataContext _dataContext;
        private readonly IChiefOccupantService _chiefOccupantService;
        public GetChiefOccupantQueryHandler(IChiefOccupantService chiefOccupantService,IMapper mapper, IdentityDataContext identityDataContect, ApplicationDataContext dataContext)
        {
            _chiefOccupantService = chiefOccupantService;
            _mapper = mapper;
            _identityContext = identityDataContect;
            _dataContext = dataContext;
        }
        public async Task<ResponseResult<ChiefOccupantVM>> Handle(GetChiefOccupantQuery request, CancellationToken cancellationToken)
        {
            var response = await _chiefOccupantService.GetChiefOccupantById(request.Id);

            if (response.Succeeded)
                return new ResponseResult<ChiefOccupantVM>()
                {
                    Errors = null,
                    Result = response.Result.ToViewModelAsync(_mapper, _identityContext, _dataContext).Result,
                    Succeeded = true
                };
            else
                return new ResponseResult<ChiefOccupantVM>()
                {
                    Errors = response.Errors,
                    Result = null,
                    Succeeded = false
                };
        }
    }
}
