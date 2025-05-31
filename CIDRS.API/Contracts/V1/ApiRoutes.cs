using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIDRS.API.Contracts.V1
{
    /// <summary>
    /// The class that defines ApiRoutes of each endpoint
    /// </summary>
    public class ApiRoutes
    {
        /// <summary>
        /// root
        /// </summary>
        public const string Root = "api";
        /// <summary>
        /// version
        /// </summary>
        public const string Version = "v1";
        /// <summary>
        /// base
        /// </summary>
        public const string Base = Root + "/" + Version + "/[controller]";

        #region Identity
        /// <summary>
        /// The Identity class that defines the Identity endpoint routes
        /// </summary>
        public static class IdentityUser
        {

            /// <summary>
            /// Login a  User
            /// </summary>
            public const string LogIn = "logIn";
            /// <summary>
            /// Validate Email
            /// </summary>
            public const string EmailValidation = "validate-email/{email}";

            /// <summary>
            /// reset the forgot password
            /// </summary>
            public const string ResetPassword = "reset-password";

            /// <summary>
            /// validate phone number
            /// </summary>
            public const string ValidatePhoneNumber = "validate-phonenumber/{phoneNumber}";

            /// <summary>
            /// Change Current password
            /// </summary>
            public const string ChangePassword = "change-password";

            /// <summary>
            /// Set Avatar
            /// </summary>
            public const string SetAvatar = "set-avatar/{userId}";


            /// <summary>
            /// refresh the token
            /// </summary>
            public const string Refresh = "refresh";

            /// <summary>
            /// forgot password request 
            /// </summary>
            public const string ForgotPassword = "forgotPassword/{email}";

            /// <summary>
            /// Confirm Email 
            /// </summary>
            public const string ConformEmail = "conform-email";

            /// <summary>
            /// Get Current User
            /// </summary>
            public const string CurrentUser = "current-user";


            /// <summary>
            /// Confirm Mobile 
            /// </summary>
            public const string ConformMobile = "conform-mobile";

            /// <summary>
            /// Resend Confirmation Token 
            /// </summary>
            public const string ResendMobileConformToken = "request-mobile-verification-code";


            /// <summary>
            /// Resend Confirmation Token 
            /// </summary>
            public const string ResendEmailConformToken = "request-email-verification-code";








        }




        #endregion

        #region Master Data
        /// <summary>
        /// The Master class that defines the Master endpoint routes
        /// </summary>
        public static class Master
        {
            /// <summary>
            /// Get Districts
            /// </summary>
            public const string GetDistricts = "districts";

            /// <summary>
            /// Get Moh Area by District Id
            /// </summary>
            public const string GetMohAreasByDisterictId = "moh-areas/{districtId}";

            /// <summary>
            /// Get Moh Area
            /// </summary>
            public const string GetMohAreas = "moh-areas";

            /// <summary>
            /// Get Police Stations
            /// </summary>
            public const string GetPoliceStations = "/police-stations";

            /// <summary>
            /// Get Police Stations
            /// </summary>
            public const string GetPoliceStationsByMohAreaId = "/police-stations/{mohAreaId}";

        }
        #endregion

        #region Admin

        /// <summary>
        /// The Admin class that defines the Master endpoint routes
        /// </summary>
        public static class Admin
        {
            /// <summary>
            /// Create Admin
            /// </summary>
            public const string CreateAdmin = Base + "/admin";

            /// <summary>
            /// Get Admins
            /// </summary>
            public const string GetAdmins = Base + "/admins";

            /// <summary>
            /// Get Admin
            /// </summary>
            public const string GetAdmin = Base + "/admin/{identifier}";

            /// <summary>
            /// Get Admin
            /// </summary>
            public const string GetAdminByUserId = Base + "/adminbyuserid/{userId}";

            /// <summary>
            /// Archive Admin
            /// </summary>
            public const string ArchiveAdmin = Base + "/admin/{identifier}";

            /// <summary>
            /// Restore Admin
            /// </summary>
            public const string RestoreAdmin = Base + "/restoreadmin/{identifier}";

            /// <summary>
            /// Update Admin
            /// </summary>
            public const string UpdateAdmin = Base + "/admin/{identifier}";

            /// <summary>
            /// Upload Avatar
            /// </summary>
            public const string UploadAdminAvatar = Base + "/adminavatar/{identifier}";

        }


        #endregion

        #region Public Health Inspectors
        /// <summary>
        /// Public Helth Inspeectors Api Routes
        /// </summary>
        public static class PublicHealthInspector
        {
            /// <summary>
            /// Register PHI
            /// </summary>
            public const string RegisterPhiByAdmin = "register-phi";

            /// <summary>
            /// Index PHI
            /// </summary>
            public const string IndexPhis = "";

            /// <summary>
            /// View PHI
            /// </summary>
            public const string ViewPhi = "{id}";

            /// <summary>
            /// Lookup PHI
            /// </summary>
            public const string Lookup = "lookup";
        }
        #endregion

        #region ChiefOccupant
        /// <summary>
        /// ChiefOccupants Api Routes
        /// </summary>
        public static class ChiefOccupant
        {
            /// <summary>
            /// Register ChiefOccupant
            /// </summary>
            public const string RegisterChiefOccupant = "register-co";

            /// <summary>
            /// Index PHI
            /// </summary>
            public const string IndexChiefOccupants = "";

            /// <summary>
            /// View ChiefOccupant
            /// </summary>
            public const string ViewChiefOccupant = "{id}";
        }
        #endregion

        #region WorkItems
        /// <summary>
        /// WorkItems Api Routes
        /// </summary>
        public static class WorkItem
        {
            /// <summary>
            /// Index WorkItems
            /// </summary>
            public const string IndexWorkItems = "";

            /// <summary>
            /// Get WorkItem
            /// </summary>
            public const string GetAWorkItem = "{id}";

            /// <summary>
            /// Re-Assign WorkItem
            /// </summary>
            public const string ReAssign = "re-assign";
            
            /// <summary>
            /// Add WorkItem Remark
            /// </summary>
            public const string Remark = "remark";
            
            /// <summary>
            /// Approve WorkItem 
            /// </summary>
            public const string Approve = "approve/{id}";

            /// <summary>
            /// Reject WorkItem 
            /// </summary>
            public const string Reject = "reject/{id}";
        }

        #endregion

        #region Police
        /// <summary>
        /// Police Api Routes
        /// </summary>
        public static class Police
        {
            /// <summary>
            /// Index Polices
            /// </summary>
            public const string IndexPolices = "";

            /// <summary>
            /// Get Police
            /// </summary>
            public const string GetAPolice = "{id}";

            
            /// <summary>
            /// Police lookup
            /// </summary>
            public const string Lookup = "lookup";

            /// <summary>
            /// Create Police
            /// </summary>
            public const string Create = "new";
        }

        #endregion

        #region Application
        /// <summary>
        /// Reporting Application Api Routes
        /// </summary>
        public static class ReportingApplication
        {
            /// <summary>
            /// Start Application
            /// </summary>
            public const string StartApplication = "start";

            /// <summary>
            /// Get Application
            /// </summary>
            public const string GetApplication = "{id}";

            /// <summary>
            /// Add Base Surrrounding Set
            /// </summary>
            public const string AddBaseSurroundingSet = "base-surrounding-set";

            /// <summary>
            /// Add Base Surrrounding Set
            /// </summary>
            public const string AddPublicSurroundingSet = "public-surrounding-set";

            /// <summary>
            /// Add Surrrounding Set
            /// </summary>
            public const string AddSurroundingSet = "surrounding-set";

            /// <summary>
            /// Add Surrrounding Set
            /// </summary>
            public const string GetBaseSurroundingSet = "base-surrounding-set";

            /// <summary>
            /// Complete Application
            /// </summary>
            public const string CompleteApplication = "complete";


        }



        #endregion

        #region Penalty
        /// <summary>
        /// Penalties Api Routes
        /// </summary>
        public static class Penalty
        {
            /// <summary>
            /// Resolve Penalty
            /// </summary>
            public const string Resolve = "resolve/{id}";
            
        }

        #endregion

        #region Statistics
        /// <summary>
        /// Statistics Api Routes
        /// </summary>
        public static class Statistics
        {
            /// <summary>
            /// Statistics
            /// </summary>
            public const string GetStatistics = "";

            /// <summary>
            /// Statistics Details
            /// </summary>
            public const string GetApplicationStatisticsDetails = "application-details";
            
            /// <summary>
            /// Statistics Details
            /// </summary>
            public const string GetEnvironmentStatisticsDetails = "environment-details";
            
            /// <summary>
            /// Statistics Details
            /// </summary>
            public const string GetPenalizationStatisticsDetails = "penalization-details";



        }

        #endregion

    }
}
