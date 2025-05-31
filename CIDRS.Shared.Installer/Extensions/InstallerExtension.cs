using CIDRS.Shared.Installer.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CIDRS.Shared.Installer.Extensions
{
    /// <summary>
    /// The Calss that contains extension methods of Insallers
    /// </summary>
    public static class InstallerExtension
    {
        /// <summary>
        ///  Install all service in assembly which are extends IInstaller
        /// </summary>
        /// <param name="services">services</param>
        /// <param name="configuration">configuration</param>
        /// <param name="installerAssembly">installerAssembly</param>
        /// <param name="startupAssembly">startupAssembly</param>
        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration, Assembly installerAssembly, Assembly startupAssembly)
        {
            //Get all Installer class instances
            var installers = installerAssembly.ExportedTypes.Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance).Cast<IInstaller>().ToList();
            //Order by Installer
            var OrderedInstallers = installers.OrderBy(i => i.OrderByKey).ToList();
            //Insatall Services
            OrderedInstallers.ForEach(installer => installer.InstallServices(configuration, services, startupAssembly));
        }

        /// <summary>
        /// Set All Configuration in assembly which are extends IStartupConfiguration
        /// </summary>
        /// <param name="app">app</param>
        /// <param name="configuration">configuration</param>
        /// <param name="assembly">assembly</param>
        public static void ConfigurationInAssembly(this IApplicationBuilder app, IConfiguration configuration, Assembly assembly)
        {
            //Get all IStartupConfiguration class instances
            var configurations = assembly.ExportedTypes.Where(x => typeof(IStartupConfiguration).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance).Cast<IStartupConfiguration>().ToList();
            //configure configurations
            configurations.ForEach(config => config.Configure(configuration, app));
        }
    }
}
