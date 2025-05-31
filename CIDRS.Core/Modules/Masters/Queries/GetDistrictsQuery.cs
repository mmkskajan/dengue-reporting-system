using AutoMapper;
using CIDRS.Core.Modules.Masters.Services;
using CIDRS.Core.Modules.Masters.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.Masters.Queries
{
    public class GetDistrictsQuery : IRequest<List<DistrictVM>>
    {
    }

    public class GetDistrictsQueryHandler : IRequestHandler<GetDistrictsQuery, List<DistrictVM>>
    {
        private readonly IMasterDataService _masterDataService;
        private readonly IMapper _mapper;

        public GetDistrictsQueryHandler(IMasterDataService masterDataService, IMapper mapper)
        {
            _masterDataService = masterDataService;
            _mapper = mapper;
        }
        public async Task<List<DistrictVM>> Handle(GetDistrictsQuery request, CancellationToken cancellationToken)
        {
            var districts = await _masterDataService.GetDistrictsAsync();

            return districts.Select(x => _mapper.Map<DistrictVM>(x)).ToList();
        }
    }
}
