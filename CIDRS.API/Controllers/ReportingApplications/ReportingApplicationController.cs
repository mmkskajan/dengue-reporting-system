using CIDRS.API.Contracts.V1;
using CIDRS.API.Controllers.Base;
using CIDRS.Core.Modules.Applications.Commands;
using CIDRS.Core.Modules.Applications.Models.Request;
using CIDRS.Core.Modules.Applications.Queries;
using CIDRS.Core.Modules.Applications.ViewModels;
using CIDRS.Shared.Common.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIDRS.API.Controllers.ReportingApplications
{
    /// <summary>
    /// Reporting Application Endpoints
    /// </summary>
    public class ReportingApplicationController : ApiController
    {
        /// <summary>
        /// Start Application
        /// </summary>
        /// <returns></returns>
        [HttpPost(ApiRoutes.ReportingApplication.StartApplication)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ResponseResult<ReportingApplicationVM>> StartAsync(int? chiefOccupantId, bool isPublicSurroundingApplication = false)
        {
            return await Mediator.Send(new StartApplicationCommand() { chiefOccupantId = chiefOccupantId, isPublicSurroundingApplication = isPublicSurroundingApplication });

        }
        
        /// <summary>
        /// Get Application
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.ReportingApplication.GetApplication)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ResponseResult<ReportingApplicationVM>> GetApplicationAsync(int id)
        {
            return await Mediator.Send(new GetReportingApplicationQuery() { Id = id });
        }

        /// <summary>
        /// Add Base Surrounding Set
        /// </summary>
        /// <returns></returns>
        [HttpPost(ApiRoutes.ReportingApplication.AddBaseSurroundingSet)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ResponseResult<ReportingApplicationVM>> AddSurroundingSetAsync(BaseSurroundingSetRequest request, int chiefOccupantId)
        {
            return await Mediator.Send(new AddBaseSurroundingSetCommand() { ChiefOccupantId = chiefOccupantId, Description = request.Description, Image = request.Image, Name = request.Name, Latitude = request.Latitude, Longitude = request.Longitude });
        }

        /// <summary>
        /// Add Public Surrounding Set
        /// </summary>
        /// <returns></returns>
        [HttpPut(ApiRoutes.ReportingApplication.AddPublicSurroundingSet)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ResponseResult<ReportingApplicationVM>> AddSurroundingSetAsync(BaseSurroundingSetRequest request)
        {
            return await Mediator.Send(new AddPublicSurroundinSetCommand() { Description = request.Description, Image = request.Image, Name = request.Name, Latitude = request.Latitude, Longitude = request.Longitude });
        }

        /// <summary>
        /// Add Surrounding Set
        /// </summary>
        /// <returns></returns>
        [HttpPut(ApiRoutes.ReportingApplication.AddSurroundingSet)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ResponseResult<ReportingApplicationVM>> AddSurroundingSetAsync(SurroundingSetRequest request)
        {
            return await Mediator.Send(new AddSurroundingSetCommand() { RelativeId = request.RelativeId, Description = request.Description, Image = request.Image, Latitude = request.Latitude, Longitude = request.Longitude });
        }

        /// <summary>
        /// Get Base  Surrounding Set
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.ReportingApplication.GetBaseSurroundingSet)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<List<SurroundingSetVM>> GetBaseSurroundingSetAsync(int? chiefOccupantId)
        {
            return await Mediator.Send(new GetBaseSurroundinSetQuery() {ChiefOccupantId = chiefOccupantId});
        }

        /// <summary>
        /// Complete Application
        /// </summary>
        /// <returns></returns>
        [HttpPost(ApiRoutes.ReportingApplication.CompleteApplication)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ResponseResult<ReportingApplicationVM>> CompleteAsync(int? chiefOccupantId, bool isPublicSurroundingApplication = false)
        {
            return await Mediator.Send(new CompleteApplicationCommand() { chiefOccupantId = chiefOccupantId, IsPublicSurroundingApplication = isPublicSurroundingApplication});

        }
    }
}
