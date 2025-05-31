using CIDRS.Core.Common.Models;
using CIDRS.Core.Modules.Identity.Extensions;
using CIDRS.Identity.Services.User;
using CIDRS.Shared.Common.Api.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.Identity.Commands
{
    public class ConformEmailCommand:IRequest<ResponseResult<bool>>
    {
        
        /// <summary>
        /// Token
        /// </summary>
        [Required]
        public string Token { get; set; }        
    }

    public class ConformEmailCommandHandler : IRequestHandler<ConformEmailCommand, ResponseResult<bool>>
    {
        private readonly IIdentityService _identityService;

        public ConformEmailCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<ResponseResult<bool>> Handle(ConformEmailCommand request, CancellationToken cancellationToken)
        {
            var serviceRequest = request.ToServiceRequest();
            var response = await _identityService.ConformEmailAsync(serviceRequest);

            if (response.Status)
                return new ResponseResult<bool>()
                {
                    Errors = null,
                    Result = true,
                    Succeeded = true
                };

            return new ResponseResult<bool>()
            {
                Errors = response.Errors,
                Result = false,
                Succeeded = false
            };

        }
    }
}
