using AutoMapper;
using CIDRS.Core.Modules.PublicHealthInspectors.Extensions;
using CIDRS.Core.Modules.PublicHealthInspectors.Services;
using CIDRS.Core.Modules.PublicHealthInspectors.ViewModels;
using CIDRS.Identity.Infrastructure;
using CIDRS.Infrastructure;
using CIDRS.Shared.Common.Pagination.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.PublicHealthInspectors.Commands
{
    public class IndexPublicHealthInspctorCommand : IRequest<PaginationVM<PublicHealthInspectorVM>>
    {
        public string BasicSearchValue { get; set; }
        public int? DistrictId { get; set; }
        public int? MohAreaId { get; set; }
        public PaginationOption PaginationOption { get; set; }
    }

    public class IndexPublicHealthInspctorCommandHandler : IRequestHandler<IndexPublicHealthInspctorCommand, PaginationVM<PublicHealthInspectorVM>>
    {
        private readonly IPublicHealthInspectorService _publicHealthInspectorService;
        private readonly IMapper _mapper;
        private readonly IdentityDataContext _identityContext;
        private readonly ApplicationDataContext _dataContext;
        public IndexPublicHealthInspctorCommandHandler(IPublicHealthInspectorService publicHealthInspectorService, IMapper mapper, IdentityDataContext identityContext, ApplicationDataContext dataContext)
        {
            _publicHealthInspectorService = publicHealthInspectorService;
            _mapper = mapper;
            _identityContext = identityContext;
            _dataContext = dataContext;
        }
        public async Task<PaginationVM<PublicHealthInspectorVM>> Handle(IndexPublicHealthInspctorCommand request, CancellationToken cancellationToken)
        {
            var searxhRequest = request.ToServiceRequest();
           var response =  await _publicHealthInspectorService.IndexPublicHealthInspectorsAsync(searxhRequest);

            var viewModels =  response.Results.Select(x => x.ToViewModelAsync(_mapper, _identityContext, _dataContext).Result).ToList();

            return new PaginationVM<PublicHealthInspectorVM>()
            {
                DisplayCount = response.DisplayCount,
                DisplayEnd = response.DisplayEnd,
                DisplayStart = response.DisplayStart,
                NumberOfPages = response.NumberOfPages,
                Page = response.Page,
                TotalItems = response.TotalItems,
                PageSize = response.PageSize,
                Results = viewModels
            };
        }
    }
}
