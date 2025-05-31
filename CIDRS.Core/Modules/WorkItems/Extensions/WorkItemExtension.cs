using AutoMapper;
using CIDRS.Core.Modules.WorkItems.Commands;
using CIDRS.Core.Modules.WorkItems.Models.Request;
using CIDRS.Core.Modules.WorkItems.ViewModels;
using CIDRS.Domain.Models.Entity.WorkItems;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.WorkItems.Extensions
{
    public static class WorkItemExtension
    {
        public static WorkItemSearchRequest ToServiceRequest(this IndexWorkItemCommand command)
        {
            return new WorkItemSearchRequest()
            {               
                PaginationOption = command.PaginationOption,
                AsigneeId = command.AsigneeId,
                EndDate = command.EndDate,
                IsActive = command.IsActive,
                StartDate = command.StartDate,
                Type = command.Type
            };
        }

        public static WorkItemVM ToViewModel(this WorkItem workItem, IMapper mapper)
        {
            var viewModel = mapper.Map<WorkItemVM>(workItem);
            return viewModel;
        }
    }
}
