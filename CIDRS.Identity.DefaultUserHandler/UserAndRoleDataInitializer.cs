using CIDRS.Domain.Models.Entity.Admins;
using CIDRS.Identity.DefaultUserHandler.Options;
using CIDRS.Identity.Domain.Enums;
using CIDRS.Identity.Domain.Models.Entity;
using CIDRS.Identity.Extensions;
using CIDRS.Infrastructure;
using CIDRS.Shared.Common.Extensions;
using CIDRS.Shared.Utility.EmailManipulator.Extensions;
using CIDRS.Shared.Utility.EmailManipulator.Models;
using CIDRS.Shared.Utility.EmailManipulator.Services;
using CIDRS.Shared.Utility.SmsManipulator.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDRS.Identity.DefaultUserHandler
{
    /// <summary>
    /// The class User and role data initializer
    /// </summary>
    public static class UserAndRoleDataInitializer
    {
        /// <summary>
        /// The Method seed roles and initial user
        /// </summary>
        /// <param name="userManager">userManager</param>
        /// <param name="superAdmin">superAdmin</param>
        /// <param name="dataContext">dataContext</param>
        /// <returns></returns>
        public static async Task SeedRolesAndInitialUser(UserManager<ApplicationUser> userManager, SuperAdmin superAdmin, ApplicationDataContext dataContext, IEmailSenderService emailSender, ISmsManipulatorService smsSender)
        {
            // Valid user submitted 
            if (IsValidUserSubmitted(superAdmin))
            {
                await SeedUsersAsync(userManager, superAdmin, dataContext, emailSender, smsSender);
            }
        }

        /// <summary>
        /// The method seed users async
        /// </summary>
        /// <param name="userManager">userManager</param>
        /// <param name="superAdmin">superAdmin</param>
        /// <returns></returns>
        private static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager, SuperAdmin superAdmin, ApplicationDataContext dataContext, IEmailSenderService emailSender, ISmsManipulatorService smsSender)
        {
            string adminEmail = superAdmin.Email;
            string adminPass = superAdmin.Password;
            if (userManager.FindByEmailAsync(adminEmail).Result == null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    UserType = ApplicationUserType.Admin,
                    PhoneNumber = superAdmin.MobileNumber,
                    PhoneNumberConfirmed = true,
                    FullName = superAdmin.FullName
                };
                user.SetCreatedTime();
                user.SetAvatar();
                IdentityResult result = await userManager.CreateAsync(user, adminPass);

                if (result.Succeeded)
                {
                    Admin admin = new Admin()
                    {
                        IdentityUserId = user.Id
                    };

                    admin.EnsureId(GetAdminId(dataContext));
                    admin.SetCreatedTime();

                    await dataContext.Admins.AddAsync(admin);
                    await dataContext.SaveChangesAsync();

                    await SendNotificationsAsync(user, emailSender, smsSender);
                }
            }
        }

        /// <summary>
        /// Get AdminId To create
        /// </summary>
        /// <returns></returns>
        private static int GetAdminId(ApplicationDataContext dataContext)
        {
            // Get Last StaffId
            var result = dataContext.Admins.OrderBy(a => a.Id).LastOrDefault();

            if (result != null)
                return result.Id + 1;
            else
                return 1;

        }

        /// <summary>
        /// The method valid user submitted
        /// </summary>
        /// <param name="superAdmin">superAdmin</param>
        /// <returns></returns>
        private static bool IsValidUserSubmitted(SuperAdmin superAdmin)
        {
            return !string.IsNullOrEmpty(superAdmin.MobileNumber) &&
                   !string.IsNullOrEmpty(superAdmin.Email) &&
                   !string.IsNullOrEmpty(superAdmin.FullName) &&
                   !string.IsNullOrEmpty(superAdmin.Password);
        }

        private static async Task SendNotificationsAsync(ApplicationUser user, IEmailSenderService emailSender, ISmsManipulatorService smsSender)
        {
            await SendAdminRegistrationEmailNotificationAsync(user, emailSender);
            await SendAdminRegistrationSmsNotificationAsync(user, smsSender);
        }


        private static async Task SendAdminRegistrationEmailNotificationAsync(ApplicationUser user, IEmailSenderService emailSender)
        {
            try
            {
                string adminName = user.FullName;
                string emailSubject = "Welcome Administrator";
                string fileName = "WelcomeAdmin.html";

                var templatePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Templates\\Email", fileName);
                var content = await templatePath.Render(new string[] { adminName });
                var html = new NotificationMessage(new string[] { user.Email }, emailSubject, content);


                await emailSender.SendEmailAsync(html);
            }
            catch 
            {

            }
            
        }

        private static async Task SendAdminRegistrationSmsNotificationAsync(ApplicationUser user, ISmsManipulatorService smsSender)
        {
            try
            {
                string adminName = user.FullName;
                string fileName = "WelcomeAdmin.txt";

                var templatePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Templates\\SMS", fileName);
                var content = await templatePath.Render(new string[] { adminName });

                await smsSender.SendSmsAsync(user.PhoneNumber, content);
            }
            catch 
            {

            }
        }

    }
}
