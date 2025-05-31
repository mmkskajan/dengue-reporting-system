using CIDRS.Core.Modules.Identity.Extensions;
using CIDRS.Identity.Models.Response;
using CIDRS.Identity.Services.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.Identity.Commands
{ 
    public class RefreshCommand : IRequest<AuthenticationResult>
    {
        /// <summary>
        /// Token
        /// </summary>
        [Required]
        public string Token { get; set; }
        /// <summary>
        /// Refresh Token
        /// </summary>
        [Required]
        public string RefreshToken { get; set; }
    }

    public class RefreshCommandHandler : IRequestHandler<RefreshCommand, AuthenticationResult>
    {
        private readonly IIdentityService _identityService;

        public RefreshCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<AuthenticationResult> Handle(RefreshCommand request, CancellationToken cancellationToken)
        {
            var refreshRequest = request.ToServiceRequest();
            var refreshResponse = await _identityService.RefreshAsync(refreshRequest);
            return refreshResponse;
        }
    }
}
