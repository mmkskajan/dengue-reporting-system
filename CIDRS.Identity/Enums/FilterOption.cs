using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CIDRS.Identity.Enums
{
    /// <summary>
    /// The Enum Filter Option
    /// </summary>
    public enum FilterOption
    {
        /// <summary>
        /// FilterOptions All(1), Archived(2), Active(3)
        /// </summary>
        [Description("All")]
        All = 1,

        /// <summary>
        /// Archived
        /// </summary>
        [Description("Archived")]
        Archived = 2,

        /// <summary>
        /// Active
        /// </summary>
        [Description("Active")]
        Active = 3,
    }
}
