using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CIDRS.Shared.Middleware.ExceptionHandler.Exceptions
{
    /// <summary>
    /// Custom exception to be used when throwing forbidden error messages to the user.
    /// </summary>
    public class ForbiddenException : Exception
    {
        /// <summary>
        /// Constructor ForbiddenException
        /// </summary>
        public ForbiddenException()
        {
        }

        /// <summary>
        /// Constructor ForbiddenException
        /// </summary>
        /// <param name="message"></param>
        public ForbiddenException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructor ForbiddenException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ForbiddenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Constructor ForbiddenException
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ForbiddenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
