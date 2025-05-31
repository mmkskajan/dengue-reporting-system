using CIDRS.Core.Modules.ChiefOccupants.Services;
using CIDRS.Identity.Domain.Enums;
using CIDRS.Identity.Domain.Models.Entity;
using CIDRS.Identity.Infrastructure;
using CIDRS.Identity.Models.Request;
using CIDRS.Identity.Models.Response;
using CIDRS.Identity.Services.User;
using CIDRS.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.Identity.Commands
{
    public class LogInRequestCommand : IRequest<AuthenticationResult>
    {
        /// <summary>
        /// Username
        /// </summary>
        [Required]
        public string Username { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        [Required]
        public string Password { get; set; }
    }

    public class LogInRequestCommandHandler : IRequestHandler<LogInRequestCommand, AuthenticationResult>
    {
        private readonly IdentityDataContext _identityContext;
        private readonly IIdentityService _identityService;
        private readonly IChiefOccupantService _chiefOccupantService;

        public LogInRequestCommandHandler(IdentityDataContext identityContext,IIdentityService identityService, IChiefOccupantService chiefOccupantService)
        {
            _identityContext = identityContext;
            _identityService = identityService;
            _chiefOccupantService = chiefOccupantService;
        }
        public async Task<AuthenticationResult> Handle(LogInRequestCommand request, CancellationToken cancellationToken)
        {
            
            var user = await _identityContext.Users.FirstOrDefaultAsync(x => x.Email == request.Username || x.UserName == request.Username || x.PhoneNumber == request.Username);

            if(user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist" }
                };
            }

            var userLogInRequest = GetLogInRequest(user, request);

            var authResult = await _identityService.LoginAsync(userLogInRequest);
            if(authResult.Status)
            {
                authResult.User.IsActive = await IsUserActiveAsync(authResult.User);
            }

            return authResult;
        }

        private UserLoginRequest GetLogInRequest(ApplicationUser user, LogInRequestCommand request)
        {
            return new UserLoginRequest()
            {
                Email = user.Email,
                Password = request.Password
            };
        }

        private async Task<bool> IsUserActiveAsync(ApplicationUserVM user)
        {
            var chifOccupant = await _chiefOccupantService.GetChiefOccupantByIdentityId(user.Id);
           return user.UserType != ApplicationUserType.ChiefOccupant || (user.UserType == ApplicationUserType.ChiefOccupant && chifOccupant.ReportingApplications.Any(x => x.Type == Domain.Enums.ApplicationType.Base && x.Status == Domain.Enums.ApplicationStatus.Completed));
            
           
        }
    }

}
