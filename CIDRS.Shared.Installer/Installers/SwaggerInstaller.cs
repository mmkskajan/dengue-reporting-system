using CIDRS.Domain.Options;
using CIDRS.Shared.Installer.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace CIDRS.Shared.Installer.Installers
{
    /// <summary>
    /// The class that contains Swagger Installer
    /// </summary>
    public class SwaggerInstaller : IInstaller, IStartupConfiguration
    {
        public int OrderByKey
        { get { return 7; } } // Order by key install according to return value 

        /// <summary>
        /// The method Configure for Swagger
        /// </summary>
        /// <param name="configuration">configuration</param>
        /// <param name="app">app</param>
        public void Configure(IConfiguration configuration, IApplicationBuilder app)
        {
            var swaggerOptions = new SwaggerOptions();
            configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger(option =>
            {
                option.RouteTemplate = swaggerOptions.JsonRoute;
            });

            app.UseSwaggerUI(option => { option.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description); });

        }

        /// <summary>
        /// The method Install services for Swagger
        /// </summary>
        /// <param name="configuration">configuration</param>
        /// <param name="services">services</param>
        /// <param name="startupAssembly">startupAssembly</param>
        public void InstallServices(IConfiguration configuration, IServiceCollection services, Assembly startupAssembly)
        {
            // Add Swagger Genaration
            services.AddSwaggerGen(x => {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "CIDRS RESTful APIs", Version = "v1" });
                //Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{startupAssembly.GetName().Name}.xml"; // Get XML file
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile); // Combine XML path
                x.IncludeXmlComments(xmlPath);

                x.AddSecurityDefinition(name: "Bearer", new OpenApiSecurityScheme
                {
                    Description = " Jwt Authorization header using the bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                // Add Security Requirement
                x.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}
