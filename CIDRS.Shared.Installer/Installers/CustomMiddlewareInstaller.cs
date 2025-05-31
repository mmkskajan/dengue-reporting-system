using CIDRS.Shared.Installer.Interface;
using CIDRS.Shared.Middleware.ExceptionHandler.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Shared.Installer.Installers
{
    /// <summary>
    /// The class that contains Custom middleware installer
    /// </summary>
    public class CustomMiddlewareInstaller : IStartupConfiguration
    {
        /// <summary>
        /// The Method Configure Custom Exception Middleware
        /// </summary>
        /// <param name="configuration">configuration</param>
        /// <param name="app">app</param>
        public void Configure(IConfiguration configuration, IApplicationBuilder app)
        {
            app.ConfigureCustomExceptionMiddleware();
        }
    }
}
