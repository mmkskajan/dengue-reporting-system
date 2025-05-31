using AutoMapper;
using CIDRS.Core.Modules.ChiefOccupants.Extensions;
using CIDRS.Core.Modules.ChiefOccupants.Services;
using CIDRS.Core.Modules.ChiefOccupants.ViewModels;
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

namespace CIDRS.Core.Modules.ChiefOccupants.Commands
{
    public class IndexChiefOccupantCommand : IRequest<PaginationVM<ChiefOccupantVM>>
    {
        public string BasicSearchValue { get; set; }
        public int? DistrictId { get; set; }
        public int? MohAreaId { get; set; }
        public int? PhiId { get; set; }
        public PaginationOption PaginationOption { get; set; }
    }

    public class IndexChiefOccupantCommandHandler : IRequestHandler<IndexChiefOccupantCommand, PaginationVM<ChiefOccupantVM>>
    {
        private readonly IMapper _mapper;
        private readonly IdentityDataContext _identityContext;
        private readonly ApplicationDataContext _dataContext;
        private readonly IChiefOccupantService _chiefOccupantService;

        public IndexChiefOccupantCommandHandler(IChiefOccupantService chiefOccupantService, IMapper mapper, IdentityDataContext identityContext, ApplicationDataContext dataContext)
        {
            _chiefOccupantService = chiefOccupantService;
            _mapper = mapper;
            _identityContext = identityContext;
            _dataContext = dataContext;
        }
        public async Task<PaginationVM<ChiefOccupantVM>> Handle(IndexChiefOccupantCommand request, CancellationToken cancellationToken)
        {
            var searxhRequest = request.ToServiceRequest();
            var response = await _chiefOccupantService.IndexChiefOccupantsAsync(searxhRequest);

            var viewModels = response.Results.Select(x => x.ToViewModelAsync(_mapper, _identityContext, _dataContext).Result).ToList();

            return new PaginationVM<ChiefOccupantVM>()
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
