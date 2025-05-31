using CIDRS.Shared.Installer.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Shared.Installer.Installers
{
    /// <summary>
    /// The class that contains Authorization Installer
    /// </summary>
    public class AuthorizationInstaller : IStartupConfiguration
    {
        /// <summary>
        /// The method Configure Authorization
        /// </summary>
        /// <param name="configuration">configuration</param>
        /// <param name="app">app</param>
        public void Configure(IConfiguration configuration, IApplicationBuilder app)
        {
            app.UseAuthorization();
        }
    }
}
