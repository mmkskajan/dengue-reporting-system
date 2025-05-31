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
    public class ChangePasswordRequestCommand : IRequest<ResponseResult<bool>>
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required, EmailAddress]
        public string Email { get; set; }
        /// <summary>
        /// Current Password
        /// </summary>
        [Required]
        public string currentPassword { get; set; }
        /// <summary>
        /// New Password
        /// </summary>
        [Required]
        public string newPassword { get; set; }
    }

    public class ChangePasswordRequestCommandHandler : IRequestHandler<ChangePasswordRequestCommand, ResponseResult<bool>>
    {
        private readonly IIdentityService _identityService;

        public ChangePasswordRequestCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<ResponseResult<bool>> Handle(ChangePasswordRequestCommand request, CancellationToken cancellationToken)
        {
            var changePasswordRequest = request.ToServiceRequest();
            var response = await _identityService.ChangePasswordAsync(changePasswordRequest);

            if(response.Status)
            {
                return new ResponseResult<bool>()
                {
                    Errors = null,
                    Result = true,
                    Succeeded= true
                };
            }

            return new ResponseResult<bool>()
            {
                Errors = response.Errors,
                Succeeded = false,
                Result = false
            };
        }
    }
}
