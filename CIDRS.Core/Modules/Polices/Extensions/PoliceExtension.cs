using CIDRS.Core.Modules.Polices.Commands;
using CIDRS.Core.Modules.Polices.Models.Request;
using CIDRS.Domain.Models.Entity.Polices;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Core.Modules.Polices.Extensions
{
    public static class PoliceExtension
    {
        public static Police ToEntityModel(this CreatePoliceRequest request)
        {
            var police = new Police()
            {
                FullName = request.FullName,
                Mobile = request.Mobile,
                PoliceStationId = request.PoliceStationId
            };

            return police;
        }

        public static CreatePoliceRequest ToServiceRequest(this CreatePoliceCommand command)
        {
            var police = new CreatePoliceRequest()
            {
                FullName = command.FullName,
                Mobile = command.Mobile,
                PoliceStationId = command.PoliceStationId
            };

            return police;
        }

        public static PoliceSearchRequest ToServiceRequest(this IndexPolicesCommand command)
        {
            var police = new PoliceSearchRequest()
            {
                BasicSearchValue = command.BasicSearchValue,
                MohAreaId = command.MohAreaId,
                PaginationOption = command.PaginationOption
            };

            return police;
        }
    }
}
