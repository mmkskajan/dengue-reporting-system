using CIDRS.API.Contracts.V1;
using CIDRS.API.Controllers.Base;
using CIDRS.Core.Common.Models;
using CIDRS.Core.Modules.PublicHealthInspectors.Commands;
using CIDRS.Core.Modules.PublicHealthInspectors.Queries;
using CIDRS.Core.Modules.PublicHealthInspectors.ViewModels;
using CIDRS.Shared.Common.Api.Models;
using CIDRS.Shared.Common.Pagination.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIDRS.API.Controllers.PublicHealthInspectors
{
 
    /// <summary>
    /// API Endpoint Collection of Public Health Inspectors
    /// </summary>
    public class PhiController : ApiController
    {

        /// <summary>
        /// Register PHI By Admin
        /// </summary>
        /// <returns></returns>
        [HttpPost(ApiRoutes.PublicHealthInspector.RegisterPhiByAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ResponseResult<PublicHealthInspectorVM>> RegisterPhiAsync(RegisterPhiCommand command)
        {
            var result = await Mediator.Send(command);
            return result;
        }

        /// <summary>
        /// Index PHI / Search PHI
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.PublicHealthInspector.IndexPhis)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<PaginationVM<PublicHealthInspectorVM>> IndexPhiAsync(string searchString ="", int? districtId = null, int? mohAreaId = null, int page = 1, int pageSize =10)
        {
            var command = new IndexPublicHealthInspctorCommand()
            {
                BasicSearchValue = searchString,
                DistrictId = districtId,
                MohAreaId = mohAreaId,
                PaginationOption = new PaginationOption() { Page = page, PageSize = pageSize }
            };
            var result = await Mediator.Send(command);
            return result;
        }

        /// <summary>
        /// View PHI
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.PublicHealthInspector.ViewPhi)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ResponseResult<PublicHealthInspectorVM>> ViewPhiAsync(int id)
        {
            var result = await Mediator.Send(new GetPublicHealthInspectorQuery() {Id=id});
            return result;
        }

        /// <summary>
        /// Lookup PHI
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.PublicHealthInspector.Lookup)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ResponseResult<List<PublicHealthInspectorVM>>> LookupPhiAsync(int districtId, int mohAreaId)
        {
            var result = await Mediator.Send(new PhiLookupCommand() { districtId = districtId, mohAreaId = mohAreaId });
            return result;
        }
    }
}
