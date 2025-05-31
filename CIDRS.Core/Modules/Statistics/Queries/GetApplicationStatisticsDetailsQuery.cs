using AutoMapper;
using CIDRS.Core.Modules.Statistics.Extensions;
using CIDRS.Core.Modules.Statistics.Models.Request;
using CIDRS.Core.Modules.Statistics.Services;
using CIDRS.Core.Modules.WorkItems.Extensions;
using CIDRS.Core.Modules.WorkItems.ViewModels;
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
    public class GetApplicationStatisticsDetailsQuery : IRequest<PaginationVM<WorkItemVM>>
    {
        public ApplicationStatisticsType Type { get; set; }
        public TimeFrequency TimeFrequency { get; set; }
        public bool IsRelative { get; set; }
        public PaginationOption PaginationOption { get; set; }
    }

    public class GetApplicationStatisticsDetailsQueryHandler : IRequestHandler<GetApplicationStatisticsDetailsQuery, PaginationVM<WorkItemVM>>
    {
        private readonly IStatisticService _statisticService;
        private readonly IMapper _mapper;
        public GetApplicationStatisticsDetailsQueryHandler(IMapper mapper, IStatisticService statisticService)
        {
            _statisticService = statisticService;
            _mapper = mapper;
        }
        public async Task<PaginationVM<WorkItemVM>> Handle(GetApplicationStatisticsDetailsQuery request, CancellationToken cancellationToken)
        {
            var searchRequest = request.ToServiceRequest();
            var response = await _statisticService.GetApplicationStatisticsAsync(searchRequest);

            var viewModels = response.Results.Select(x => x.ToViewModel(_mapper)).ToList();

            return new PaginationVM<WorkItemVM>()
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
