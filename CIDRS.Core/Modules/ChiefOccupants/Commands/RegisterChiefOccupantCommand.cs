using AutoMapper;
using CIDRS.Core.Common.Models;
using CIDRS.Core.Modules.ChiefOccupants.Extensions;
using CIDRS.Core.Modules.ChiefOccupants.Services;
using CIDRS.Core.Modules.ChiefOccupants.ViewModels;
using CIDRS.Identity.Infrastructure;
using CIDRS.Infrastructure;
using CIDRS.Shared.Common.Api.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.ChiefOccupants.Commands
{
    public class RegisterChiefOccupantCommand : IRequest<ResponseResult<ChiefOccupantVM>>
    {
        /// <summary>
        /// Full Name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Phone Number
        /// </summary>
        public string PhoenNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required, EmailAddress]
        public string Email { get; set; }
        public int DistrictId { get; set; }
        public int MohAreaId { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
    }

    public class RegisterChiefOccupantCommandHandler : IRequestHandler<RegisterChiefOccupantCommand, ResponseResult<ChiefOccupantVM>>
    {
        private readonly IChiefOccupantService _chiefOccupantService;
        private readonly IMapper _mapper;
        private readonly IdentityDataContext _identityContext;
        private readonly ApplicationDataContext _dataContext;

        public RegisterChiefOccupantCommandHandler(IChiefOccupantService chiefOccupantService, IMapper mapper, IdentityDataContext identityContext, ApplicationDataContext dataContext)
        {
            _chiefOccupantService = chiefOccupantService;
            _mapper = mapper;
            _identityContext = identityContext;
            _dataContext = dataContext;
        }
        public async Task<ResponseResult<ChiefOccupantVM>> Handle(RegisterChiefOccupantCommand request, CancellationToken cancellationToken)
        {
            var coRequest = request.ToServiceRequest();
            var response = await _chiefOccupantService.RegisterChiefOccupantAsync(coRequest);

            if (response.Succeeded)
            {
                var viewModel = await response.Result.ToViewModelAsync(_mapper, _identityContext, _dataContext);
                return new ResponseResult<ChiefOccupantVM>()
                {
                    Succeeded = true,
                    Errors = null,
                    Result = viewModel
                };
            }
            else
                return new ResponseResult<ChiefOccupantVM>()
                {
                    Succeeded = false,
                    Result = null,
                    Errors = response.Errors
                };
        }
    }
}
