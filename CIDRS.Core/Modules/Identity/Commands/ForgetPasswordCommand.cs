using CIDRS.Core.Common.Models;
using CIDRS.Identity.Infrastructure;
using CIDRS.Identity.Models.Response;
using CIDRS.Identity.Services.User;
using CIDRS.Shared.Common.Api.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.Identity.Commands
{
    /// <summary>
    /// Token
    /// </summary>

    public class ForgetPasswordCommand : IRequest<ResponseResult<bool>>
    {
        public string Email { get; set; }

    }

    public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, ResponseResult<bool>>
    {
        private readonly IIdentityService _identityService;
        public ForgetPasswordCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<ResponseResult<bool>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var forgetPasswordResponse = await _identityService.forgotPasswordAsync(request.Email);

            if (forgetPasswordResponse.Status)
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
                Errors =forgetPasswordResponse.Errors,
                Succeeded = false,
                Result= false
            };
        }
    }


}
