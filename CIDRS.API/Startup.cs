using CIDRS.Core.Helpers.DependencyInjection;
using CIDRS.Shared.Installer.Extensions;
using CIDRS.Shared.Installer.Interface;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CIDRS.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            this.ConfigureDatabaseInfrastructure(services);
            services.InstallServicesInAssembly(Configuration, typeof(IInstaller).Assembly, typeof(Startup).Assembly);

            Console.WriteLine("Services registerd");
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add Infrastructure services to the container.
        /// </summary>
        /// <param name="services"></param>
        public virtual void ConfigureDatabaseInfrastructure(IServiceCollection services)
        {
            services.ConfigureDatabaseServices(Configuration);

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();

                }
            //Handle Exception Globally


            app.ConfigurationInAssembly(Configuration, typeof(IInstaller).Assembly);
        }
    }
}
