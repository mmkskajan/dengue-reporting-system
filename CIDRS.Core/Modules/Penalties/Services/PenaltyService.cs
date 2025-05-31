using CIDRS.Core.Modules.ChiefOccupants.Services;
using CIDRS.Domain.Models.Entity.ChiefOccupants;
using CIDRS.Infrastructure;
using CIDRS.Shared.Common.Api.Models;
using CIDRS.Shared.Common.Extensions;
using CIDRS.Shared.Utility.SmsManipulator.Extensions;
using CIDRS.Shared.Utility.SmsManipulator.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.Penalties.Services
{
    public class PenaltyService : IPenaltyService
    {
        private readonly ApplicationDataContext _dataContext;
        private readonly ISmsManipulatorService _smsSender;

        public PenaltyService(ApplicationDataContext dataContext, ISmsManipulatorService smsSender)
        {
            _dataContext = dataContext;
            _smsSender = smsSender;
        }

        public async Task<bool> ResolveRedNoticessAsync(int chiefOccupantId)
        {
            var chiefOccupantResponse = await GetChiefOccupantById(chiefOccupantId);

            if (!chiefOccupantResponse.Succeeded)
                return false;

            var chiefOccupant = chiefOccupantResponse.Result;
            var redNotices = chiefOccupant.Penalties.Where(x => x.PenaltyStatus == Domain.Enums.PenaltyStatus.Active && x.PenaltyType == Domain.Enums.PenaltyType.RedNotice).ToList();

            redNotices.ForEach(x =>
            {
                x.PenaltyStatus = Domain.Enums.PenaltyStatus.Resolved;
                x.ResolvedDate = DateTime.Today;
                x.SetUpdatedTime();
                _dataContext.Penalties.Update(x);
            });

            await _dataContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddPenaltyAsync(int chiefOccupantId)
        {
            var chiefOccupantResponse = await GetChiefOccupantById(chiefOccupantId);

            if (!chiefOccupantResponse.Succeeded)
                return false;

            var chiefOccupant = chiefOccupantResponse.Result;

            if (chiefOccupant.ApplicationRejectedCount >= 2)
            {
                var penaltty = new Penalty()
                {
                    ChiefOccupant = chiefOccupant,
                    ChiefOccupantId = chiefOccupantId,
                    DueDate = DateTime.Today.AddDays(7),
                    PenaltyStatus = Domain.Enums.PenaltyStatus.Active,
                    PenaltyType = Domain.Enums.PenaltyType.Fee
                };
                penaltty.SetCreatedTime();
                await _dataContext.Penalties.AddAsync(penaltty);
                await _dataContext.SaveChangesAsync();
                return true;
            }
            else
            {
                var penaltty = new Penalty()
                {
                    ChiefOccupant = chiefOccupant,
                    ChiefOccupantId = chiefOccupantId,
                    DueDate = DateTime.Today.AddDays(7),
                    PenaltyStatus = Domain.Enums.PenaltyStatus.Active,
                    PenaltyType = Domain.Enums.PenaltyType.RedNotice
                };
                penaltty.SetCreatedTime();
                await _dataContext.Penalties.AddAsync(penaltty);
                await _dataContext.SaveChangesAsync();
                return true;
            }
        }

        public async Task<ResponseResult<bool>> ResolveAsync(int id)
        {
            var penalty = await _dataContext.Penalties.Include(x => x.ChiefOccupant).FirstOrDefaultAsync(x => x.ResolvedDate == null && x.Id == id);

            if (penalty == null)
                return new ResponseResult<bool>()
                {
                    Errors = new[] { "No Active Penalty Found!!" },
                    Result = false,
                    Succeeded = false
                };
            penalty.PenaltyStatus = Domain.Enums.PenaltyStatus.Resolved;
            penalty.ResolvedDate = DateTime.Today;
            penalty.SetUpdatedTime();
            _dataContext.Penalties.Update(penalty);
            await _dataContext.SaveChangesAsync();
            await NotifyChiefcOcupantAsync(penalty.ChiefOccupant);
            return new ResponseResult<bool>()
            {
                Errors = null,
                Result = true,
                Succeeded = true
            };
        }

        private async Task<ResponseResult<ChiefOccupant>> GetChiefOccupantById(int id)
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

        private async Task NotifyChiefcOcupantAsync(ChiefOccupant chiefOccupant)
        {
            try
            {
                string fileName = string.Empty;
                string templatePath = string.Empty;
                string content = string.Empty;


                fileName = "NotifyCOResolvePenalty.txt";
                templatePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Templates\\SMS", fileName);
                content = await templatePath.Render(new string[] { chiefOccupant?.FullName });

                await _smsSender.SendSmsAsync(chiefOccupant.PhoneNumber, content);
            }
            catch
            {

            }
        }
    }
}
