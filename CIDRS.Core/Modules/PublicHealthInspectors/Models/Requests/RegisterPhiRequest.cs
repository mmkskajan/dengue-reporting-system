using CIDRS.Identity.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Core.Modules.PublicHealthInspectors.Models.Requests
{
    public class RegisterPhiRequest : UserRegistrationRequest
    {
        public int DistrictId { get; set; }
        public int MohAreaId { get; set; }
    }
}
