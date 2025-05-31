using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Shared.Utility.SmsManipulator.Options
{
    public class SmsConfiguration
    {
        public string Url { get; set; }
        public int UserId { get; set; }
        public string ApiKey { get; set; }
        public string SenderId { get; set; }
        public bool IsEnabledSendSms { get; set; }
    }
}
