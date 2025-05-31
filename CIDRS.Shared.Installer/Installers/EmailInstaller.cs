using CIDRS.Shared.Installer.Interface;
using CIDRS.Shared.Utility.EmailManipulator.Options;
using CIDRS.Shared.Utility.EmailManipulator.Services;
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
    public class EmailInstaller : IInstaller
    {
        public int OrderByKey
        { get { return 5; } } // Order by key install according to return value 

        /// <summary>
        /// The method Install services for Email
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="services"></param>
        /// <param name="startupAssembly"></param>
        public void InstallServices(IConfiguration configuration, IServiceCollection services, Assembly startupAssembly)
        {
            //Get Email Configuration details
            var emailConfig = configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);

            services.AddTransient<IEmailSenderService, EmailSenderService>();
        }
    }
}
