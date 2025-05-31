using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Domain.Options
{
    /// <summary>
    /// Model to manupulate swagger document contents
    /// </summary>
    public class SwaggerOptions
    {
        /// <summary>
        /// JsonRoute
        /// </summary>
        public string JsonRoute { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// UiEndpoint
        /// </summary>
        public string UiEndpoint { get; set; }

    }
}
