using CIDRS.Shared.Common.Api;
using CIDRS.Shared.Middleware.ExceptionHandler.Exceptions;
using CIDRS.Shared.Utility.SmsManipulator.Models;
using CIDRS.Shared.Utility.SmsManipulator.Options;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CIDRS.Shared.Utility.SmsManipulator.Services
{
    public class SmsManipulatorService : ISmsManipulatorService
    {
        private readonly IApiService _apiService;
        private readonly SmsConfiguration _smsConfiguration;

        public SmsManipulatorService(IApiService apiService, SmsConfiguration smsConfiguration)
        {
            _apiService = apiService;
            _smsConfiguration = smsConfiguration;
        }
        public async Task SendBulkSmsAsync(IEnumerable<SmsNotification> smsNotifications)
        {
            foreach (var notification in smsNotifications)
            {
                foreach (var to in notification.To)
                {
                    if (IsValidTo(to))
                    {
                        foreach (var message in notification.Messages)
                        {
                            var request = GetNotifyRequest();
                            request.to = to;
                            request.message = message;

                            await _apiService.PostAsync<NotifyResponse, NotifyRequest>(_smsConfiguration.Url, string.Empty, request, false);
                        }

                    }
                }
            }
        }

        public async Task SendSmsAsync(string to, string message)
        {
            if (IsValidTo(to) && _smsConfiguration.IsEnabledSendSms)
            {
                var request = GetNotifyRequest();
                request.to = to;
                request.message = message;

                await _apiService.PostAsync<NotifyResponse, NotifyRequest>(_smsConfiguration.Url, string.Empty, request, false);
            }
        }


        private bool IsValidTo(string to)
        {
            return !string.IsNullOrWhiteSpace(to) && to.Length == 11 && Regex.IsMatch(to, "^[0-9]+$") && to.StartsWith("94");
        }

        private NotifyRequest GetNotifyRequest()
        {
            if (_smsConfiguration == null)
                throw new BusinessLogicException("SMS Configuration Not Valid!!");

            return new NotifyRequest()
            {
                api_key = _smsConfiguration.ApiKey,
                sender_id = _smsConfiguration.SenderId,
                user_id = _smsConfiguration.UserId
            };
        }
    }
}
