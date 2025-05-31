using CIDRS.Identity.Options;
using CIDRS.Shared.Installer.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CIDRS.Shared.Installer.Installers
{
    /// <summary>
    /// The class that contains Authentication methods of Insallers
    /// </summary>
    public class AuthenticationInstaller : IInstaller, IStartupConfiguration
    {
        public int OrderByKey
        { get { return 4; } } // Order by key install according to return value 

        /// <summary>
        /// The method authentication configure 
        /// </summary>
        /// <param name="configuration">configuration</param>
        /// <param name="app">app</param>
        public void Configure(IConfiguration configuration, IApplicationBuilder app)
        {
            app.UseAuthentication();
        }

        /// <summary>
        /// The method Install services for Authentication
        /// </summary>
        /// <param name="configuration">configuration</param>
        /// <param name="services">services</param>
        /// <param name="startupAssembly">startupAssembly</param>
        public void InstallServices(IConfiguration configuration, IServiceCollection services, Assembly startupAssembly)
        {
            //jwt settings
            var jwtSettings = new JwtSettings();
            configuration.Bind(key: nameof(JwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            //TokenValidationParameters
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key: Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                RequireExpirationTime = false
            };

            services.AddSingleton(tokenValidationParameters);

            //Add Authentication
            services.AddAuthentication(configureOptions: x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = tokenValidationParameters;
            });

        }
    }
}
