using CIDRS.API.Contracts.V1;
using CIDRS.API.Controllers.Base;
using CIDRS.Core.Common.Models;
using CIDRS.Core.Modules.ChiefOccupants.Commands;
using CIDRS.Core.Modules.ChiefOccupants.Queries;
using CIDRS.Core.Modules.ChiefOccupants.ViewModels;
using CIDRS.Shared.Common.Api.Models;
using CIDRS.Shared.Common.Pagination.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIDRS.API.Controllers.ChiefOccupants
{
    /// <summary>
    /// Chief Occupants API Endpoints
    /// </summary>

    public class ChiefOccupantController : ApiController
    {
        /// <summary>
        /// Register Chief Occupant Self Registratin 
        /// </summary>
        /// <returns></returns>
        [HttpPost(ApiRoutes.ChiefOccupant.RegisterChiefOccupant)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ResponseResult<ChiefOccupantVM>> RegisterChiefOccupantAsync(RegisterChiefOccupantCommand command)
        {
            var result = await Mediator.Send(command);
            return result;
        }

        /// <summary>
        /// Index ChiefOccupant / Search ChiefOccupant
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.ChiefOccupant.IndexChiefOccupants)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<PaginationVM<ChiefOccupantVM>> IndexChiefOccupantAsync(string searchString = "", int? districtId = null, int? mohAreaId = null,int? phiId = null, int page = 1, int pageSize = 10)
        {
            var command = new IndexChiefOccupantCommand()
            {
                BasicSearchValue = searchString,
                DistrictId = districtId,
                MohAreaId = mohAreaId,
                PhiId= phiId,
                PaginationOption = new PaginationOption() { Page = page, PageSize = pageSize }
            };
            var result = await Mediator.Send(command);
            return result;
        }

        /// <summary>
        /// View ChiefOccupant
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.ChiefOccupant.ViewChiefOccupant)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ResponseResult<ChiefOccupantVM>> ViewPhiAsync(int id)
        {
            var result = await Mediator.Send(new GetChiefOccupantQuery() { Id = id });
            return result;
        }
    }
    
}
