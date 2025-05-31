using CIDRS.Core.Modules.Applications.Models.Request;
using CIDRS.Domain.Models.Entity.Applications;
using CIDRS.Shared.Common.Api.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.Applications.Services
{
    public interface IReportingApplicationService
    {
        Task<ResponseResult<ReportingApplication>> StartReportingApplication(int? chiefOccupantId = null, bool publicSurroundingComplainet = false);
        Task<ResponseResult<ReportingApplication>> GetReportingApplication(int id);

        Task<ResponseResult<ReportingApplication>> AddSurroundingSet(BaseSurroundingSetRequest baseSurroundingSet, int chiefOccupantId);
        Task<ResponseResult<ReportingApplication>> AddSurroundingSet(SurroundingSetRequest surroundingSet);
        Task<ResponseResult<ReportingApplication>> AddSurroundingSet(BaseSurroundingSetRequest baseSurroundingSet);
        Task<List<SurroundingSet>> GetPendingBaseSurroundingSets(int? chiefOccupantId);
        Task<ResponseResult<ReportingApplication>> CompleteAsync(int? chiefOccupantId = null, bool isPublicSurroundingApplication = false);
    }
}
