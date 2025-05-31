using AutoMapper;
using CIDRS.Core.Modules.WorkItems.Extensions;
using CIDRS.Core.Modules.WorkItems.Services;
using CIDRS.Core.Modules.WorkItems.ViewModels;
using CIDRS.Domain.Enums;
using CIDRS.Shared.Common.Pagination.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.WorkItems.Commands
{
    public class IndexWorkItemCommand : IRequest<PaginationVM<WorkItemVM>>
    {
        public bool? IsActive { get; set; }
        public WorkItemType? Type { get; set; }
        public int? AsigneeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public PaginationOption PaginationOption { get; set; }
    }

    public class IndexWorkItemCommandHandler : IRequestHandler<IndexWorkItemCommand, PaginationVM<WorkItemVM>>
    {
        private readonly IWorkItemService _workItemService;
        private readonly IMapper _mapper;


        public IndexWorkItemCommandHandler(IWorkItemService workItemService, IMapper mapper)
        {
            _workItemService = workItemService;
            _mapper = mapper;            
        }
        public async Task<PaginationVM<WorkItemVM>> Handle(IndexWorkItemCommand request, CancellationToken cancellationToken)
        {
            var searchRequest = request.ToServiceRequest();
            var response = await _workItemService.IndexWorkItemsAsync(searchRequest);

            var viewModels = response.Results.Select(x => x.ToViewModel(_mapper)).ToList();

            return new PaginationVM<WorkItemVM>()
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
