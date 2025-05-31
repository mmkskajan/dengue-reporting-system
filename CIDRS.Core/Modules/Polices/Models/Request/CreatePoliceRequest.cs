using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Core.Modules.Polices.Models.Request
{
    public class CreatePoliceRequest
    {
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public int PoliceStationId { get; set; }
        
    }
}
