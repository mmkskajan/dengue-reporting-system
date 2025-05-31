using AutoMapper;
using CIDRS.Core.Common.Models;
using CIDRS.Core.Modules.PublicHealthInspectors.Extensions;
using CIDRS.Core.Modules.PublicHealthInspectors.Services;
using CIDRS.Core.Modules.PublicHealthInspectors.ViewModels;
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

namespace CIDRS.Core.Modules.PublicHealthInspectors.Commands
{
    public class RegisterPhiCommand : IRequest<ResponseResult<PublicHealthInspectorVM>>
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
    }

    public class RegisterPhiCommandHandler : IRequestHandler<RegisterPhiCommand, ResponseResult<PublicHealthInspectorVM>>
    {
        private readonly IPublicHealthInspectorService _publicHealthInspectorService;
        private readonly IMapper _mapper;
        private readonly IdentityDataContext _identityContext;
        private readonly ApplicationDataContext _dataContext;

        public RegisterPhiCommandHandler(IPublicHealthInspectorService publicHealthInspectorService, IMapper mapper,IdentityDataContext identityContext,ApplicationDataContext dataContext)
        {
            _publicHealthInspectorService = publicHealthInspectorService;
            _mapper = mapper;
            _identityContext = identityContext;
            _dataContext = dataContext;
        }
        public async Task<ResponseResult<PublicHealthInspectorVM>> Handle(RegisterPhiCommand request, CancellationToken cancellationToken)
        {
            var phiRequest = request.ToCommand();
            var response = await _publicHealthInspectorService.RegisterPublicHealthInspectorsAsync(phiRequest);

            if (response.Succeeded)
            {
                var viewModel = await response.Result.ToViewModelAsync(_mapper, _identityContext,_dataContext);
                return new ResponseResult<PublicHealthInspectorVM>()
                {
                    Succeeded = true,
                    Errors = null,
                    Result = viewModel
                };
            }
            else
                return new ResponseResult<PublicHealthInspectorVM>()
                {
                    Succeeded = false,
                    Result = null,
                    Errors = response.Errors 
                };
            

        }
    }

}
