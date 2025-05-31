using CIDRS.Identity.Infrastructure;
using CIDRS.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Shared.Installer.Extensions
{
    /// <summary>
    /// The class contains extension methods of IServiceCollection
    /// </summary>
    public static class ConfigureDatabaseExtension
    {
        /// <summary>
        /// The Method Configure Database Services
        /// </summary>
        /// <param name="services">services</param>
        /// <param name="configuration">configuration</param>
        /// <param name="useInMemory">useInMemory</param>
        /// <returns></returns>
        public static IServiceCollection ConfigureDatabaseServices(this IServiceCollection services, IConfiguration configuration, bool useInMemory = false)
        {
            // In memory database used for test
            if (useInMemory)
            {
                // Add Db context
                services.AddDbContext<IdentityDataContext>(options =>
                options.UseInMemoryDatabase("InmemoryIdentityDB"));
                //virtual Memory to store the test data
                services.AddDbContext<ApplicationDataContext>(options =>
                options.UseInMemoryDatabase("InmemoryDB"));

            }
            else
            {
                // Get Connection string of identify DB
                string IdentityConnection = configuration.GetConnectionString("IdentityDbConnection");
                services.AddDbContext<IdentityDataContext>(options =>
                {
                    options.UseSqlServer(IdentityConnection); // Use Sql Server Database
                    options.EnableSensitiveDataLogging(true); // Log Sensitive Data
                });

                // Get Connection string of identify DB
                string DefaultConnection = configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<ApplicationDataContext>(options =>
                {
                    options.UseSqlServer(DefaultConnection); // Use Sql Server Database
                    options.EnableSensitiveDataLogging(true); // Log Sensitive Data
                });


            }
            return services;
        }
    }
}
