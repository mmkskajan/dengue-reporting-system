using CIDRS.Shared.Installer.Interface;
using CIDRS.Shared.Utility.SmsManipulator.Options;
using CIDRS.Shared.Utility.SmsManipulator.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CIDRS.Shared.Installer.Installers
{
    /// <summary>
    /// The class that contains Email Installer
    /// </summary>
    public class SmsInstaller : IInstaller
    {
        public int OrderByKey
        { get { return 8; } } // Order by key install according to return value 

        /// <summary>
        /// The method Install services for Email
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="services"></param>
        /// <param name="startupAssembly"></param>
        public void InstallServices(IConfiguration configuration, IServiceCollection services, Assembly startupAssembly)
        {
            //Get Email Configuration details
            var smsConfig = configuration
                .GetSection("SmsConfiguration")
                .Get<SmsConfiguration>();
            services.AddSingleton(smsConfig);

            services.AddTransient<ISmsManipulatorService, SmsManipulatorService>();
        }
    }
}
