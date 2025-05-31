using CIDRS.Core.Common.Models;
using CIDRS.Core.Modules.ChiefOccupants.Extensions;
using CIDRS.Core.Modules.ChiefOccupants.Models.Request;
using CIDRS.Core.Modules.PublicHealthInspectors.Services;
using CIDRS.Core.Modules.WorkItems.Services;
using CIDRS.Domain.Models.Entity.ChiefOccupants;
using CIDRS.Identity.Domain.Models.Entity;
using CIDRS.Identity.Services.User;
using CIDRS.Infrastructure;
using CIDRS.Shared.Common.Api.Models;
using CIDRS.Shared.Common.Extensions;
using CIDRS.Shared.Common.Pagination.Extensions;
using CIDRS.Shared.Common.Pagination.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.ChiefOccupants.Services
{
    public class ChiefOccupantService : IChiefOccupantService
    {
        private readonly IIdentityService _identityService;
        private readonly ApplicationDataContext _dataContext;
        private readonly IWorkItemService _workItemServise;
        private readonly IPublicHealthInspectorService _publicHealthInspectorService;

        public ChiefOccupantService(IIdentityService identityService, ApplicationDataContext dataContext, IWorkItemService workItemServise, IPublicHealthInspectorService publicHealthInspectorService)
        {
            _identityService = identityService;
            _dataContext = dataContext;
            _workItemServise = workItemServise;
            _publicHealthInspectorService = publicHealthInspectorService;
        }

        public async Task<ResponseResult<ChiefOccupant>> RegisterChiefOccupantAsync(RegisterChiefOccupantRequest request)
        {
            request.UserType = CIDRS.Identity.Domain.Enums.ApplicationUserType.ChiefOccupant;
            var userRequest = request.ToUserRegistrationRequest();
      
                var userRegistrationResponse = await _identityService.RegisterUserAsync(userRequest);

            if (!userRegistrationResponse.Status)
            {
                return new ResponseResult<ChiefOccupant>()
                {
                    Errors = userRegistrationResponse.Errors,
                    Succeeded = false,
                    Result = null
                };
            }
            else
            {
                var coRequest = request.ToEntityModel(userRegistrationResponse.User.Id);
                coRequest.SetCreatedTime();
                coRequest.EnsureId(GetCoId());
                using (var transaction = _dataContext.Database.BeginTransaction())
                {
                    try
                    {
                        await _dataContext.ChiefOccupants.AddAsync(coRequest);
                        await _dataContext.SaveChangesAsync();
                        await _workItemServise.CreateWorkItemAsync(Domain.Enums.WorkItemType.CORegistration, coRequest.Id);

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        return new ResponseResult<ChiefOccupant>()
                        {
                            Errors = new[] { ex.Message },
                            Result = null,
                            Succeeded = false
                        };
                    }
                }               

                return new ResponseResult<ChiefOccupant>()
                {
                    Errors = null,
                    Result = coRequest,
                    Succeeded = true
                };

            }

        }

        public async Task<PaginationVM<ChiefOccupant>> IndexChiefOccupantsAsync(ChiefOccupantSearchRequest searchRequest)
        {
            var currentUser = await _identityService.GetCurrentUserAsync();
            if(currentUser.UserType == CIDRS.Identity.Domain.Enums.ApplicationUserType.Phi)
            {
                var phi = await _publicHealthInspectorService.ViewPublicHealthInspectorsAsync(currentUser.Id);
                searchRequest.PhiId = phi.Id;
            }
            searchRequest.BasicSearchValue = searchRequest.BasicSearchValue?.Trim();
            IQueryable<ChiefOccupant> chiefOccupants = GetBasicSearchResult(searchRequest,currentUser);

            return await chiefOccupants.PaginateAsync(searchRequest?.PaginationOption?.Page, searchRequest?.PaginationOption?.PageSize);

        }

        public async Task<ResponseResult<ChiefOccupant>> GetChiefOccupantById(int id)
        {
            var chiefOccupant = await _dataContext.ChiefOccupants
                                                    .Include(x => x.MohArea)
                                                    .Include(x => x.District)
                                                    .Include(x => x.Penalties)
                                                    .Include(x => x.RespectivePhi)
                                                        .ThenInclude(x => x.District)
                                                    .Include(x => x.RespectivePhi)
                                                        .ThenInclude(x => x.MohArea)
                                                    .Include(x => x.RespectivePolice)
                                                        .ThenInclude(x => x.PoliceStation)
                                                    .Include(x => x.ReportingApplications)
                                                        .ThenInclude(x => x.SurroundingSets)
                                                            .ThenInclude(x => x.RelativeSurroundingSet)
                                                    .Include(x => x.ReportingApplications)
                                                        .ThenInclude(x => x.WorkItem)
                                                            .ThenInclude(x => x.WorkItemActions)
                                                    .Include(x => x.ReportingApplications)
                                                        .ThenInclude(x => x.WorkItem)
                                                            .ThenInclude(x => x.WorkItemRemarks)
                                                    .Include(x => x.WorkItem)
                                                        .ThenInclude(x => x.WorkItemActions)
                                                    .Include(x => x.WorkItem)
                                                        .ThenInclude(x => x.WorkItemRemarks)
                                                        .SingleOrDefaultAsync(x => x.Id == id);

            if (chiefOccupant == null)
                return new ResponseResult<ChiefOccupant>()
                {
                    Errors = new[] { "No Chief Occupant Found!" },
                    Result = null,
                    Succeeded = false
                };

            return new ResponseResult<ChiefOccupant>()
            {
                Errors = null,
                Result = chiefOccupant,
                Succeeded = true
            };

        }

        public async Task<ChiefOccupant> GetChiefOccupantByIdentityId(string identityId)
        {
            var chiefOccupant = await _dataContext.ChiefOccupants
                                                    .Include(x => x.MohArea)
                                                    .Include(x => x.District)
                                                    .Include(x => x.Penalties)
                                                    .Include(x => x.RespectivePhi)
                                                        .ThenInclude(x => x.District)
                                                    .Include(x => x.RespectivePhi)
                                                        .ThenInclude(x => x.MohArea)
                                                    .Include(x => x.RespectivePolice)
                                                        .ThenInclude(x => x.PoliceStation)
                                                    .Include(x => x.ReportingApplications)
                                                        .ThenInclude(x => x.SurroundingSets)
                                                            .ThenInclude(x => x.RelativeSurroundingSet)
                                                    .Include(x => x.ReportingApplications)
                                                        .ThenInclude(x => x.WorkItem)
                                                            .ThenInclude(x => x.WorkItemActions)
                                                    .Include(x => x.ReportingApplications)
                                                        .ThenInclude(x => x.WorkItem)
                                                            .ThenInclude(x => x.WorkItemRemarks)
                                                    .Include(x => x.WorkItem)
                                                        .ThenInclude(x => x.WorkItemActions)
                                                    .Include(x => x.WorkItem)
                                                        .ThenInclude(x => x.WorkItemRemarks)
                                                        .SingleOrDefaultAsync(x => x.IdentityUserId == identityId);

            return chiefOccupant;

        }

        /// <summary>
        /// Get AdminId To create
        /// </summary>
        /// <returns></returns>
        private int GetCoId()
        {
            // Get Last StaffId
            var result = _dataContext.ChiefOccupants.OrderBy(a => a.Id).LastOrDefault();

            if (result != null)
                return result.Id + 1;
            else
                return 1;

        }

        private IQueryable<ChiefOccupant> GetBasicSearchResult(ChiefOccupantSearchRequest request, ApplicationUser currentUser)
        {
            if(currentUser.UserType == CIDRS.Identity.Domain.Enums.ApplicationUserType.ChiefOccupant)
            {
                return _dataContext.ChiefOccupants.Where(x => x.Id == 0);
            }

            IQueryable<ChiefOccupant> result = _dataContext.ChiefOccupants
                                                    .Include(x => x.MohArea)
                                                    .Include(x => x.District)
                                                    .Include(x => x.Penalties)
                                                    .Include(x => x.RespectivePhi)
                                                        .ThenInclude(x => x.District)
                                                    .Include(x => x.RespectivePhi)
                                                        .ThenInclude(x => x.MohArea)
                                                    .Include(x => x.ReportingApplications)
                                                        .ThenInclude(x => x.SurroundingSets);


            if (!string.IsNullOrWhiteSpace(request.BasicSearchValue))
            {
                request.BasicSearchValue = request.BasicSearchValue.ToLower().Trim();

                result = result.Where(x => x.FullName.ToLower().Contains(request.BasicSearchValue) ||
                                       x.PhoneNumber.Contains(request.BasicSearchValue) ||
                                       x.Identifier.ToLower().Contains(request.BasicSearchValue));
            }

            if (request.DistrictId != null)
            {
                result = result.Where(x => x.DistrictId == request.DistrictId);
            }

            if (request.MohAreaId != null)
            {
                result = result.Where(x => x.MohAreaId == request.MohAreaId);
            }
            if(request.PhiId != null)
            {
                result = result.Where(x => x.PhiId == request.PhiId);
            }
            result = result.OrderBy(e => e.FullName);

            return result;
        }
    }
}
