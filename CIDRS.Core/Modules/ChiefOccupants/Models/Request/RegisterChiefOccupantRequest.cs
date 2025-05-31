using CIDRS.Identity.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Core.Modules.ChiefOccupants.Models.Request
{
    public class RegisterChiefOccupantRequest : UserRegistrationRequest
    {
        public string Address { get; set; }
        public int DistrictId { get; set; }
        public int MohAreaId { get; set; }
    }
}
