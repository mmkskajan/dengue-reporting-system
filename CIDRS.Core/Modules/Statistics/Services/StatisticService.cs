using AutoMapper;
using CIDRS.Core.Modules.Applications.ViewModels;
using CIDRS.Core.Modules.Statistics.Models.Request;
using CIDRS.Core.Modules.Statistics.Models.Response;
using CIDRS.Domain.Models.Entity.ChiefOccupants;
using CIDRS.Domain.Models.Entity.WorkItems;
using CIDRS.Identity.Domain.Models.Entity;
using CIDRS.Identity.Services.User;
using CIDRS.Infrastructure;
using CIDRS.Shared.Common.Pagination.Extensions;
using CIDRS.Shared.Common.Pagination.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.Statistics.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly ApplicationDataContext _dataContext;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public StatisticService(ApplicationDataContext dataContext, IIdentityService identityService, IMapper mapper)
        {
            _dataContext = dataContext;
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task<StatisticsDetails> GetStatisticsAsync()
        {
            var currentUser = await _identityService.GetCurrentUserAsync();

            switch (currentUser.UserType)
            {
                case CIDRS.Identity.Domain.Enums.ApplicationUserType.Admin:
                    return new StatisticsDetails()
                    {
                        Global = await GetGlobalAsync(),
                        Related = null,
                        ChiefOccupantStatistics = null
                    };
                case CIDRS.Identity.Domain.Enums.ApplicationUserType.Phi:
                    return new StatisticsDetails()
                    {
                        Global = await GetGlobalAsync(),
                        Related = await GetRelatedAsync(currentUser),
                        ChiefOccupantStatistics = null

                    };
                case CIDRS.Identity.Domain.Enums.ApplicationUserType.ChiefOccupant:
                    return new StatisticsDetails()
                    {
                        Global = await GetGlobalAsync(),
                        Related = await GetRelatedAsync(currentUser),
                        ChiefOccupantStatistics = await GetChiefOccupantStatistics()
                    };
                default:
                    return new StatisticsDetails()
                    {
                        Global = null,
                        Related = null,
                        ChiefOccupantStatistics = null
                    };
            }
        }

        public async Task<PaginationVM<WorkItem>> GetApplicationStatisticsAsync(ApplicationStatisticsSearchRequest searchRequest)
        {
            var currentUser = await _identityService.GetCurrentUserAsync();
            if (currentUser.UserType == CIDRS.Identity.Domain.Enums.ApplicationUserType.Admin)
                searchRequest.IsRelative = false;

            IQueryable<WorkItem> chiefOccupants = GetApplicationStatisticsSearchResult(searchRequest, currentUser);

            return await chiefOccupants.PaginateAsync(searchRequest?.PaginationOption?.Page, searchRequest?.PaginationOption?.PageSize);

        }

        private IQueryable<WorkItem> GetApplicationStatisticsSearchResult(ApplicationStatisticsSearchRequest request, ApplicationUser currentUser)
        {
            if (currentUser.UserType == CIDRS.Identity.Domain.Enums.ApplicationUserType.ChiefOccupant)
            {
                return _dataContext.WorkItems.Where(x => x.Id == 0);
            }

            IQueryable<WorkItem> result = _dataContext.WorkItems.Include(x => x.Application)
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
                                                        .Include(x => x.WorkItemRemarks);


            switch (request.Type)
            {
                case ApplicationStatisticsType.HomeSurroundingApplication:
                    result = result.Where(x => x.Type == Domain.Enums.WorkItemType.ReportingApplication && x.Application.Type == Domain.Enums.ApplicationType.HomeSurroundingAllerts);
                    break;
                case ApplicationStatisticsType.PublicSurroundingApplication:
                    result = result.Where(x => x.Type == Domain.Enums.WorkItemType.ReportingApplication && x.Application.Type == Domain.Enums.ApplicationType.PublicSurroundingComplaints);
                    break;
                case ApplicationStatisticsType.ChiefOccupantRegistration:
                    result = result.Where(x => x.Type == Domain.Enums.WorkItemType.CORegistration);
                    break;

            }

            switch (request.TimeFrequency)
            {
                case TimeFrequency.Today:
                    result = result.Where(x => x.CreatedAt.Value.Date == DateTime.Today);
                    break;
                case TimeFrequency.Week:
                    result = result.Where(x => x.CreatedAt.Value.Date >= DateTime.Today.AddDays(-7));
                    break;
                case TimeFrequency.Month:
                    result = result.Where(x => x.CreatedAt.Value.Date == DateTime.Today.AddDays(-30));
                    break;
            }


            if (request.IsRelative)
            {
                switch (request.Type)
                {
                    case ApplicationStatisticsType.HomeSurroundingApplication:
                    case ApplicationStatisticsType.PublicSurroundingApplication:
                        result = result.Where(x => x.Application.ChiefOccupant.RespectivePhi.IdentityUserId == currentUser.Id);
                        break;
                    case ApplicationStatisticsType.ChiefOccupantRegistration:
                        result = result.Where(x => x.ChiefOccupant.RespectivePhi.IdentityUserId == currentUser.Id);
                        break;
                }

            }


            result = result.OrderByDescending(e => e.CreatedAt);

            return result;
        }

        public async Task<PaginationVM<ChiefOccupant>> GetEnvironmentStatisticsAsync(EnvironmentStatisticsSearchRequest searchRequest)
        {
            var currentUser = await _identityService.GetCurrentUserAsync();
            if (currentUser.UserType == CIDRS.Identity.Domain.Enums.ApplicationUserType.Admin)
                searchRequest.IsRelative = false;

            IQueryable<ChiefOccupant> chiefOccupants = GetEnvironmentStatisticsSearchResult(searchRequest, currentUser);

            return await chiefOccupants.PaginateAsync(searchRequest?.PaginationOption?.Page, searchRequest?.PaginationOption?.PageSize);

        }

        private IQueryable<ChiefOccupant> GetEnvironmentStatisticsSearchResult(EnvironmentStatisticsSearchRequest request, ApplicationUser currentUser)
        {
            if (currentUser.UserType == CIDRS.Identity.Domain.Enums.ApplicationUserType.ChiefOccupant)
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
                                                        .ThenInclude(x => x.WorkItemRemarks);


            switch (request.Status)
            {
                case EnvironmentStatus.Clean:
                    result = result.Where(x => x.ApplicationRejectedCount == 0);
                    break;
                case EnvironmentStatus.Danger:
                    result = result.Where(x => x.ApplicationRejectedCount == 0);
                    break;
            }

            if (request.IsRelative)
                result = result.Where(x => x.RespectivePhi.IdentityUserId == currentUser.Id);

            result = result.OrderByDescending(e => e.CreatedAt);

            return result;
        }

        public async Task<PaginationVM<ChiefOccupant>> GetPenalizationStatisticsAsync(PenalizationStatisticsSearchRequest searchRequest)
        {
            var currentUser = await _identityService.GetCurrentUserAsync();
            if (currentUser.UserType == CIDRS.Identity.Domain.Enums.ApplicationUserType.Admin)
                searchRequest.IsRelative = false;

            IQueryable<ChiefOccupant> chiefOccupants = GetPenalizationStatisticsSearchResult(searchRequest, currentUser);

            return await chiefOccupants.PaginateAsync(searchRequest?.PaginationOption?.Page, searchRequest?.PaginationOption?.PageSize);

        }

        private IQueryable<ChiefOccupant> GetPenalizationStatisticsSearchResult(PenalizationStatisticsSearchRequest request, ApplicationUser currentUser)
        {
            if (currentUser.UserType == CIDRS.Identity.Domain.Enums.ApplicationUserType.ChiefOccupant)
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
                                                        .ThenInclude(x => x.WorkItemRemarks);


            switch (request.Status)
            {
                case PenalizationStatus.Penalized:
                    result = result.Where(x => x.Penalties.Any(y => y.PenaltyStatus == Domain.Enums.PenaltyStatus.Active));
                    break;
                case PenalizationStatus.Resolved:
                    result = result.Where(x => !x.Penalties.Any(y => y.PenaltyStatus == Domain.Enums.PenaltyStatus.Active));
                    break;
            }

            if (request.IsRelative)
                result = result.Where(x => x.RespectivePhi.IdentityUserId == currentUser.Id);

            result = result.OrderByDescending(e => e.CreatedAt);

            return result;
        }


        private async Task<Global> GetGlobalAsync()
        {
            var overview = new OverviewStatistics()
            {
                HomeSurroundingAlerts = await _dataContext.WorkItems.Include(x => x.Application).Where(x => x.Type == Domain.Enums.WorkItemType.ReportingApplication && x.Application.Type == Domain.Enums.ApplicationType.HomeSurroundingAllerts).CountAsync(),
                PublicSurroundingComplaints = await _dataContext.WorkItems.Include(x => x.Application).Where(x => x.Type == Domain.Enums.WorkItemType.ReportingApplication && x.Application.Type == Domain.Enums.ApplicationType.PublicSurroundingComplaints).CountAsync(),
                RegisteredChiefOccupants = await _dataContext.WorkItems.Where(x => x.Type == Domain.Enums.WorkItemType.CORegistration).CountAsync()
            };

            var homeSurroundings = new HomeSurroundingSatatistics()
            {
                Month = await _dataContext.WorkItems.Include(x => x.Application).Where(x => x.Type == Domain.Enums.WorkItemType.ReportingApplication && x.Application.Type == Domain.Enums.ApplicationType.HomeSurroundingAllerts && x.CreatedAt.Value.Date >= DateTime.Today.AddDays(-30)).CountAsync(),
                Week = await _dataContext.WorkItems.Include(x => x.Application).Where(x => x.Type == Domain.Enums.WorkItemType.ReportingApplication && x.Application.Type == Domain.Enums.ApplicationType.HomeSurroundingAllerts && x.CreatedAt.Value.Date >= DateTime.Today.AddDays(-7)).CountAsync(),
                Today = await _dataContext.WorkItems.Include(x => x.Application).Where(x => x.Type == Domain.Enums.WorkItemType.ReportingApplication && x.Application.Type == Domain.Enums.ApplicationType.HomeSurroundingAllerts && x.CreatedAt.Value.Date == DateTime.Today).CountAsync(),
            };

            var publicSurroundings = new PublicSurroundingStatistics()
            {
                Month = await _dataContext.WorkItems.Include(x => x.Application).Where(x => x.Type == Domain.Enums.WorkItemType.ReportingApplication && x.Application.Type == Domain.Enums.ApplicationType.PublicSurroundingComplaints && x.CreatedAt.Value.Date >= DateTime.Today.AddDays(-30)).CountAsync(),
                Week = await _dataContext.WorkItems.Include(x => x.Application).Where(x => x.Type == Domain.Enums.WorkItemType.ReportingApplication && x.Application.Type == Domain.Enums.ApplicationType.PublicSurroundingComplaints && x.CreatedAt.Value.Date >= DateTime.Today.AddDays(-7)).CountAsync(),
                Today = await _dataContext.WorkItems.Include(x => x.Application).Where(x => x.Type == Domain.Enums.WorkItemType.ReportingApplication && x.Application.Type == Domain.Enums.ApplicationType.PublicSurroundingComplaints && x.CreatedAt.Value.Date == DateTime.Today).CountAsync(),
            };

            var chiefOccupants = new ChiefOccupantsStatistics()
            {
                Month = await _dataContext.WorkItems.Where(x => x.Type == Domain.Enums.WorkItemType.CORegistration && x.CreatedAt.Value.Date >= DateTime.Today.AddDays(-30)).CountAsync(),
                Week = await _dataContext.WorkItems.Where(x => x.Type == Domain.Enums.WorkItemType.CORegistration && x.CreatedAt.Value.Date >= DateTime.Today.AddDays(-7)).CountAsync(),
                Today = await _dataContext.WorkItems.Where(x => x.Type == Domain.Enums.WorkItemType.CORegistration && x.CreatedAt.Value.Date == DateTime.Today).CountAsync(),
            };

            var totalCo = await _dataContext.ChiefOccupants.CountAsync();
            var cleanCo = await _dataContext.ChiefOccupants.Where(x => x.ApplicationRejectedCount == 0).CountAsync();
            var dangerCo = totalCo - cleanCo;

            var environment = new EnvironmentStatistics()
            {
                Clean = new Clean()
                {
                    Value = new Value()
                    {
                        Count = cleanCo,
                        Percentage = string.Format("{0}%", GetPercentage(cleanCo,totalCo)),
                        TotalCount = totalCo
                    }
                },
                Danger = new Danger()
                {
                    Value = new Value()
                    {
                        Count = dangerCo,
                        Percentage = string.Format("{0}%", GetPercentage(dangerCo,totalCo)),
                        TotalCount = totalCo
                    }
                }
            };

            var penalized = await _dataContext.ChiefOccupants.Include(x => x.Penalties).Where(x => x.Penalties.Any(y => y.PenaltyStatus == Domain.Enums.PenaltyStatus.Active)).CountAsync();
            var resolved = totalCo - penalized;

            var penalization = new PenalizationStatistics()
            {
                Pending = new Pending()
                {
                    Value = new Value()
                    {
                        Count = penalized,
                        Percentage = string.Format("{0}%", GetPercentage(penalized , totalCo)),
                        TotalCount = totalCo
                    }
                },
                Resolved = new Resolved()
                {
                    Value = new Value()
                    {
                        Count = resolved,
                        Percentage = string.Format("{0}%", GetPercentage(resolved , totalCo)),
                        TotalCount = totalCo
                    }
                }
            };

            return new Global()
            {
                ChiefOccupants = chiefOccupants,
                Environment = environment,
                HomeSurroundings = homeSurroundings,
                Overview = overview,
                Penalization = penalization,
                PublicSurroundings = publicSurroundings
            };
        }

        private  double GetPercentage(int part, int total)
        {
            if (total == 0)
                return 0.00;

            return Math.Round(((double)part / (double)total * 100), 1);
        }

        private async Task<Related> GetRelatedAsync(ApplicationUser user)
        {
            var overview = new OverviewStatistics()
            {
                HomeSurroundingAlerts = await _dataContext.WorkItems.Include(x => x.Application).ThenInclude(x => x.ChiefOccupant).ThenInclude(x => x.RespectivePhi).Where(x => x.Type == Domain.Enums.WorkItemType.ReportingApplication && x.Application.Type == Domain.Enums.ApplicationType.HomeSurroundingAllerts && x.Application.ChiefOccupant.RespectivePhi.IdentityUserId == user.Id).CountAsync(),
                PublicSurroundingComplaints = await _dataContext.WorkItems.Include(x => x.Application).ThenInclude(x => x.ChiefOccupant).ThenInclude(x => x.RespectivePhi).Where(x => x.Type == Domain.Enums.WorkItemType.ReportingApplication && x.Application.Type == Domain.Enums.ApplicationType.PublicSurroundingComplaints && x.Application.ChiefOccupant.RespectivePhi.IdentityUserId == user.Id).CountAsync(),
                RegisteredChiefOccupants = await _dataContext.WorkItems.Include(x => x.ChiefOccupant).ThenInclude(x => x.RespectivePhi).Where(x => x.Type == Domain.Enums.WorkItemType.CORegistration && x.ChiefOccupant.RespectivePhi.IdentityUserId == user.Id).CountAsync()
            };

            var homeSurroundings = new HomeSurroundingSatatistics()
            {
                Month = await _dataContext.WorkItems.Include(x => x.Application).ThenInclude(x => x.ChiefOccupant).ThenInclude(x => x.RespectivePhi).Where(x => x.Type == Domain.Enums.WorkItemType.ReportingApplication && x.Application.Type == Domain.Enums.ApplicationType.HomeSurroundingAllerts && x.Application.ChiefOccupant.RespectivePhi.IdentityUserId == user.Id && x.CreatedAt.Value.Date >= DateTime.Today.AddDays(-30)).CountAsync(),
                Week = await _dataContext.WorkItems.Include(x => x.Application).ThenInclude(x => x.ChiefOccupant).ThenInclude(x => x.RespectivePhi).Where(x => x.Type == Domain.Enums.WorkItemType.ReportingApplication && x.Application.Type == Domain.Enums.ApplicationType.HomeSurroundingAllerts && x.Application.ChiefOccupant.RespectivePhi.IdentityUserId == user.Id && x.CreatedAt.Value.Date >= DateTime.Today.AddDays(-7)).CountAsync(),
                Today = await _dataContext.WorkItems.Include(x => x.Application).ThenInclude(x => x.ChiefOccupant).ThenInclude(x => x.RespectivePhi).Where(x => x.Type == Domain.Enums.WorkItemType.ReportingApplication && x.Application.Type == Domain.Enums.ApplicationType.HomeSurroundingAllerts && x.Application.ChiefOccupant.RespectivePhi.IdentityUserId == user.Id && x.CreatedAt.Value.Date >= DateTime.Today).CountAsync(),
            };

            var publicSurroundings = new PublicSurroundingStatistics()
            {
                Month = await _dataContext.WorkItems.Include(x => x.Application).ThenInclude(x => x.ChiefOccupant).ThenInclude(x => x.RespectivePhi).Where(x => x.Type == Domain.Enums.WorkItemType.ReportingApplication && x.Application.Type == Domain.Enums.ApplicationType.PublicSurroundingComplaints && x.Application.ChiefOccupant.RespectivePhi.IdentityUserId == user.Id && x.CreatedAt.Value.Date >= DateTime.Today.AddDays(-30)).CountAsync(),
                Week = await _dataContext.WorkItems.Include(x => x.Application).ThenInclude(x => x.ChiefOccupant).ThenInclude(x => x.RespectivePhi).Where(x => x.Type == Domain.Enums.WorkItemType.ReportingApplication && x.Application.Type == Domain.Enums.ApplicationType.PublicSurroundingComplaints && x.Application.ChiefOccupant.RespectivePhi.IdentityUserId == user.Id && x.CreatedAt.Value.Date >= DateTime.Today.AddDays(-7)).CountAsync(),
                Today = await _dataContext.WorkItems.Include(x => x.Application).ThenInclude(x => x.ChiefOccupant).ThenInclude(x => x.RespectivePhi).Where(x => x.Type == Domain.Enums.WorkItemType.ReportingApplication && x.Application.Type == Domain.Enums.ApplicationType.PublicSurroundingComplaints && x.Application.ChiefOccupant.RespectivePhi.IdentityUserId == user.Id && x.CreatedAt.Value.Date >= DateTime.Today).CountAsync(),
            };

            var chiefOccupants = new ChiefOccupantsStatistics()
            {
                Month = await _dataContext.WorkItems.Include(x => x.ChiefOccupant).ThenInclude(x => x.RespectivePhi).Where(x => x.Type == Domain.Enums.WorkItemType.CORegistration && x.ChiefOccupant.RespectivePhi.IdentityUserId == user.Id && x.CreatedAt.Value.Date >= DateTime.Today.AddDays(-30)).CountAsync(),
                Week = await _dataContext.WorkItems.Include(x => x.ChiefOccupant).ThenInclude(x => x.RespectivePhi).Where(x => x.Type == Domain.Enums.WorkItemType.CORegistration && x.ChiefOccupant.RespectivePhi.IdentityUserId == user.Id && x.CreatedAt.Value.Date >= DateTime.Today.AddDays(-7)).CountAsync(),
                Today = await _dataContext.WorkItems.Include(x => x.ChiefOccupant).ThenInclude(x => x.RespectivePhi).Where(x => x.Type == Domain.Enums.WorkItemType.CORegistration && x.ChiefOccupant.RespectivePhi.IdentityUserId == user.Id && x.CreatedAt.Value.Date >= DateTime.Today).CountAsync(),
            };

            var totalCo = await _dataContext.ChiefOccupants.Include(x => x.RespectivePhi).Where(x => x.RespectivePhi.IdentityUserId == user.Id).CountAsync();
            var cleanCo = await _dataContext.ChiefOccupants.Include(x => x.RespectivePhi).Where(x => x.RespectivePhi.IdentityUserId == user.Id && x.ApplicationRejectedCount == 0).CountAsync();
            var dangerCo = totalCo - cleanCo;

            var environment = new EnvironmentStatistics()
            {
                Clean = new Clean()
                {
                    Value = new Value()
                    {
                        Count = cleanCo,
                        Percentage = string.Format("{0}%", GetPercentage(cleanCo , totalCo)),
                        TotalCount = totalCo
                    }
                },
                Danger = new Danger()
                {
                    Value = new Value()
                    {
                        Count = dangerCo,
                        Percentage = string.Format("{0}%", GetPercentage(dangerCo , totalCo)),
                        TotalCount = totalCo
                    }
                }
            };

            var penalized = await _dataContext.ChiefOccupants.Include(x => x.RespectivePhi).Include(x => x.Penalties).Where(x => x.Penalties.Any(y => y.PenaltyStatus == Domain.Enums.PenaltyStatus.Active) && x.RespectivePhi.IdentityUserId == user.Id).CountAsync();
            var resolved = totalCo - penalized;

            var penalization = new PenalizationStatistics()
            {
                Pending = new Pending()
                {
                    Value = new Value()
                    {
                        Count = penalized,
                        Percentage = string.Format("{0}%", GetPercentage(penalized , totalCo)),
                        TotalCount = totalCo
                    }
                },
                Resolved = new Resolved()
                {
                    Value = new Value()
                    {
                        Count = resolved,
                        Percentage = string.Format("{0}%", GetPercentage(resolved , totalCo)),
                        TotalCount = totalCo
                    }
                }
            };

            return new Related()
            {
                ChiefOccupants = chiefOccupants,
                Environment = environment,
                HomeSurroundings = homeSurroundings,
                Overview = overview,
                Penalization = penalization,
                PublicSurroundings = publicSurroundings
            };
        }

        private async Task<ChiefOccupantDetailStatistics> GetChiefOccupantStatistics()
        {
            var currentUser = await _identityService.GetCurrentUserAsync();

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
                                                        .SingleOrDefaultAsync(x => x.IdentityUserId == currentUser.Id);

            return new ChiefOccupantDetailStatistics()
            {
                IsInDanger = IsDanger(chiefOccupant),
                IsPanelized = IsPenalized(chiefOccupant),
                HomeSurroundingAlerts = GetHomeSurroundingAlerts(chiefOccupant),
                PublicSurroundingComplaints = GetPublicSurroundingComplaints(chiefOccupant),
                Rating = GetRating(chiefOccupant)
            };

        }

        private int GetRating(ChiefOccupant chiefOccupant)
        {
            var approved = chiefOccupant.ReportingApplications.Where(x => x.Type == Domain.Enums.ApplicationType.HomeSurroundingAllerts && x.WorkItem.Status == Domain.Enums.WorkItemStatus.Approved).Count();
            var rejected = chiefOccupant.ReportingApplications.Where(x => x.Type == Domain.Enums.ApplicationType.HomeSurroundingAllerts && x.WorkItem.Status == Domain.Enums.WorkItemStatus.Rejected).Count();

            var total = approved + rejected;

            return total == 0 ? 10 : ((approved / total) * 10);
        }

        private PublicSurroundingComplaints GetPublicSurroundingComplaints(ChiefOccupant chiefOccupant)
        {
            var application = _mapper.Map<ReportingApplicationVM>(chiefOccupant.ReportingApplications.FirstOrDefault(x => x.Type == Domain.Enums.ApplicationType.PublicSurroundingComplaints && x.Status == Domain.Enums.ApplicationStatus.Pending));

            return new PublicSurroundingComplaints()
            {
                PendingApplication = application,
                PendingReview = chiefOccupant.ReportingApplications.Where(x => x.Type == Domain.Enums.ApplicationType.PublicSurroundingComplaints && x.WorkItem.Status == Domain.Enums.WorkItemStatus.Active).Count(),
                Total = chiefOccupant.ReportingApplications.Where(x => x.Type == Domain.Enums.ApplicationType.PublicSurroundingComplaints).Count()
            };
        }

        private HomeSurroundingAllerts GetHomeSurroundingAlerts(ChiefOccupant chiefOccupant)
        {

            var application = _mapper.Map<ReportingApplicationVM>(chiefOccupant.ReportingApplications.FirstOrDefault(x => x.Type == Domain.Enums.ApplicationType.HomeSurroundingAllerts && x.Status == Domain.Enums.ApplicationStatus.Pending));

            return new HomeSurroundingAllerts()
            {
                Approved = chiefOccupant.ReportingApplications.Where(x => x.Type == Domain.Enums.ApplicationType.HomeSurroundingAllerts && x.WorkItem.Status == Domain.Enums.WorkItemStatus.Approved).Count(),
                Rejected = chiefOccupant.ReportingApplications.Where(x => x.Type == Domain.Enums.ApplicationType.HomeSurroundingAllerts && x.WorkItem.Status == Domain.Enums.WorkItemStatus.Rejected).Count(),
                PendingApplication = application,
                PendingReview = chiefOccupant.ReportingApplications.Where(x => x.Type == Domain.Enums.ApplicationType.HomeSurroundingAllerts && x.WorkItem.Status == Domain.Enums.WorkItemStatus.Active).Count(),
                Total = chiefOccupant.ReportingApplications.Where(x => x.Type == Domain.Enums.ApplicationType.HomeSurroundingAllerts).Count()
            };
        }

        private bool IsPenalized(ChiefOccupant chiefOccupant)
        {
            return chiefOccupant.Penalties.Any(x => x.PenaltyStatus == Domain.Enums.PenaltyStatus.Active);
        }

        private bool IsDanger(ChiefOccupant chiefOccupant)
        {
            return chiefOccupant.ApplicationRejectedCount > 0;
        }

    }
}
