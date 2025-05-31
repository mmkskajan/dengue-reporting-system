using CIDRS.Core.Common.Models;
using CIDRS.Core.Modules.Penalties.Services;
using CIDRS.Core.Modules.Polices.Services;
using CIDRS.Core.Modules.PublicHealthInspectors.Services;
using CIDRS.Core.Modules.WorkItems.Models.Request;
using CIDRS.Core.Modules.WorkItems.ViewModels;
using CIDRS.Domain.Enums;
using CIDRS.Domain.Models.Entity.ChiefOccupants;
using CIDRS.Domain.Models.Entity.Polices;
using CIDRS.Domain.Models.Entity.PublicHealthInspectors;
using CIDRS.Domain.Models.Entity.WorkItems;
using CIDRS.Identity.Domain.Models.Entity;
using CIDRS.Identity.Services.User;
using CIDRS.Infrastructure;
using CIDRS.Shared.Common.Api.Models;
using CIDRS.Shared.Common.Extensions;
using CIDRS.Shared.Common.Pagination.Extensions;
using CIDRS.Shared.Common.Pagination.Models;
using CIDRS.Shared.Utility.EmailManipulator.Extensions;
using CIDRS.Shared.Utility.EmailManipulator.Models;
using CIDRS.Shared.Utility.EmailManipulator.Services;
using CIDRS.Shared.Utility.SmsManipulator.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.WorkItems.Services
{
    public class WorkItemService : IWorkItemService
    {
        private readonly ApplicationDataContext _dataContext;
        private readonly IPublicHealthInspectorService _publicHealthInspectorService;
        private readonly IIdentityService _identityService;
        private readonly IPoliceService _policeService;
        private readonly ISmsManipulatorService _smsSender;
        private readonly IEmailSenderService _emailSender;
        private readonly IPenaltyService _penaltyService;

        public WorkItemService(ApplicationDataContext dataContext, IPublicHealthInspectorService publicHealthInspectorService, IIdentityService identityService, IPoliceService policeService, ISmsManipulatorService smsSender, IEmailSenderService emailSender, IPenaltyService penaltyService)
        {
            _dataContext = dataContext;
            _publicHealthInspectorService = publicHealthInspectorService;
            _identityService = identityService;
            _policeService = policeService;
            _smsSender = smsSender;
            _emailSender = emailSender;
            _penaltyService = penaltyService;
        }

        public async Task<ResponseResult<WorkItem>> CreateWorkItemAsync(WorkItemType type, int relativeId)
        {
            var workItem = GetNewWorkItem(type, relativeId);
            await _dataContext.WorkItems.AddAsync(workItem);
            await _dataContext.SaveChangesAsync();

            return new ResponseResult<WorkItem>()
            {
                Succeeded = true,
                Errors = null,
                Result = workItem
            };

        }

        public async Task<ResponseResult<WorkItem>> GetWorkitemAsync(int id)
        {
            var workItem = await _dataContext.WorkItems.Include(x => x.Application)
                                                            .ThenInclude(x => x.SurroundingSets)
                                                                .ThenInclude(x => x.RelativeSurroundingSet)
                                                        .Include(x => x.Application)
                                                            .ThenInclude(x => x.ChiefOccupant)
                                                       .Include(x => x.ChiefOccupant)
                                                            .ThenInclude(x => x.District)
                                                       .Include(x => x.ChiefOccupant)
                                                            .ThenInclude(x => x.MohArea)
                                                       .Include(x => x.ChiefOccupant)
                                                            .ThenInclude(x => x.RespectivePhi)
                                                                .ThenInclude(x => x.MohArea)
                                                       .Include(x => x.ChiefOccupant)
                                                            .ThenInclude(x => x.RespectivePhi)
                                                                .ThenInclude(x => x.District)
                                                                .Include(x => x.ChiefOccupant)
                                                        .ThenInclude(x => x.RespectivePolice)
                                                            .ThenInclude(x => x.PoliceStation)
                                                       .Include(x => x.WorkItemActions)
                                                            .ThenInclude(x => x.AssignTo)
                                                                .ThenInclude(x => x.District)
                                                        .Include(x => x.WorkItemActions)
                                                            .ThenInclude(x => x.AssignTo)
                                                                .ThenInclude(x => x.MohArea)
                                                        .Include(x => x.WorkItemRemarks)
                                                       .FirstOrDefaultAsync(x => x.Id == id);

            if (workItem == null)
                return new ResponseResult<WorkItem>()
                {
                    Errors = new[] { "No WorkItem Found for given Id!!" },
                    Result = null,
                    Succeeded = false
                };

            return new ResponseResult<WorkItem>()
            {
                Succeeded = true,
                Result = workItem,
                Errors = null
            };

        }

        public async Task<PaginationVM<WorkItem>> IndexWorkItemsAsync(WorkItemSearchRequest searchRequest)
        {
            var currentUser = await _identityService.GetCurrentUserAsync();

            var phi = (currentUser != null && currentUser.UserType == CIDRS.Identity.Domain.Enums.ApplicationUserType.Phi) ? await _publicHealthInspectorService.ViewPublicHealthInspectorsAsync(currentUser.Id) : null;

            if (phi != null)
                searchRequest.AsigneeId = phi.Id;

            IQueryable<WorkItem> workItems = GetBasicSearchResult(searchRequest);

            return await workItems.PaginateAsync(searchRequest?.PaginationOption?.Page, searchRequest?.PaginationOption?.PageSize);

        }

        public async Task<ResponseResult<bool>> ReAsssignAsync(int id, int assigneeId, int policeId)
        {
            var workItem = await _dataContext.WorkItems.Include(x => x.ChiefOccupant)
                                                       .Include(x => x.WorkItemActions).FirstOrDefaultAsync(y => y.Id == id);
            if (workItem == null)
                return new ResponseResult<bool>()
                {
                    Errors = new[] { "No WorkItem Found with given Id!!" },
                    Result = false,
                    Succeeded = false
                };

            if (workItem.Type != WorkItemType.CORegistration)
                return new ResponseResult<bool>()
                {
                    Errors = new[] { "This WorkItem Not support to ReAssign!!" },
                    Result = false,
                    Succeeded = false
                };

            if (workItem.Status != WorkItemStatus.Active)
                return new ResponseResult<bool>()
                {
                    Errors = new[] { "In Active WorKIem!!", "This WorkItem Not support to ReAssign!!" },
                    Result = false,
                    Succeeded = false
                };

            var assigneeResponse = await _publicHealthInspectorService.ViewPublicHealthInspectorsAsync(assigneeId);

            if (!assigneeResponse.Succeeded)
                return new ResponseResult<bool>()
                {
                    Errors = assigneeResponse.Errors,
                    Result = false,
                    Succeeded = false
                };

            if (workItem.ChiefOccupant.DistrictId != assigneeResponse.Result.DistrictId || workItem.ChiefOccupant.MohAreaId != assigneeResponse.Result.MohAreaId)
                return new ResponseResult<bool>()
                {
                    Errors = new[] { "Assignee Not Relervent to Chief Occupant!!" },
                    Result = false,
                    Succeeded = false
                };

            var policeResponse = await _policeService.GetPoliceAsync(policeId);
            if (!policeResponse.Succeeded)
                return new ResponseResult<bool>()
                {
                    Errors = policeResponse.Errors,
                    Result = false,
                    Succeeded = false
                };

            if (!policeResponse.Result.PoliceStation.MohAreaPoliceStations.Select(x => x.MohAreaId).Contains(workItem.ChiefOccupant.MohAreaId))
                return new ResponseResult<bool>()
                {
                    Errors = new[] { "Police Not Relervent to Chief Occupant!!" },
                    Result = false,
                    Succeeded = false
                };

            using (var transaction = _dataContext.Database.BeginTransaction())
            {
                try
                {
                    var workAction = GetNewWorkItemAction(WorkItemActionType.Assign, assigneeId);
                    workItem.WorkItemActions.Add(workAction);
                    _dataContext.WorkItems.Update(workItem);
                    await _dataContext.SaveChangesAsync();

                    workItem.ChiefOccupant.PhiId = assigneeId;
                    workItem.ChiefOccupant.PoliceId = policeId;
                    _dataContext.ChiefOccupants.Update(workItem.ChiefOccupant);
                    await _dataContext.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    return new ResponseResult<bool>()
                    {
                        Errors = new[] { ex.Message },
                        Result = false,
                        Succeeded = false
                    };
                }
            }

            await NotifyAssigneesAsync(assigneeResponse.Result,policeResponse.Result,workItem.ChiefOccupant);

            return new ResponseResult<bool>()
            {
                Errors = null,
                Result = true,
                Succeeded = true
            };


        }


        public async Task<ResponseResult<bool>> ReAsssignAsync(int id, int assigneeId)
        {
            var workItem = await _dataContext.WorkItems.Include(x => x.Application)
                                                            .ThenInclude(x => x.ChiefOccupant)
                                                       .Include(x => x.WorkItemActions).FirstOrDefaultAsync(y => y.Id == id);
            if (workItem == null)
                return new ResponseResult<bool>()
                {
                    Errors = new[] { "No WorkItem Found with given Id!!" },
                    Result = false,
                    Succeeded = false
                };

            if (workItem.Type != WorkItemType.ReportingApplication)
                return new ResponseResult<bool>()
                {
                    Errors = new[] { "This WorkItem Not support to Auto Assign!!" },
                    Result = false,
                    Succeeded = false
                };

            if (workItem.Status != WorkItemStatus.Active)
                return new ResponseResult<bool>()
                {
                    Errors = new[] { "In Active WorKIem!!", "This WorkItem Not support to Auto Assign!!" },
                    Result = false,
                    Succeeded = false
                };

            var assigneeResponse = await _publicHealthInspectorService.ViewPublicHealthInspectorsAsync(assigneeId);

            if (!assigneeResponse.Succeeded)
                return new ResponseResult<bool>()
                {
                    Errors = assigneeResponse.Errors,
                    Result = false,
                    Succeeded = false
                };

            if (workItem.Application.ChiefOccupant.DistrictId != assigneeResponse.Result.DistrictId || workItem.Application.ChiefOccupant.MohAreaId != assigneeResponse.Result.MohAreaId)
                return new ResponseResult<bool>()
                {
                    Errors = new[] { "Assignee Not Relervent to Chief Occupant!!" },
                    Result = false,
                    Succeeded = false
                };

           

            using (var transaction = _dataContext.Database.BeginTransaction())
            {
                try
                {
                    var workAction = GetNewWorkItemAction(WorkItemActionType.Assign, assigneeId);
                    workItem.WorkItemActions.Add(workAction);
                    _dataContext.WorkItems.Update(workItem);
                    await _dataContext.SaveChangesAsync();                    

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    return new ResponseResult<bool>()
                    {
                        Errors = new[] { ex.Message },
                        Result = false,
                        Succeeded = false
                    };
                }
            }

            await NotifyAssigneesAsync(assigneeResponse.Result, workItem.Application.ChiefOccupant, workItem.Application.Type);

            return new ResponseResult<bool>()
            {
                Errors = null,
                Result = true,
                Succeeded = true
            };


        }

        public async Task<ResponseResult<bool>> AddRemarkAsync(int id, string remark)
        {
            if (string.IsNullOrWhiteSpace(remark))
                return new ResponseResult<bool>()
                {
                    Errors = new[] { "Remark Can't be Empty!!" },
                    Result = false,
                    Succeeded = false
                };

            var workItem = await _dataContext.WorkItems.Include(x => x.ChiefOccupant)
                                                       .Include(x => x.WorkItemRemarks).FirstOrDefaultAsync(y => y.Id == id);
            if (workItem == null)
                return new ResponseResult<bool>()
                {
                    Errors = new[] { "No WorkItem Found with given Id!!" },
                    Result = false,
                    Succeeded = false
                };

            if (workItem.Status != WorkItemStatus.Active)
                return new ResponseResult<bool>()
                {
                    Errors = new[] { "In Active WorKIem!!", "This WorkItem Not support to Add Remarks!!" },
                    Result = false,
                    Succeeded = false
                };

            var currentUser = await _identityService.GetCurrentUserAsync();

            using (var transaction = _dataContext.Database.BeginTransaction())
            {
                try
                {
                    var workItemRemark = GetNewWorkItemRemark(remark, currentUser.FullName);
                    workItem.WorkItemRemarks.Add(workItemRemark);
                    _dataContext.WorkItems.Update(workItem);
                    await _dataContext.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    return new ResponseResult<bool>()
                    {
                        Errors = new[] { ex.Message },
                        Result = false,
                        Succeeded = false
                    };
                }
            }

            return new ResponseResult<bool>()
            {
                Errors = null,
                Result = true,
                Succeeded = true
            };


        }

        public async Task<ResponseResult<bool>> ApproveCORegistrationAsync(int id)
        {
            var response =await GetWorkitemAsync(id);

            if (!response.Succeeded)
                return new ResponseResult<bool>()
                {
                    Errors = response.Errors,
                    Succeeded = response.Succeeded,
                    Result = false
                };
            var workItem = response.Result;

            if (workItem.Status != WorkItemStatus.Active)
                return GetErrorResult<bool>("Work Item Already Actioned!!");

            if (workItem.Type != WorkItemType.CORegistration)
                return GetErrorResult<bool>("Work Item Type Not Supported!!");

            var action = GetNewWorkItemAction(WorkItemActionType.Approve);
            workItem.WorkItemActions.Add(action);
            workItem.Status = WorkItemStatus.Approved;
            workItem.SetUpdatedTime();

            _dataContext.WorkItems.Update(workItem);
            await _dataContext.SaveChangesAsync();

            await NotifyChiefcOcupantAsync(workItem.ChiefOccupant, ChiefOccupantNotification.ApproveCoRegistration);

            return new ResponseResult<bool>()
            {
                Errors = null,
                Result = true,
                Succeeded = true
            };           

        }

        public async Task<ResponseResult<bool>> ApproveAsync(int id)
        {
            var response = await GetWorkitemAsync(id);

            if (!response.Succeeded)
                return new ResponseResult<bool>()
                {
                    Errors = response.Errors,
                    Succeeded = response.Succeeded,
                    Result = false
                };
            var workItem = response.Result;

            if (workItem.Status != WorkItemStatus.Active)
                return GetErrorResult<bool>("Work Item Already Actioned!!");

            if (workItem.Type != WorkItemType.ReportingApplication)
                return GetErrorResult<bool>("Work Item Type Not Supported!!");

            var currentUser = await _identityService.GetCurrentUserAsync();

            if (!(currentUser.UserType == CIDRS.Identity.Domain.Enums.ApplicationUserType.Admin || currentUser.UserType == CIDRS.Identity.Domain.Enums.ApplicationUserType.Phi))
                return GetErrorResult<bool>("User not authorized to review the work item!!");

            var assignee = workItem.WorkItemActions.OrderByDescending(x => x.CreatedAt).FirstOrDefault(x => x.Type == WorkItemActionType.Assign);

            if (assignee == null && currentUser.UserType == CIDRS.Identity.Domain.Enums.ApplicationUserType.Phi)
                return GetErrorResult<bool>("You are not got assigned to review the work item!!");

            if(currentUser.UserType == CIDRS.Identity.Domain.Enums.ApplicationUserType.Phi && assignee.AssignTo.IdentityUserId != currentUser.Id)
                return GetErrorResult<bool>("You are not got assigned to review the work item!!");

            if (workItem.Application.Type == ApplicationType.HomeSurroundingAllerts)
            {

                var action = GetNewWorkItemAction(WorkItemActionType.Approve);
                workItem.WorkItemActions.Add(action);
                workItem.Status = WorkItemStatus.Approved;
                workItem.SetUpdatedTime();

                workItem.Application.ChiefOccupant.ApplicationRejectedCount = 0;

                _dataContext.WorkItems.Update(workItem);
                await _dataContext.SaveChangesAsync();

                var penaltyResponse = await _penaltyService.ResolveRedNoticessAsync(workItem.Application.ChiefOccupantId);
                await NotifyChiefcOcupantAsync(workItem.Application.ChiefOccupant, ChiefOccupantNotification.ApproveHomeSurroundingAlert);

                return new ResponseResult<bool>()
                {
                    Errors = null,
                    Result = true,
                    Succeeded = true
                };
            }
            if(workItem.Application.Type == ApplicationType.PublicSurroundingComplaints)
            {
                var action = GetNewWorkItemAction(WorkItemActionType.Approve);
                workItem.WorkItemActions.Add(action);
                workItem.Status = WorkItemStatus.Approved;
                workItem.SetUpdatedTime();

                _dataContext.WorkItems.Update(workItem);
                await _dataContext.SaveChangesAsync();

                await NotifyChiefcOcupantAsync(workItem.Application.ChiefOccupant, ChiefOccupantNotification.ApprovePublicSurroundingComplaints);

                return new ResponseResult<bool>()
                {
                    Errors = null,
                    Result = true,
                    Succeeded = true
                };
            }

            return new ResponseResult<bool>()
            {
                Errors = null,
                Result = false,
                Succeeded = false
            };

        }

        public async Task<ResponseResult<bool>> RejectAsync(int id)
        {
            var response = await GetWorkitemAsync(id);

            if (!response.Succeeded)
                return new ResponseResult<bool>()
                {
                    Errors = response.Errors,
                    Succeeded = response.Succeeded,
                    Result = false
                };
            var workItem = response.Result;

            if (workItem.Status != WorkItemStatus.Active)
                return GetErrorResult<bool>("Work Item Already Actioned!!");

            if (workItem.Type != WorkItemType.ReportingApplication)
                return GetErrorResult<bool>("Work Item Type Not Supported!!");

            var currentUser = await _identityService.GetCurrentUserAsync();

            if (!(currentUser.UserType == CIDRS.Identity.Domain.Enums.ApplicationUserType.Admin || currentUser.UserType == CIDRS.Identity.Domain.Enums.ApplicationUserType.Phi))
                return GetErrorResult<bool>("User not authorized to review the work item!!");

            var assignee = workItem.WorkItemActions.OrderByDescending(x => x.CreatedAt).FirstOrDefault(x => x.Type == WorkItemActionType.Assign);

            if (assignee == null && currentUser.UserType == CIDRS.Identity.Domain.Enums.ApplicationUserType.Phi)
                return GetErrorResult<bool>("You are not got assigned to review the work item!!");

            if (currentUser.UserType == CIDRS.Identity.Domain.Enums.ApplicationUserType.Phi && assignee.AssignTo.IdentityUserId != currentUser.Id)
                return GetErrorResult<bool>("You are not got assigned to review the work item!!");

            if (workItem.Application.Type == ApplicationType.HomeSurroundingAllerts)
            {

                var action = GetNewWorkItemAction(WorkItemActionType.Reject);
                workItem.WorkItemActions.Add(action);
                workItem.Status = WorkItemStatus.Rejected;
                workItem.SetUpdatedTime();

                workItem.Application.ChiefOccupant.ApplicationRejectedCount += 1;

                _dataContext.WorkItems.Update(workItem);
                await _dataContext.SaveChangesAsync();

                var penaltyResponse = await _penaltyService.AddPenaltyAsync(workItem.Application.ChiefOccupantId);
                await NotifyChiefcOcupantAsync(workItem.Application.ChiefOccupant, ChiefOccupantNotification.RejectHomeSurroundingAlert);
                await NotifyPoliceAsync(workItem.Application.ChiefOccupant, ChiefOccupantNotification.RejectHomeSurroundingAlert);
                await NotifyPhiAsync(workItem.Application.ChiefOccupant, ChiefOccupantNotification.RejectHomeSurroundingAlert);

                return new ResponseResult<bool>()
                {
                    Errors = null,
                    Result = true,
                    Succeeded = true
                };
            }
            

            return new ResponseResult<bool>()
            {
                Errors = null,
                Result = false,
                Succeeded = false
            };

        }

        #region Private Methods
        private WorkItemAction GetNewWorkItemAction(WorkItemActionType type, int? assignTo = null)
        {
            var workItemAction = new WorkItemAction();
            workItemAction.EnsureId(GetWorkItemActionId());
            workItemAction.SetCreatedTime();
            workItemAction.Type = type;
            if (type == WorkItemActionType.Assign && assignTo != null)
                workItemAction.AssignToId = assignTo;
            return workItemAction;
        }

        private WorkItemRemark GetNewWorkItemRemark(string remark, string ownerName)
        {
            var workItemRemark = new WorkItemRemark();
            workItemRemark.Remark = remark;
            workItemRemark.OwnerName = ownerName;
            workItemRemark.EnsureId(GetWorkItemRemarkId());
            workItemRemark.SetCreatedTime();
            return workItemRemark;
        }

        private WorkItem GetNewWorkItem(WorkItemType type, int relativeId)
        {

            var workItem = new WorkItem();
            workItem.EnsureId(GetWorkItemId());
            workItem.SetCreatedTime();
            workItem.WorkItemActions.Add(GetNewWorkItemAction(WorkItemActionType.New));
            switch (type)
            {
                case WorkItemType.CORegistration:
                    workItem.ChiefOccupantId = relativeId;
                    workItem.Status = WorkItemStatus.Active;
                    workItem.Type = WorkItemType.CORegistration;

                    break;
                case WorkItemType.ReportingApplication:
                    workItem.ReportingApplicationId = relativeId;
                    workItem.Status = WorkItemStatus.Active;
                    workItem.Type = WorkItemType.ReportingApplication;
                    break;
                default:
                    break;
            }

            return workItem;
        }

        private int GetWorkItemId()
        {
            // Get Last StaffId
            var result = _dataContext.WorkItems.OrderBy(a => a.Id).LastOrDefault();

            if (result != null)
                return result.Id + 1;
            else
                return 1;

        }

        private int GetWorkItemActionId()
        {
            // Get Last StaffId
            var result = _dataContext.WorkItemActions.OrderBy(a => a.Id).LastOrDefault();

            if (result != null)
                return result.Id + 1;
            else
                return 1;

        }
        private int GetWorkItemRemarkId()
        {
            // Get Last StaffId
            var result = _dataContext.WorkItemRemarks.OrderBy(a => a.Id).LastOrDefault();

            if (result != null)
                return result.Id + 1;
            else
                return 1;

        }

        private IQueryable<WorkItem> GetBasicSearchResult(WorkItemSearchRequest request)
        {

            IQueryable<WorkItem> workItems = _dataContext.WorkItems.Include(x => x.Application)
                                                            .ThenInclude(x => x.SurroundingSets)
                                                       .Include(x => x.ChiefOccupant)
                                                            .ThenInclude(x => x.RespectivePhi)
                                                       .Include(x => x.WorkItemActions)
                                                            .ThenInclude(x => x.AssignTo);

            if (request.IsActive != null)
            {
                workItems = request.IsActive.Value ? workItems.Where(x => x.Status == WorkItemStatus.Active) : workItems.Where(x => x.Status != WorkItemStatus.Active);
            }

            if (request.Type != null)
            {
                workItems = workItems.Where(x => x.Type == request.Type);
            }

            if (request.StartDate != null && request.EndDate != null)
            {
                workItems = workItems.Where(x => x.WorkItemActions.Any(y => y.Type == WorkItemActionType.New && y.CreatedAt.Value.Date >= request.StartDate.Value.Date && y.CreatedAt.Value.Date <= request.EndDate.Value.Date));
            }

            if (request.AsigneeId != null)
            {
                workItems = workItems.Where(x => x.WorkItemActions.Any(y => y.Type == WorkItemActionType.Assign) && x.WorkItemActions.OrderByDescending(y => y.CreatedAt.Value).FirstOrDefault(z => z.Type == WorkItemActionType.Assign).AssignToId == request.AsigneeId.Value);
            }

            return workItems;
        }

        private async Task NotifyAssigneesAsync(PublicHealthInspector phi, Police police, ChiefOccupant chiefOccupant)
        {
            try
            {
                string fileName = "NotifyCOWIAssignment.txt";

                var templatePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Templates\\SMS", fileName);
                var content = await templatePath.Render(new string[] { chiefOccupant?.FullName, police?.FullName, police?.PoliceStation?.Name, phi?.FullName, phi?.MohArea?.Name });
                await _smsSender.SendSmsAsync(chiefOccupant.PhoneNumber, content);

                fileName = "NotifyPhiWIAssignment.txt";

                templatePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Templates\\SMS", fileName);
                content = await templatePath.Render(new string[] { phi?.FullName, chiefOccupant?.FullName, chiefOccupant?.Address, police?.FullName, police?.PoliceStation?.Name });
                await _smsSender.SendSmsAsync(phi.PhoneNumber, content);

                fileName = "NotifyPoliceWIAssignment.txt";

                templatePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Templates\\SMS", fileName);
                content = await templatePath.Render(new string[] { police?.FullName, chiefOccupant?.FullName, chiefOccupant?.Address, phi?.FullName, phi?.MohArea?.Name });
                await _smsSender.SendSmsAsync(police.Mobile, content);
            }
            catch 
            {

            }
        }

        private async Task NotifyAssigneesAsync(PublicHealthInspector phi, ChiefOccupant chiefOccupant,ApplicationType applicationType)
        {
            try
            {
                var type = string.Empty;
                switch (applicationType)
                {
                    case ApplicationType.Base:
                        type = "Base";
                        break;
                    case ApplicationType.HomeSurroundingAllerts:
                        type = "Home Surrounding Alert";
                        break;
                    case ApplicationType.PublicSurroundingComplaints:
                        type = "Public Surrounding Complaint";
                        break;                    
                }

                string fileName = "NotifyCOHomeRAAssignment.txt";

                var templatePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Templates\\SMS", fileName);
                var content = await templatePath.Render(new string[] { chiefOccupant?.FullName, phi?.FullName, phi?.MohArea?.Name,type });
                await _smsSender.SendSmsAsync(chiefOccupant.PhoneNumber, content);

                fileName = "NotifyPhiHomeRAAssignment.txt";

                templatePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Templates\\SMS", fileName);
                content = await templatePath.Render(new string[] { phi?.FullName, chiefOccupant?.FullName, chiefOccupant?.Address,type });
                await _smsSender.SendSmsAsync(phi.PhoneNumber, content);

                fileName = "NotifyPhiHomeRAAssignment.html";
                templatePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Templates\\Email", fileName);
                content = await templatePath.Render(new string[] { phi?.FullName, chiefOccupant?.FullName, chiefOccupant?.Address,type});
                var user = await _identityService.GetUserAsync(phi.IdentityUserId);
                var html = new NotificationMessage(new string[] { user.Email }, "CIDRS -A "+ type +" Added to your Work Queue", content);
                await _emailSender.SendEmailAsync(html);
            }
            catch
            {

            }
        }

        private async Task NotifyChiefcOcupantAsync(ChiefOccupant chiefOccupant, ChiefOccupantNotification type)
        {
            try
            {
                string fileName = string.Empty;
                string templatePath = string.Empty;
                string content = string.Empty;

                switch (type)
                {
                    case ChiefOccupantNotification.ApproveCoRegistration:
                        fileName = "AnnounceCOActivation.txt";
                        templatePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Templates\\SMS", fileName);
                        content = await templatePath.Render(new string[] { chiefOccupant?.FullName });
                        break;
                    case ChiefOccupantNotification.ApproveHomeSurroundingAlert:
                        fileName = "HSApprovalNotification.txt";
                        templatePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Templates\\SMS", fileName);
                        content = await templatePath.Render(new string[] { chiefOccupant?.FullName });
                        break;
                    case ChiefOccupantNotification.ApprovePublicSurroundingComplaints:
                        fileName = "PSApprovalNotification.txt";
                        templatePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Templates\\SMS", fileName);
                        content = await templatePath.Render(new string[] { chiefOccupant?.FullName });
                        break;
                    case ChiefOccupantNotification.RejectHomeSurroundingAlert:
                        fileName = "NotifyCORejectHSAlert.txt";
                        templatePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Templates\\SMS", fileName);
                        content = await templatePath.Render(new string[] { chiefOccupant?.FullName });
                        break;

                }                
                await _smsSender.SendSmsAsync(chiefOccupant.PhoneNumber, content);
            }
            catch 
            {

            }
        }

        private async Task NotifyPoliceAsync(ChiefOccupant chiefOccupant, ChiefOccupantNotification type)
        {
            try
            {
                string fileName = string.Empty;
                string templatePath = string.Empty;
                string content = string.Empty;
                switch (type)
                {
                    case ChiefOccupantNotification.RejectHomeSurroundingAlert:
                        fileName = "NotifyPoliceRejectHSAlert.txt";
                        templatePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Templates\\SMS", fileName);
                        content = await templatePath.Render(new string[] { chiefOccupant?.RespectivePolice?.FullName, chiefOccupant?.FullName, chiefOccupant?.Address, chiefOccupant.RespectivePhi.FullName, chiefOccupant.RespectivePhi.MohArea.Name});
                        break;
                    default:
                        break;
                }
                await _smsSender.SendSmsAsync(chiefOccupant.RespectivePolice.Mobile, content);
            }
            catch 
            {

            }

        }
        private async Task NotifyPhiAsync(ChiefOccupant chiefOccupant, ChiefOccupantNotification type)
        {
            try
            {
                string fileName = string.Empty;
                string templatePath = string.Empty;
                string content = string.Empty;
                switch (type)
                {
                    case ChiefOccupantNotification.RejectHomeSurroundingAlert:
                        fileName = "NotifyPhiRejectHSAlert.txt";
                        templatePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Templates\\SMS", fileName);
                        content = await templatePath.Render(new string[] { chiefOccupant?.RespectivePhi?.FullName, chiefOccupant?.FullName, chiefOccupant?.Address, chiefOccupant?.RespectivePolice?.FullName, chiefOccupant?.RespectivePolice?.PoliceStation?.Name});
                        break;
                    default:
                        break;
                }
                await _smsSender.SendSmsAsync(chiefOccupant.RespectivePhi.PhoneNumber, content);
            }
            catch 
            {

            }

        }

        private ResponseResult<T> GetErrorResult<T>(params string[] errors)
        {
            return new ResponseResult<T>()
            {
                Errors = errors,
                Result = default(T),
                Succeeded = false
            };
        }
        #endregion




    }

    enum ChiefOccupantNotification
    {
        ApproveCoRegistration = 1,
        ApproveHomeSurroundingAlert = 2,
        ApprovePublicSurroundingComplaints = 3,
        RejectHomeSurroundingAlert = 4
    }
}
