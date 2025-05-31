using CIDRS.Core.Modules.Statistics.Models.Request;
using CIDRS.Core.Modules.Statistics.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Core.Modules.Statistics.Extensions
{
    public static class StatisticsExtension
    {
        public static ApplicationStatisticsSearchRequest ToServiceRequest(this GetApplicationStatisticsDetailsQuery query)
        {
            return new ApplicationStatisticsSearchRequest()
            {
                IsRelative = query.IsRelative,
                PaginationOption = query.PaginationOption,
                TimeFrequency = query.TimeFrequency,
                Type = query.Type
            };
        }

        public static EnvironmentStatisticsSearchRequest ToServiceRequest(this GetEnvironmentStatisticsDetailsQuery query)
        {
            return new EnvironmentStatisticsSearchRequest()
            {
                IsRelative = query.IsRelative,
                PaginationOption = query.PaginationOption,
                Status = query.Status
            };
        }

        public static PenalizationStatisticsSearchRequest ToServiceRequest(this GetPenalizationStatisticsDetailsQuery query)
        {
            return new PenalizationStatisticsSearchRequest()
            {
                IsRelative = query.IsRelative,
                PaginationOption = query.PaginationOption,
                Status = query.Status
            };
        }

    }
}
