using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Shared.Middleware.ExceptionHandler.ErrorModels
{
	/// <summary>
	/// Error Message With Stack Trace Model
	/// </summary>
	public class ErrorMessageWithStackTraceModel : ErrorMessageModel
	{
		/// <summary>
		/// StackTrace
		/// </summary>
		public string StackTrace { get; set; }
	}
}
