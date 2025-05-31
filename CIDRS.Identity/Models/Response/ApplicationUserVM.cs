using CIDRS.Identity.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Identity.Models.Response
{
    public class ApplicationUserVM
    {
        public string FullName { get; set; }
        /// <summary>
        /// Avatar
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// Created Date and Time
        /// </summary>
        public Nullable<DateTime> CreatedAt { get; set; }
        /// <summary>
        /// Updated Date and Time
        /// </summary>
        public Nullable<DateTime> UpdatedAt { get; set; }
        /// <summary>
        /// Archived Date and Time
        /// </summary>
        public Nullable<DateTime> ArchivedAt { get; set; }
        
        /// <summary>
        /// Application User Type (Admin, PHI , Chief Occupant)
        /// </summary>
        public ApplicationUserType UserType { get; set; }
        /// <summary>
        /// Username
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Phone Number
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// User Id
        /// </summary>
        public string Id { get; set; }

        public bool hasTempPassword { get; set; }

        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }

        public bool IsActive { get; set; }
    }
}
