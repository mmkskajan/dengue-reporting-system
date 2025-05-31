using CIDRS.Shared.Utility.EmailManipulator.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Shared.Utility.EmailManipulator.Options
{
    /// <summary>
    /// The class of Email Configuration
    /// </summary>
    public class EmailConfiguration : IEmailConfiguration
    {
        /// <summary>
        /// Smtp Server 
        /// </summary>
        public string SmtpServer { get; set; }
        /// <summary>
        /// Smtp Port
        /// </summary>
        public int SmtpPort { get; set; }
        /// <summary>
        /// Smtp User name
        /// </summary>
        public string SmtpUsername { get; set; }
        /// <summary>
        /// Smtp Password
        /// </summary>
        public string SmtpPassword { get; set; }
        /// <summary>
        /// From
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Bcc
        /// </summary>
        public string Bcc { get; set; }
    }
}
