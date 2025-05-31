using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CIDRS.Domain.Interfaces
{
    public interface ISuperAdmin
    {

        public string Email { get; set; }

        /// <summary>
        /// Country
        /// </summary>
        [Required]
        public string UserId { get; set; }

        public string Name { get; set; }



    }
}
