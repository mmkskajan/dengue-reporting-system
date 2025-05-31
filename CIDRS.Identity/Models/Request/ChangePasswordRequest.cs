using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CIDRS.Identity.Models.Request
{
    
    /// <summary>
    /// Data transfer object for change password
    /// </summary>
    public class ChangePasswordRequest
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required, EmailAddress]
        public string Email { get; set; }
        /// <summary>
        /// Current Password
        /// </summary>
        [Required]
        public string currentPassword { get; set; }
        /// <summary>
        /// New Password
        /// </summary>
        [Required]
        public string newPassword { get; set; }
    }
}
