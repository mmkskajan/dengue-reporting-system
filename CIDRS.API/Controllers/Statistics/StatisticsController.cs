using CIDRS.API.Contracts.V1;
using CIDRS.API.Controllers.Base;
using CIDRS.Core.Modules.ChiefOccupants.ViewModels;
using CIDRS.Core.Modules.Statistics.Models.Request;
using CIDRS.Core.Modules.Statistics.Models.Response;
using CIDRS.Core.Modules.Statistics.Queries;
using CIDRS.Core.Modules.WorkItems.ViewModels;
using CIDRS.Shared.Common.Pagination.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIDRS.API.Controllers.Statistics
{
    /// <summary>
    /// Statistics Endpoints 
    /// </summary>
    public class StatisticsController : ApiController
    {
        /// <summary>
        /// Get Statistics
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.Statistics.GetStatistics)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<StatisticsDetails> GetStatisticsAsync()
        {
            var result = await Mediator.Send(new GetStatisticsQuery() { });
            return result;
        }
        /// <summary>
        /// Get Application Statistics Details
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.Statistics.GetApplicationStatisticsDetails)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<PaginationVM<WorkItemVM>> GetStatisticsDetails(ApplicationStatisticsType applicationType = ApplicationStatisticsType.Any, TimeFrequency timeFrequency = TimeFrequency.Any, bool isRelative = false, int page = 1, int pageSize = 10)
        {
            var result = await Mediator.Send(new GetApplicationStatisticsDetailsQuery() {Type = applicationType, TimeFrequency = timeFrequency, IsRelative =isRelative, PaginationOption = new Shared.Common.Pagination.Models.PaginationOption() {Page= page, PageSize = pageSize } });
            return result;
        }

        /// <summary>
        /// Get Environment Statistics Details
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.Statistics.GetEnvironmentStatisticsDetails)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<PaginationVM<ChiefOccupantVM>> GetStatisticsDetails(EnvironmentStatus status = EnvironmentStatus.Any, bool isRelative = false, int page = 1, int pageSize = 10)
        {
            var result = await Mediator.Send(new GetEnvironmentStatisticsDetailsQuery() { Status = status, IsRelative = isRelative, PaginationOption = new Shared.Common.Pagination.Models.PaginationOption() { Page = page, PageSize = pageSize } });
            return result;
        }

        /// <summary>
        /// Get Penalization Statistics Details
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.Statistics.GetPenalizationStatisticsDetails)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<PaginationVM<ChiefOccupantVM>> GetStatisticsDetails(PenalizationStatus status = PenalizationStatus.Any, bool isRelative = false, int page = 1, int pageSize = 10)
        {
            var result = await Mediator.Send(new GetPenalizationStatisticsDetailsQuery() { Status = status, IsRelative = isRelative, PaginationOption = new Shared.Common.Pagination.Models.PaginationOption() { Page = page, PageSize = pageSize } });
            return result;
        }

    }
}
