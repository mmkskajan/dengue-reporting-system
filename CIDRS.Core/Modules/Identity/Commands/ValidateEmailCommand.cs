using CIDRS.Identity.Services.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.Identity.Commands
{
    public class ValidateEmailCommand :IRequest<bool>
    {
        public string Email { get; set; }
    }

    public class ValidateEmailCommandHandler : IRequestHandler<ValidateEmailCommand, bool>
    {
        private readonly IIdentityService _identityService;

        public ValidateEmailCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<bool> Handle(ValidateEmailCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.IsValidEmail(request.Email);
        }
    }
}
