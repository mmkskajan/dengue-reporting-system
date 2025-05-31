using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Core.Modules.Applications.Models.Request
{
    public class BaseSurroundingSetRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
