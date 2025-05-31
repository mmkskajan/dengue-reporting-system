using CIDRS.Shared.Middleware.ExceptionHandler.ErrorModels;
using CIDRS.Shared.Middleware.ExceptionHandler.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CIDRS.Shared.Middleware.ExceptionHandler
{
    /// <summary>
	/// The class that contains Exception Handle methods
	/// </summary>
	public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private HttpContext _context;

        /// <summary>
        /// The constructor Exception Handle Middleware
        /// </summary>
        /// <param name="next">next</param>
        /// <param name="configuration">configuration</param>
        /// <param name="logger">logger</param>
        public ExceptionHandlingMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// The method of http request delegate
        /// </summary>
        /// <param name="context">context</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            _context = context;

            try
            {
                await _next(context);

            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        #region Exception private methods
        /// <summary>
        /// The method of Handle Exception Async
        /// </summary>
        /// <param name="exception">exception</param>
        /// <returns></returns>
        private Task HandleExceptionAsync(Exception exception)
        {
            if (exception is BadRequestException)
            {
                return HandleBadRequestExceptionAsync(exception); // Bad Request Exception Async
            }
            else if (exception is NotFoundException)
            {
                return HandleNotFoundExceptionAsync(exception); // Handle Not Found Exception Async
            }
            else if (exception is ForbiddenException)
            {
                return HandleCustomExceptionAsync(exception, HttpStatusCode.Forbidden); // Handle Custom Exception Async
            }
            else
            {
                return HandleCommonExceptionAsync(exception); //Handle Common Exception Async
            }
        }

        /// <summary>
        /// The Method of Handle Bad Request Exception Async
        /// </summary>
        /// <param name="exception">exception</param>
        /// <returns></returns>
        private Task HandleBadRequestExceptionAsync(Exception exception)
        {
            return HandleCustomExceptionAsync(exception, HttpStatusCode.BadRequest); // Handle Custom Exception Async
        }

        /// <summary>
        /// The method of Handle Not Found Exception Async
        /// </summary>
        /// <param name="exception">exception</param>
        /// <returns></returns>
        private Task HandleNotFoundExceptionAsync(Exception exception)
        {
            return HandleCustomExceptionAsync(exception, HttpStatusCode.NotFound); // Handle Custom Exception Async
        }

        /// <summary>
        /// The method of Handle Custom Exception Async
        /// </summary>
        /// <param name="exception">exception</param>
        /// <param name="statusCodeOfResponse">statusCodeOfResponse</param>
        /// <returns></returns>
        private Task HandleCustomExceptionAsync(Exception exception, HttpStatusCode statusCodeOfResponse)
        {
            var referenceId = _context.TraceIdentifier;
            var exceptionDescription = exception.Message;
            var errorName = GetErrorName(statusCodeOfResponse);

            _logger.LogError(exception, "{errorName}. Reason: {exceptionDescription}. ReferenceId: {referenceId}", errorName, exceptionDescription, referenceId);

            var responseMessage = GenerateAndSerializeResponseMessage(exception, exceptionDescription, referenceId);

            _context.Response.ContentType = "application/json";
            _context.Response.StatusCode = (int)statusCodeOfResponse;
            return _context.Response.WriteAsync(responseMessage);
        }

        /// <summary>
        /// The method of Handle Common Exception Async
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private Task HandleCommonExceptionAsync(Exception exception)
        {
            var referenceId = _context.TraceIdentifier;
            var exceptionDescription = exception.Message;

            _logger.LogError(exception, "Reason: {exceptionDescription}. ReferenceId: {referenceId}", exceptionDescription, referenceId);

            var responseMessage = GenerateAndSerializeResponseMessage(exception, exceptionDescription, referenceId);
            _context.Response.ContentType = "application/json";
            _context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return _context.Response.WriteAsync(responseMessage);
        }

        /// <summary>
        /// The method of Generate And Serialize Response Message
        /// </summary>
        /// <param name="exception">exception</param>
        /// <param name="message">message</param>
        /// <param name="referenceId">referenceId</param>
        /// <returns></returns>
        private string GenerateAndSerializeResponseMessage(Exception exception, string message, string referenceId)
        {
            var errorMessageModel = new ErrorMessageModel()
            {
                ErrorMessage = message,
                ReferenceId = referenceId
            };

            if (IsIncludeStackTrace())
            {
                // Different models are used because the first model after serialization shouldn't include "StackTrace" attribute.
                errorMessageModel = new ErrorMessageWithStackTraceModel()
                {
                    ErrorMessage = message,
                    ReferenceId = referenceId,
                    StackTrace = exception.ToString()
                };
            }

            var errorMessage = JsonConvert.SerializeObject(errorMessageModel);
            return errorMessage;
        }
        /// <summary>
        /// The method of Include Stack Trace
        /// </summary>
        /// <returns></returns>
        private bool IsIncludeStackTrace()
        {
            return _configuration.GetSection("GlobalExceptionHandling").GetValue("IsIncludeStackTraceInAPIErrorResponse", false);
        }

        /// <summary>
        /// The method of Get Error Name
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        private string GetErrorName(HttpStatusCode statusCode)
        {
            string name = string.Empty;

            if (statusCode == HttpStatusCode.NotFound)
                name = "Not Found Error";
            else if (statusCode == HttpStatusCode.BadRequest)
            {
                name = "Bad Request Error";
            }

            return name;
        }
        #endregion
    }
}
