using CIDRS.Identity.Services.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.Identity.Commands
{
    public class ValidatePhoneNumberCommand:IRequest<bool>
    {
        public string PhoneNumber { get; set; }

    }
    public class ValidatePhoneNumberCommandHandler : IRequestHandler<ValidatePhoneNumberCommand, bool>
    {
        private readonly IIdentityService _identityService;

        public ValidatePhoneNumberCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<bool> Handle(ValidatePhoneNumberCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.IsValidPhoneNumber(request.PhoneNumber);

        }
    }
}
