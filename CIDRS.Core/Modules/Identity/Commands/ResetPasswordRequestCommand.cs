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
    public class ResetPasswordRequestCommand : IRequest<ResponseResult<bool>>
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required, EmailAddress]
        public string Email { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        [Required]
        public string Token { get; set; }
        /// <summary>
        /// new password
        /// </summary>
        [Required]
        public string newPassword { get; set; }
    }

    public class ResetPasswordRequestCommandHandler : IRequestHandler<ResetPasswordRequestCommand, ResponseResult<bool>>
    {
        private readonly IIdentityService _identityService;

        public ResetPasswordRequestCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<ResponseResult<bool>> Handle(ResetPasswordRequestCommand request, CancellationToken cancellationToken)
        {
            var passwordResetRequest = request.ToServiceRequest();
            var resetResponse = await _identityService.ResetPasswordAsync(passwordResetRequest);

            if(resetResponse.Status)
            {
                return new ResponseResult<bool>()
                {
                    Errors = null,
                    Result = true,
                    Succeeded = true
                };
            }

            return new ResponseResult<bool>()
            {
                Errors = resetResponse.Errors,
                Result = false,
                Succeeded = false
            };
        }
    }
}
