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
    public class GetMohAreasQuery : IRequest<List<MohAreaVM>>
    {
        public int? DistrictId { get; set; }
    }

    public class GetMohAreasQueryHandler : IRequestHandler<GetMohAreasQuery, List<MohAreaVM>>
    {
        private readonly IMasterDataService _masterDataService;
        private readonly IMapper _mapper;
        public GetMohAreasQueryHandler(IMasterDataService masterDataService, IMapper mapper)
        {
            _masterDataService = masterDataService;
            _mapper = mapper;
        }
        public async Task<List<MohAreaVM>> Handle(GetMohAreasQuery request, CancellationToken cancellationToken)
        {
            var mohAreas = await _masterDataService.GetMohAreasAsync(request.DistrictId);

            return mohAreas.Select(x => _mapper.Map<MohAreaVM>(x)).ToList();
        }
    }
}
