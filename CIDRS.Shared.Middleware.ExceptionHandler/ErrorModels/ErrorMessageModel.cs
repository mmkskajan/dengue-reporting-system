using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Shared.Middleware.ExceptionHandler.ErrorModels
{
	/// <summary>
	/// Error Message Model
	/// </summary>
	public class ErrorMessageModel
	{
		/// <summary>
		/// Message
		/// </summary>
		public string ErrorMessage { get; set; }
		/// <summary>
		/// ReferenceId
		/// </summary>
		public string ReferenceId { get; set; }
	}
}
