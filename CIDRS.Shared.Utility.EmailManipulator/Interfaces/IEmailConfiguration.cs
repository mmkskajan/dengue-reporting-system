using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Shared.Utility.EmailManipulator.Interfaces
{
    /// <summary>
    /// The Interface Email Configuration
    /// </summary>
    public interface IEmailConfiguration
    {
        /// <summary>
        /// smtp server
        /// </summary>
        public string SmtpServer { get; set; }
        /// <summary>
        /// smtp port
        /// </summary>
        public int SmtpPort { get; set; }
        /// <summary>
        /// smtp user name
        /// </summary>
        public string SmtpUsername { get; set; }
        /// <summary>
        /// smtp password
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
