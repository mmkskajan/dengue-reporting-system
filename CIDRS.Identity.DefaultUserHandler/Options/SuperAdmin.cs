using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Identity.DefaultUserHandler.Options
{
    /// <summary>
    /// Data transfer object for Super Admin
    /// </summary>
    public class SuperAdmin
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// FullName
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// MobileNumber
        /// </summary>
        public string MobileNumber { get; set; }
     
    }
}
