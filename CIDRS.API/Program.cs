using CIDRS.Identity.DefaultUserHandler;
using CIDRS.Identity.DefaultUserHandler.Options;
using CIDRS.Identity.Domain.Models.Entity;
using CIDRS.Identity.Infrastructure;
using CIDRS.Infrastructure;
using CIDRS.Shared.Utility.EmailManipulator.Services;
using CIDRS.Shared.Utility.SmsManipulator.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CIDRS.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                try
                {
                    var dbContext = serviceProvider.GetRequiredService<IdentityDataContext>();
                    await dbContext.Database.MigrateAsync();
                    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    var superAdmin = serviceProvider.GetRequiredService<SuperAdmin>();
                    var appDataContext = serviceProvider.GetRequiredService<ApplicationDataContext>();
                    var emailSender = serviceProvider.GetRequiredService<IEmailSenderService>();
                    var smsSender = serviceProvider.GetRequiredService<ISmsManipulatorService>();
                    await UserAndRoleDataInitializer.SeedRolesAndInitialUser(userManager, superAdmin,appDataContext,emailSender,smsSender);

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
