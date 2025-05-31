using CIDRS.API.Contracts.V1;
using CIDRS.API.Controllers.Base;
using CIDRS.Core.Modules.Polices.Commands;
using CIDRS.Core.Modules.Polices.Models.Response;
using CIDRS.Core.Modules.Polices.Queries;
using CIDRS.Shared.Common.Api.Models;
using CIDRS.Shared.Common.Pagination.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIDRS.API.Controllers.Polices
{
    /// <summary>
    /// API Endpoints of Police
    /// </summary>
    public class PoliceController : ApiController
    {
        /// <summary>
        /// Create Police Endpoint
        /// </summary>
        /// <returns></returns>
        [HttpPost(ApiRoutes.Police.Create)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ResponseResult<PoliceVM>> CreatePoliceAsync(CreatePoliceCommand command)
        {
            var result = await Mediator.Send(command);
            return result;
        }

        /// <summary>
        /// Get Police Endpoint
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.Police.GetAPolice)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ResponseResult<PoliceVM>> GetPoliceAsync(int id)
        {
            var result = await Mediator.Send(new GetPoliceQuery() { Id = id});
            return result;
        }

        /// <summary>
        /// Lookup Police
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.Police.Lookup)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ResponseResult<List<PoliceVM>>> LookupPhiAsync(int mohAreaId)
        {
            var result = await Mediator.Send(new PoliceLookupCommand() { mohAreaId = mohAreaId });
            return result;
        }

        /// <summary>
        /// Index Police / Search Police
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.Police.IndexPolices)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<PaginationVM<PoliceVM>> IndexPhiAsync(string searchString = "", int? mohAreaId = null, int page = 1, int pageSize = 10)
        {
            var command = new IndexPolicesCommand()
            {
                BasicSearchValue = searchString,
                MohAreaId = mohAreaId,
                PaginationOption = new PaginationOption() { Page = page, PageSize = pageSize }
            };
            var result = await Mediator.Send(command);
            return result;
        }
    }
}
