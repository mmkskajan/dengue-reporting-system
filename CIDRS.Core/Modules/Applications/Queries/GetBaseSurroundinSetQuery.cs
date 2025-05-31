using AutoMapper;
using CIDRS.Core.Modules.Applications.Services;
using CIDRS.Core.Modules.Applications.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.Applications.Queries
{
    public class GetBaseSurroundinSetQuery : IRequest<List<SurroundingSetVM>>
    {
        public int? ChiefOccupantId { get; set; }
    }
    public class GetBaseSurroundinSetQueryHandler : IRequestHandler<GetBaseSurroundinSetQuery, List<SurroundingSetVM>>
    {
        private readonly IReportingApplicationService _reportingApplicationService;
        private readonly IMapper _mapper;

        public GetBaseSurroundinSetQueryHandler(IReportingApplicationService reportingApplicationService, IMapper mapper)
        {
            _reportingApplicationService = reportingApplicationService;
            _mapper = mapper;
        }
        public async Task<List<SurroundingSetVM>> Handle(GetBaseSurroundinSetQuery request, CancellationToken cancellationToken)
        {
            var result = await _reportingApplicationService.GetPendingBaseSurroundingSets(request.ChiefOccupantId);
            var viewModels = result.Select(x => _mapper.Map<SurroundingSetVM>(x)).ToList();
            return viewModels;
        }
    }
}
