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
    /// The class that contains default service Installer
    /// </summary>
    public class DefaultInstaller : IInstaller, IStartupConfiguration
    {
        public int OrderByKey
        { get { return 3; } } // Order by key install according to return value 

        /// <summary>
        /// The method Https Redirection 
        /// </summary>
        /// <param name="configuration">configuration</param>
        /// <param name="app">app</param>
        public void Configure(IConfiguration configuration, IApplicationBuilder app)
        {
            //app.ConfigureCustomExceptionMiddleware();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

        /// <summary>
        /// The Method Install services
        /// </summary>
        /// <param name="configuration">configuration</param>
        /// <param name="services">services</param>
        /// <param name="startupAssembly">startupAssembly</param>
        public void InstallServices(IConfiguration configuration, IServiceCollection services, Assembly startupAssembly)
        {
            services.AddMvc();

        }

    }
}
