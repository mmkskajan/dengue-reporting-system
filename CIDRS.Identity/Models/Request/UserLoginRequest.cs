using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CIDRS.Identity.Models.Request
{
    /// <summary>
    /// Data transfer object for User Login
    /// </summary>
    public class UserLoginRequest
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required, EmailAddress]
        public string Email { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        [Required]
        public string Password { get; set; }

    }
}
