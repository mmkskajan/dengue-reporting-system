using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CIDRS.Identity.Enums
{
    /// <summary>
    /// The Enum TokenOptionsList
    /// </summary>
    public enum AuthenticationOption
    {
        /// <summary>
        /// Login
        /// </summary>
        [Description("Login")]
        login = 1,

        /// <summary>
        /// Forgot Password
        /// </summary>
        [Description("ForgotPassword")]
        forgotPassword = 2,

        /// <summary>
        /// Email Conformation
        /// </summary>
        [Description("EmailConformation")]
        EmailConformation = 3,


        /// <summary>
        /// Register User By Admin
        /// </summary>
        [Description("RegisterUserByAdmin")]
        RegisterUserByAdmin = 4
    }
}
