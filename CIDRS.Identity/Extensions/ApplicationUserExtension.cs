using CIDRS.Identity.Domain.Models.Entity;
using CIDRS.Identity.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Identity.Extensions
{
    /// <summary>
    /// Application User Extension
    /// </summary>
    public static class ApplicationUserExtension
    {
        /// <summary>
        /// Method to Update Created Date and Time
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static void SetCreatedTime(this ApplicationUser entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;
        }

        /// <summary>
        /// Method to Update Updated Date and Time
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static void SetUpdatedTime(this ApplicationUser entity)
        {
            entity.UpdatedAt = DateTime.Now;
        }

        /// <summary>
        /// Method to Update Deleted Date and Time
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isRestore"></param>
        public static void SetArchivedTime(this ApplicationUser entity)
        {
            entity.UpdatedAt = DateTime.Now;

        }

        /// <summary>
        /// Set avatar
        /// </summary>
        /// <param name="admin"></param>
        /// <param name="avatar"></param>
        public static void SetAvatar(this ApplicationUser user, string avatar = "")
        {
            user.Avatar = avatar;
        }

        /// <summary>
        /// To View Model
        /// </summary>
        /// <param name="user"></param>
        public static ApplicationUserVM ToViewModel(this ApplicationUser user)
        {
            return new ApplicationUserVM()
            {
                ArchivedAt = user.ArchivedAt,
                Avatar = user.Avatar,
                CreatedAt = user.CreatedAt,
                Email = user.Email,
                FullName = user.FullName,
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
                UpdatedAt = user.UpdatedAt,
                UserName = user.UserName,
                UserType = user.UserType,
                hasTempPassword = user.hasTempPassword,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed
            };
        }
    }
}
