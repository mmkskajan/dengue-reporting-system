using AutoMapper;
using CIDRS.Core.Common.Models;
using CIDRS.Identity.Services.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.Identity.Commands
{
    public class SetAvatarCommand : IRequest<UserVM>
    {
        public string UserId { get; set; }
        public IFormFile avatar { get; set; }
    }

    public class SetAvatarCommandHandler : IRequestHandler<SetAvatarCommand, UserVM>
    {
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public SetAvatarCommandHandler(IIdentityService identityService, IMapper mapper)
        {
            _identityService = identityService;
            _mapper = mapper;
        }
        public async Task<UserVM> Handle(SetAvatarCommand request, CancellationToken cancellationToken)
        {
            var avatarResponse = await _identityService.SetAvatarAsync(request.UserId, request.avatar);
            return _mapper.Map<UserVM>(avatarResponse);
        }
    }
}
