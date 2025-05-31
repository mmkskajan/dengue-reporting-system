using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CIDRS.Identity.Models.Request
{
    /// <summary>
    /// Data transfer object for Refresh Token
    /// </summary>
    public class RefreshTokenRequest
    {
        /// <summary>
        /// Token
        /// </summary>
        [Required]
        public string Token { get; set; }
        /// <summary>
        /// Refresh Token
        /// </summary>
        [Required]
        public string RefreshToken { get; set; }
    }
}
