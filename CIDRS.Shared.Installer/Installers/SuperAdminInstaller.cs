using CIDRS.Identity.DefaultUserHandler.Options;
using CIDRS.Shared.Installer.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CIDRS.Shared.Installer.Installers
{
    /// <summary>
    /// The class that contains Super Admin Installer
    /// </summary>
    public class SuperAdminInstaller : IInstaller
    {
        public int OrderByKey
        { get { return 6; } } // Order by key install according to return value 

        /// <summary>
        /// The method Install services for Super Admin
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="services"></param>
        /// <param name="startupAssembly"></param>
        public void InstallServices(IConfiguration configuration, IServiceCollection services, Assembly startupAssembly)
        {
            //Get Super Admin Details
            var superAdmin = new SuperAdmin();
            configuration.Bind(key: nameof(SuperAdmin), superAdmin);
            services.AddSingleton(superAdmin);
        }
    }
}
