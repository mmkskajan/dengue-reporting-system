using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Shared.Utility.SmsManipulator.Models
{
    public class SmsNotification
    {
        public string[] To { get; set; }
        public string[] Messages { get; set; }
    }
}
