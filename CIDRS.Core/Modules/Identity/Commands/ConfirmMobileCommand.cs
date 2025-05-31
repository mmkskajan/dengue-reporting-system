using CIDRS.Core.Common.Models;
using CIDRS.Identity.Services.User;
using CIDRS.Shared.Common.Api.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.Identity.Commands
{
    public class ConfirmMobileCommand : IRequest<ResponseResult<bool>>
    {
        public string Token { get; set; }
    }

    public class ConfirmMobileCommandHandler : IRequestHandler<ConfirmMobileCommand, ResponseResult<bool>>
    {
        private readonly IIdentityService _identityService;

        public ConfirmMobileCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<ResponseResult<bool>> Handle(ConfirmMobileCommand request, CancellationToken cancellationToken)
        {
            var response = await _identityService.ConformMobileAsync(request.Token);

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
