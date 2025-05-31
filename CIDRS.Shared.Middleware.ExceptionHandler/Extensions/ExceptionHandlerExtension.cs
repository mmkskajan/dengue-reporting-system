
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CIDRS.Shared.Middleware.ExceptionHandler.Extensions
{
	/// <summary>
	/// The class that contains exception handler methods
	/// </summary>
	public static class ExceptionHandlerExtension
	{
		/// <summary>
		/// The method of Configure Exception Handle
		/// </summary>
		/// <param name="app">app</param>
		public static void ConfigureExceptionHandling(this IApplicationBuilder app)
		{
            app.UseExceptionHandler(options =>
			{
				options.Run(async context =>
				{
					context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					context.Response.ContentType = "text/html";
					var ex = context.Features.Get<IExceptionHandlerFeature>();
					if (ex != null)
					{
						var err = $"{ex.Error.Message}. {ex.Error.StackTrace }";
						await context.Response.WriteAsync(err).ConfigureAwait(false);
					}
				});
			});
		}

		/// <summary>
		/// Converts the list of errors within the model state into an easier format 
		/// so that it can be shown to the user.
		/// </summary>
		/// <param name="modelStateDictionary">The state of the model, i.e the errors</param>
		/// <returns>A carriage return list of error strings.</returns>
		public static string ErrorsToStringList(this ModelStateDictionary modelStateDictionary)
		{
			var errorStrings = new StringBuilder();

			foreach (var modelState in modelStateDictionary.Values)
			{
				foreach (var error in modelState.Errors)
				{
					errorStrings.Append(error.ErrorMessage + Environment.NewLine);
				}
			}

			return errorStrings.ToString();
		}

		/// <summary>
		/// The method of Configure Custom Exception Middleware
		/// </summary>
		/// <param name="app"></param>
		public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
		{
			app.UseMiddleware<ExceptionHandlingMiddleware>();
		}
	}
}
