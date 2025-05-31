using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Shared.Utility.SmsManipulator.Models
{

    public class NotifyRequest
    {
        public int user_id { get; set; }
        public string api_key { get; set; }
        public string sender_id { get; set; }
        public string to { get; set; }
        public string message { get; set; }
    }

}
