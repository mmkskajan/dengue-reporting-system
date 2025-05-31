using CIDRS.Identity.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Identity.Domain.Models.Entity
{
    /// <summary>
    /// Application User Identity Entity Model
    /// </summary>
    public class ApplicationUser : IdentityUser
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
        /// Resfresh Tokens
        /// </summary>
        public IList<RefreshToken> RefreshTokens { get; set; }

        /// <summary>
        /// Application User Type (Admin, PHI , Chief Occupant)
        /// </summary>
        public ApplicationUserType UserType { get; set; }

        public bool hasTempPassword { get; set; }

        /// <summary>
        /// The Constructor Application user
        /// </summary>
        public ApplicationUser()
        {
            RefreshTokens = new List<RefreshToken>();
        }

    }
}
