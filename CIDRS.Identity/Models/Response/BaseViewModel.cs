using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Identity.Models.Response
{
    /// <summary>
    /// Data transfer object for View Model Base
    /// </summary>
    public class BaseViewModel
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public string Identifier { get; set; }
        /// <summary>
        /// Created Date and Time
        /// </summary>
        public Nullable<DateTime> CreatedAt { get; set; }
        /// <summary>
        /// Updated Date and Time
        /// </summary>
        public Nullable<DateTime> UpdatedAt { get; set; }
        /// <summary>
        /// Archived Date and Time
        /// </summary>
        public Nullable<DateTime> ArchivedAt { get; set; }
    }
}
