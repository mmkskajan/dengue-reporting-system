using CIDRS.Core.Helpers.DependencyInjection;
using CIDRS.Core.Modules.Applications.Services;
using CIDRS.Core.Modules.ChiefOccupants.Services;
using CIDRS.Core.Modules.Masters.Services;
using CIDRS.Core.Modules.Penalties.Services;
using CIDRS.Core.Modules.Polices.Services;
using CIDRS.Core.Modules.PublicHealthInspectors.Services;
using CIDRS.Core.Modules.Statistics.Services;
using CIDRS.Core.Modules.WorkItems.Services;
using CIDRS.Identity.Domain.Models.Entity;
using CIDRS.Identity.Infrastructure;
using CIDRS.Identity.Services.User;
using CIDRS.Shared.Common.Api;
using CIDRS.Shared.Installer.Interface;
using CIDRS.Shared.Utility.FileManipulator;
using CIDRS.Shared.Utility.FileManipulator.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CIDRS.Shared.Installer.Installers
{
    public class DependencyInstaller : IInstaller
    {
        public int OrderByKey
        { get { return 2; } }

        /// <summary>
        /// Install services for Dependency resolvation
        /// </summary>
        /// <param name="configuration"> configuration</param>
        /// <param name="services"> services</param>
        public void InstallServices(IConfiguration configuration, IServiceCollection services, Assembly startupAssembly)
        {
            services.AddCore();
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

            }).AddDefaultTokenProviders()
            .AddEntityFrameworkStores<IdentityDataContext>();

            services.Configure<DataProtectionTokenProviderOptions>(o =>
                o.TokenLifespan = TimeSpan.FromHours(24)
            );

            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IFileWriter, FileWriter>();
            services.AddTransient<IApiService, ApiService>();            
            
            services.AddTransient<IMasterDataService, MasterDataService>();
            services.AddTransient<IPublicHealthInspectorService, PublicHealthInspectorService>();
            services.AddTransient<IChiefOccupantService, ChiefOccupantService>();
            services.AddTransient<IWorkItemService, WorkItemService>();
            services.AddTransient<IPoliceService, PoliceService>();
            services.AddTransient<IReportingApplicationService, ReportingApplicationService>();
            services.AddTransient<IPenaltyService, PenaltyService>();
            services.AddTransient<IStatisticService, StatisticService>();


            


        }
    }
}
