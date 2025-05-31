using CIDRS.Core.Modules.Applications.Commands;
using CIDRS.Core.Modules.Applications.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Core.Modules.Applications.Extensions
{
    public static class ReportingApplicationExtension
    {
        public static BaseSurroundingSetRequest ToServiceRequest(this AddBaseSurroundingSetCommand command)
        {
            return new BaseSurroundingSetRequest()
            {
                Description = command.Description,
                Image = command.Image,
                Latitude = command.Latitude,
                Longitude = command.Longitude,
                Name = command.Name
            };
        }

        public static BaseSurroundingSetRequest ToServiceRequest(this AddPublicSurroundinSetCommand command)
        {
            return new BaseSurroundingSetRequest()
            {
                Description = command.Description,
                Image = command.Image,
                Latitude = command.Latitude,
                Longitude = command.Longitude,
                Name = command.Name
            };
        }

        public static SurroundingSetRequest ToServiceRequest(this AddSurroundingSetCommand command)
        {
            return new SurroundingSetRequest()
            {
                Description = command.Description,
                Image = command.Image,
                Latitude = command.Latitude,
                Longitude = command.Longitude,
                RelativeId = command.RelativeId
            };
        }
    }
}
