using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Identity.Models.Response
{
    /// <summary>
    /// Data transfer object for Register Results
    /// </summary>
    public class RegisterResult : AuthenticationResult
    {
        /// <summary>
        /// User Id
        /// </summary>
        public string UserId { get; set; }

    }
}
