using CIDRS.Shared.Installer.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CIDRS.Shared.Installer.Installers
{
    /// <summary>
    /// The class that contains cors policy Installer
    /// </summary>
    public class CorsPolicyInstaller : IInstaller, IStartupConfiguration
    {
        public int OrderByKey
        { get { return 2; } } // Order by key install according to return value 

        /// <summary>
        /// The method configure core policy
        /// </summary>
        /// <param name="configuration">configuration</param>
        /// <param name="app">app</param>
        public void Configure(IConfiguration configuration, IApplicationBuilder app)
        {
            app.UseCors("CorsPolicy");

        }

        /// <summary>
        /// Install Services for cors policy
        /// </summary>
        /// <param name="configuration">configuration</param>
        /// <param name="services">services</param>
        public void InstallServices(IConfiguration configuration, IServiceCollection services, Assembly startupAssembly)
        {
            //allow Cross domain request
            services.AddCors(options =>
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    //.AllowCredentials();
                })
            );
        }


    }
}
