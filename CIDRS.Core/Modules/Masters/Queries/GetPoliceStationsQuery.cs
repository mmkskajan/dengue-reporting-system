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
    public class GetPoliceStationsQuery : IRequest<List<PoliceStationVM>>
    {
        public int? MohAreaId { get; set; }
    }

    public class GetPoliceStationsQueryHandler : IRequestHandler<GetPoliceStationsQuery, List<PoliceStationVM>>
    {
        private readonly IMasterDataService _masterDataService;
        private readonly IMapper _mapper;
        public GetPoliceStationsQueryHandler(IMasterDataService masterDataService, IMapper mapper)
        {
            _masterDataService = masterDataService;
            _mapper = mapper;
        }
        public async Task<List<PoliceStationVM>> Handle(GetPoliceStationsQuery request, CancellationToken cancellationToken)
        {
            var policeStations = await _masterDataService.GetPoliceStationsAsync(request.MohAreaId);

            return policeStations.Select(x => _mapper.Map<PoliceStationVM>(x)).ToList();
        }
    }
}
