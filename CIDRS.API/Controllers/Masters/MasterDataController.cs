using CIDRS.API.Contracts.V1;
using CIDRS.API.Controllers.Base;
using CIDRS.Core.Modules.Masters.Queries;
using CIDRS.Core.Modules.Masters.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIDRS.API.Controllers.Masters
{
    /// <summary>
    /// Master Data API Endpoints
    /// </summary>
    public class MasterDataController : ApiController
    {

        /// <summary>
        /// Get Districts Endpoint
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.Master.GetDistricts)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<List<DistrictVM>>> GetDistrictsAsync()
        {
           var result = await Mediator.Send(new GetDistrictsQuery());

            return result;
        }

        /// <summary>
        /// Get All Moh Areas Endpoint
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.Master.GetMohAreas)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<List<MohAreaVM>>> GetMohAreasAsync()
        {
            var result = await Mediator.Send(new GetMohAreasQuery() { DistrictId = null});

            return result;
        }

        /// <summary>
        /// Get Moh Areas By District Id Endpoint
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.Master.GetMohAreasByDisterictId)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<List<MohAreaVM>>> GetMohAreasAsync(int districtId)
        {
            var result = await Mediator.Send(new GetMohAreasQuery() { DistrictId = districtId });

            return result;
        }

        /// <summary>
        /// Get All Police Station Endpoint
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.Master.GetPoliceStations)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<List<PoliceStationVM>>> GetPoliceStationsAsync()
        {
            var result = await Mediator.Send(new GetPoliceStationsQuery() { MohAreaId = null });

            return result;
        }

        /// <summary>
        /// Get Police Station By Moh Area Id Endpoint
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.Master.GetPoliceStationsByMohAreaId)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<List<PoliceStationVM>>> GetPoliceStationsAsync(int mohAreaId)
        {
            var result = await Mediator.Send(new GetPoliceStationsQuery() { MohAreaId = mohAreaId });

            return result;
        }
    }
}
