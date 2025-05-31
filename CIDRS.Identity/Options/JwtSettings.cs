using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Identity.Options
{
    /// <summary>
    /// Jwt Setting Option
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// Client Secret
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Token Life Time
        /// </summary>
        public TimeSpan TokenLifeTime { get; set; }

    }
}
