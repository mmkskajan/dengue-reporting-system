using AutoMapper;
using CIDRS.Core.Modules.Polices.Models.Response;
using CIDRS.Core.Modules.Polices.Services;
using CIDRS.Infrastructure;
using CIDRS.Shared.Common.Api.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.Polices.Queries
{
    public class GetPoliceQuery : IRequest<ResponseResult<PoliceVM>>
    {
        public int Id { get; set; }
    }

    public class GetPoliceQueryHandler : IRequestHandler<GetPoliceQuery, ResponseResult<PoliceVM>>
    {
        private readonly IPoliceService _policeService;
        private readonly IMapper _mapper;
        private readonly ApplicationDataContext _dataContext;

        public GetPoliceQueryHandler(IPoliceService policeService, ApplicationDataContext dataContext, IMapper mapper)
        {
            _policeService = policeService;
            _mapper = mapper;
            _dataContext = dataContext;
        }
        public async Task<ResponseResult<PoliceVM>> Handle(GetPoliceQuery query, CancellationToken cancellationToken)
        {
            var response = await _policeService.GetPoliceAsync(query.Id);

            if(response.Succeeded)
            {
                var policeVM = _mapper.Map<PoliceVM>(response.Result);
                return new ResponseResult<PoliceVM>()
                {
                    Errors = null,
                    Result = policeVM,
                    Succeeded = true
                };
            }

            return new ResponseResult<PoliceVM>()
            {
                Errors = response.Errors,
                Succeeded = false,
                Result = null
            };

        }
    }
}
