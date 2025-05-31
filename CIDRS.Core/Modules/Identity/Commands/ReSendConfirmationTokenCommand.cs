using CIDRS.Identity.Services.User;
using CIDRS.Shared.Common.Api.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.Identity.Commands
{
    public enum ConfirmationMedia
    {
        Mobile = 1,
        Email = 2
    }
    public class ReSendConfirmationTokenCommand : IRequest<ResponseResult<bool>>
    {
        public ConfirmationMedia confirmationMedia { get; set; }
    }

    public class ReSendConfirmationTokenCommandHandler : IRequestHandler<ReSendConfirmationTokenCommand, ResponseResult<bool>>
    {
        private readonly IIdentityService _identityService;

        public ReSendConfirmationTokenCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<ResponseResult<bool>> Handle(ReSendConfirmationTokenCommand request, CancellationToken cancellationToken)
        {
            switch (request.confirmationMedia)
            {
                case ConfirmationMedia.Mobile:
                    return await _identityService.ResendMobileConfirmationTokenAsync();
                case ConfirmationMedia.Email:
                    return await _identityService.ResendEmailConfirmationTokenAsync();
                default:
                    return new ResponseResult<bool>()
                    {
                        Result = false,
                        Errors = new[] { "Invalid media request for confirmation token"},
                        Succeeded = false
                    };
            }
        }
    }
}
