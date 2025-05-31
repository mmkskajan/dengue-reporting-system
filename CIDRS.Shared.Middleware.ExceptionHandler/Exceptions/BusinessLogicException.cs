using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Shared.Middleware.ExceptionHandler.Exceptions
{
	/// <summary>
	/// Custom exception to be used when throwing business specific error messages to the user.
	/// </summary>
	public class BusinessLogicException : BadRequestException
	{
		/// <summary>
		/// Default Constructor BusinessLogicException
		/// </summary>
		public BusinessLogicException()
		{
		}

		/// <summary>
		/// Constructor BusinessLogicException
		/// </summary>
		/// <param name="message"></param>
		public BusinessLogicException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Constructor BusinessLogicException
		/// </summary>
		/// <param name="message"></param>
		/// <param name="inner"></param>
		public BusinessLogicException(string message, Exception inner)
			: base(message, inner)
		{
		}

	}
}
