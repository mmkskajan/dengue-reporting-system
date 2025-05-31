using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CIDRS.Identity.Models.Request
{
    /// <summary>
    /// Data transfer object for Confirm Email
    /// </summary>
    public class ConformEmailRequest
    {     
        /// <summary>
        /// Token
        /// </summary>
        [Required]
        public string Token { get; set; }
    }
}
