using CIDRS.Identity.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CIDRS.Identity.Models.Request
{
    public class UpdateUserRequest
    {
        /// <summary>
        /// Full Name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Phone Number
        /// </summary>
        public string PhoenNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required, EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Roles
        /// </summary>
        public List<string> RoleIds { get; set; }

        /// <summary>
        /// Application User Type (Admin, Service provider , Customer)
        /// </summary>
        [Required]
        public ApplicationUserType UserType { get; set; }
    }
}
