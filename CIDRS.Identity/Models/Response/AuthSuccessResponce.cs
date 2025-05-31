using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Identity.Models.Response
{
    /// <summary>
    /// Data transfer object for Authentication Success
    /// </summary>
    public class AuthSuccessResponce
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Refresh Token
        /// </summary>
        public string RefreshToken { get; set; }

        public ApplicationUserVM User { get; set; }
    }
}
