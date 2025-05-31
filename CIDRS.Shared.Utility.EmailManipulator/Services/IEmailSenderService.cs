using CIDRS.Shared.Utility.EmailManipulator.Models;
using System.Threading.Tasks;

namespace CIDRS.Shared.Utility.EmailManipulator.Services
{
    /// <summary>
    /// The Interface IEmail Sender Service
    /// </summary>
    public interface IEmailSenderService
    {
        /// <summary>
        /// The Method of send email async
        /// </summary>
        /// <param name="message">message</param>
        /// <returns></returns>
        Task SendEmailAsync(NotificationMessage message);
    }
}