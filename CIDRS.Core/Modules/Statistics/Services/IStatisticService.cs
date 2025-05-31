using CIDRS.Core.Modules.Statistics.Models.Request;
using CIDRS.Core.Modules.Statistics.Models.Response;
using CIDRS.Domain.Models.Entity.ChiefOccupants;
using CIDRS.Domain.Models.Entity.WorkItems;
using CIDRS.Shared.Common.Pagination.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.Statistics.Services
{
    public interface IStatisticService
    {
        Task<StatisticsDetails> GetStatisticsAsync();
        Task<PaginationVM<WorkItem>> GetApplicationStatisticsAsync(ApplicationStatisticsSearchRequest searchRequest);
        Task<PaginationVM<ChiefOccupant>> GetEnvironmentStatisticsAsync(EnvironmentStatisticsSearchRequest searchRequest);
        Task<PaginationVM<ChiefOccupant>> GetPenalizationStatisticsAsync(PenalizationStatisticsSearchRequest searchRequest);
    }
}
