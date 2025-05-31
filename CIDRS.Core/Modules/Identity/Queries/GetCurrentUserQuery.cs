using AutoMapper;
using CIDRS.Core.Common.Models;
using CIDRS.Core.Modules.ChiefOccupants.Services;
using CIDRS.Domain.Enums;
using CIDRS.Identity.Services.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.Identity.Queries
{
    public class GetCurrentUserQuery : IRequest<UserVM>
    {
    }
    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserVM>
    {
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;
        private readonly IChiefOccupantService _chiefOccupantService;

        public GetCurrentUserQueryHandler(IIdentityService identityService,IMapper mapper,IChiefOccupantService chiefOccupantService)
        {
            _identityService = identityService;
            _mapper = mapper;
            _chiefOccupantService = chiefOccupantService;
        }
        public async Task<UserVM> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _identityService.GetCurrentUserAsync();
            var userVM =  _mapper.Map<UserVM>(user);
            userVM.IsActive = await IsUserActiveAsync(userVM);
            return userVM;
        }

        private async Task<bool> IsUserActiveAsync(UserVM user)
        {
            var chifOccupant = await _chiefOccupantService.GetChiefOccupantByIdentityId(user.Id);
            return user.UserType != ApplicationUserType.ChiefOccupant || (user.UserType == ApplicationUserType.ChiefOccupant && chifOccupant.ReportingApplications.Any(x => x.Type == Domain.Enums.ApplicationType.Base && x.Status == Domain.Enums.ApplicationStatus.Completed));


        }
    }
}
