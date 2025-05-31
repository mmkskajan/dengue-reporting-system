using CIDRS.API.Contracts.V1;
using CIDRS.API.Controllers.Base;
using CIDRS.Core.Modules.Penalties.Commands;
using CIDRS.Shared.Common.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIDRS.API.Controllers.Penalties
{
    /// <summary>
    /// Penalty API Endpoints
    /// </summary>
    public class PenaltyController : ApiController
    {
        /// <summary>
        /// Resolve Penalty 
        /// </summary>
        /// <returns></returns>
        [HttpPost(ApiRoutes.Penalty.Resolve)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ResponseResult<bool>> RegisterChiefOccupantAsync(int id)
        {
            var result = await Mediator.Send(new ResolveCommand() {Id=id });
            return result;
        }
    }
}
