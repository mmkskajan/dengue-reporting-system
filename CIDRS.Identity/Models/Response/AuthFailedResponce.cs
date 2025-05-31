using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Identity.Models.Response
{
    /// <summary>
    /// Data transfer object for Authentication Failed
    /// </summary>
    public class AuthFailedResponce
    {
        /// <summary>
        /// Errors
        /// </summary>
        public IEnumerable<string> Errors { get; set; }
    }
}
