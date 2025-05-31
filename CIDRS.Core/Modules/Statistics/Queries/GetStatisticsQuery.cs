using CIDRS.Core.Modules.Statistics.Models.Response;
using CIDRS.Core.Modules.Statistics.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.Statistics.Queries
{
    public class GetStatisticsQuery : IRequest<StatisticsDetails>
    {
       
    }

    public class GetStatisticsQueryHandler : IRequestHandler<GetStatisticsQuery, StatisticsDetails>
    {
        private readonly IStatisticService _statisticService;

        public GetStatisticsQueryHandler(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }
        public async Task<StatisticsDetails> Handle(GetStatisticsQuery request, CancellationToken cancellationToken)
        {
            return await _statisticService.GetStatisticsAsync();
        }
    }
}
