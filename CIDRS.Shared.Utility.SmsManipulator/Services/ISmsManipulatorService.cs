using CIDRS.Shared.Utility.SmsManipulator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CIDRS.Shared.Utility.SmsManipulator.Services
{
    public interface ISmsManipulatorService
    {
        Task SendSmsAsync(string to, string message);
        Task SendBulkSmsAsync(IEnumerable<SmsNotification> smsNotifications);
    }
}
