using CIDRS.Domain.Models.Entity.Admins;
using CIDRS.Domain.Models.Entity.Applications;
using CIDRS.Domain.Models.Entity.Base;
using CIDRS.Domain.Models.Entity.ChiefOccupants;
using CIDRS.Domain.Models.Entity.Polices;
using CIDRS.Domain.Models.Entity.PublicHealthInspectors;
using CIDRS.Domain.Models.Entity.WorkItems;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Shared.Common.Extensions
{
    /// <summary>
    /// The class that contains extension methods 
    /// </summary>
    public static class BaseExtension
    {
        /// <summary>
        /// Prefix of Admin identifier
        /// </summary>
        private const string AdminPrefix = "MOH";
        private const string PhiPrefix = "PHI";
        private const string CoPrefix = "CO";
        private const string WorkItemPrefix = "WI";
        private const string WorkItemActionPrefix = "WIA";
        private const string WorkItemRemarkPrefix = "WIR";
        private const string PolicePrefix = "POL";
        private const string ApplicationPrefix = "RA";


        /// <summary>
        /// Method to Update Created Date and Time
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static void SetCreatedTime(this BaseEntity entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;
        }

        /// <summary>
        /// Method to Update Updated Date and Time
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static void SetUpdatedTime(this BaseEntity entity)
        {
            entity.UpdatedAt = DateTime.Now;
        }

        /// <summary>
        /// Method to Update Deleted Date and Time
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isRestore"></param>
        public static void SetArchivedTime(this BaseEntity entity, bool isRestore = false)
        {
            entity.UpdatedAt = DateTime.Now;
            entity.ArchivedAt = isRestore ? null : (Nullable<DateTime>)DateTime.Now; // sets the Archived date and time according to restoration

        }

        /// <summary>
        /// Ensure the Identifier
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="identifier"></param>
        public static void EnsureId(this BaseEntity entity, int identifier)
        {
            if (string.IsNullOrEmpty(entity.Identifier))
                entity.Identifier = GetPrefix(entity) + string.Format("{0:D7}", identifier);// Pad the Identifier
        }

        /// <summary>
        /// Get Prefix
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private static string GetPrefix(BaseEntity entity)
        {
            if (entity is Admin)
                return AdminPrefix;
            if (entity is PublicHealthInspector)
                return PhiPrefix;
            if (entity is ChiefOccupant)
                return CoPrefix; 
            if (entity is WorkItem)
                return WorkItemPrefix;
            if (entity is WorkItemAction)
                return WorkItemActionPrefix;
            if (entity is WorkItemRemark)
                return WorkItemRemarkPrefix;
            if (entity is Police)
                return PolicePrefix;
            if (entity is ReportingApplication)
                return ApplicationPrefix;

            return string.Empty;
        }
    }
}
