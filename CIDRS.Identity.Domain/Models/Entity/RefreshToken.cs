using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CIDRS.Identity.Domain.Models.Entity
{
    /// <summary>
    /// The class refresh token
    /// </summary>
    public class RefreshToken
    {
        /// <summary>
        /// Token
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Token { get; set; }
        /// <summary>
        /// Jwt Id
        /// </summary>
        public string JwtId { get; set; }
        /// <summary>
        /// Creation Date
        /// </summary>
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// Expiry Date
        /// </summary>
        public DateTime ExpiryDate { get; set; }
        /// <summary>
        /// Used
        /// </summary>
        public bool Used { get; set; }
        /// <summary>
        /// Invalidated
        /// </summary>
        public bool Invalidated { get; set; }
        /// <summary>
        /// User Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// User
        /// </summary>
        public ApplicationUser User { get; set; }
    }
}
