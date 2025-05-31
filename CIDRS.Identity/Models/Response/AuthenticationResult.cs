using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Identity.Models.Response
{
    /// <summary>
    /// Data transfer object for Authentication Result
    /// </summary>
    public class AuthenticationResult
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Refresh Token
        /// </summary>
        public string RefreshToken { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public String Id { get; set; }
        /// <summary>
        /// Error
        /// </summary>
        public IEnumerable<string> Errors { get; set; }
        /// <summary>
        /// User 
        /// </summary>
        public ApplicationUserVM User { get; set; }


    }
}
