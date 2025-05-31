using AutoMapper;
using CIDRS.Core.Modules.ChiefOccupants.Extensions;
using CIDRS.Core.Modules.ChiefOccupants.ViewModels;
using CIDRS.Core.Modules.Statistics.Extensions;
using CIDRS.Core.Modules.Statistics.Models.Request;
using CIDRS.Core.Modules.Statistics.Services;
using CIDRS.Identity.Infrastructure;
using CIDRS.Infrastructure;
using CIDRS.Shared.Common.Pagination.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.Statistics.Queries
{
    public class GetPenalizationStatisticsDetailsQuery : IRequest<PaginationVM<ChiefOccupantVM>>
    {
        public PenalizationStatus Status { get; set; }
        public bool IsRelative { get; set; }
        public PaginationOption PaginationOption { get; set; }
    }

    public class GetPenalizationStatisticsDetailsQueryHandler : IRequestHandler<GetPenalizationStatisticsDetailsQuery, PaginationVM<ChiefOccupantVM>>
    {
        private readonly IMapper _mapper;
        private readonly IStatisticService _statisticService;
        private readonly IdentityDataContext _identityContext;
        private readonly ApplicationDataContext _dataContext;
        public GetPenalizationStatisticsDetailsQueryHandler(IStatisticService statisticService, IMapper mapper, IdentityDataContext identityDataContext, ApplicationDataContext dataContext)
        {
            _mapper = mapper;
            _statisticService = statisticService;
            _dataContext = dataContext;
            _identityContext = identityDataContext;
        }
        public async Task<PaginationVM<ChiefOccupantVM>> Handle(GetPenalizationStatisticsDetailsQuery request, CancellationToken cancellationToken)
        {
            var searchRequest = request.ToServiceRequest();
            var response = await _statisticService.GetPenalizationStatisticsAsync(searchRequest);

            var viewModels = response.Results.Select(x => x.ToViewModelAsync(_mapper, _identityContext, _dataContext).Result).ToList();

            return new PaginationVM<ChiefOccupantVM>()
            {
                DisplayCount = response.DisplayCount,
                DisplayEnd = response.DisplayEnd,
                DisplayStart = response.DisplayStart,
                NumberOfPages = response.NumberOfPages,
                Page = response.Page,
                TotalItems = response.TotalItems,
                PageSize = response.PageSize,
                Results = viewModels
            };
        }
    }
}
