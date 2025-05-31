using AutoMapper;
using CIDRS.Core.Modules.Polices.Extensions;
using CIDRS.Core.Modules.Polices.Models.Response;
using CIDRS.Core.Modules.Polices.Services;
using CIDRS.Infrastructure;
using CIDRS.Shared.Common.Pagination.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.Polices.Commands
{
    public class IndexPolicesCommand : IRequest<PaginationVM<PoliceVM>>
    {
        public string BasicSearchValue { get; set; }
        public int? MohAreaId { get; set; }
        public PaginationOption PaginationOption { get; set; }
    }

    public class IndexPolicesCommandHandler : IRequestHandler<IndexPolicesCommand, PaginationVM<PoliceVM>>
    {
        private readonly IPoliceService _policeService;
        private readonly IMapper _mapper;
        private readonly ApplicationDataContext _dataContext;
        public IndexPolicesCommandHandler(IPoliceService policeService, IMapper mapper, ApplicationDataContext dataContext)
        {
            _policeService = policeService;
            _mapper = mapper;
            _dataContext = dataContext;
        }
        public async Task<PaginationVM<PoliceVM>> Handle(IndexPolicesCommand request, CancellationToken cancellationToken)
        {
            var searxhRequest = request.ToServiceRequest();
            var response = await _policeService.IndexPolicesAsync(searxhRequest);

            var viewModels = response.Results.Select(x => _mapper.Map<PoliceVM>(x)).ToList();

            return new PaginationVM<PoliceVM>()
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
