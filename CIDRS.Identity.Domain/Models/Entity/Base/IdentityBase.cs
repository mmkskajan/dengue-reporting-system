using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CIDRS.Identity.Domain.Models.Entity.Base
{
    /// <summary>
    /// The class Identity Base
    /// </summary>
    public class IdentityBase
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public int Id { get; set; }
    }
}
