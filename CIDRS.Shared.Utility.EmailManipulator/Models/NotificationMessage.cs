using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CIDRS.Shared.Utility.EmailManipulator.Models
{
    /// <summary>
    /// The Class of Notification Message
    /// </summary>
    public class NotificationMessage
    {
        /// <summary>
        /// List of mail address To
        /// </summary>
        public List<MailboxAddress> To { get; set; }
        /// <summary>
        /// Subject
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The constructor of Notification Message
        /// </summary>
        /// <param name="to">to</param>
        /// <param name="subject">subject</param>
        /// <param name="content">content</param>
        public NotificationMessage(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress(x)));
            Subject = subject;
            Content = content;
        }
    }
}
