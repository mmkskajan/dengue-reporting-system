using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CIDRS.Identity.Models.Request
{
    /// <summary>
    /// Data transfer object for Password Request
    /// </summary>
    public class PasswordResetRequest
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required, EmailAddress]
        public string Email { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        [Required]
        public string Token { get; set; }
        /// <summary>
        /// new password
        /// </summary>
        [Required]
        public string newPassword { get; set; }
    }
}
