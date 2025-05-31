using CIDRS.Identity.Domain.Models.Entity;
using CIDRS.Identity.Models.Request;
using CIDRS.Identity.Models.Response;
using CIDRS.Shared.Common.Api.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CIDRS.Identity.Services.User
{
    /// <summary>
	/// Contract of Identity Service
	/// </summary>
    public interface IIdentityService
    {
        /// <summary>
        /// The method register user async
        /// </summary>
        /// <param name="request">request</param>
        /// <returns></returns>
        Task<AuthenticationResult> RegisterUserAsync(UserRegistrationRequest request);
        /// <summary>
        /// The Method Register User By Admin Async
        /// </summary>
        /// <param name="request">request</param>
        /// <returns></returns>
        Task<AuthenticationResult> RegisterUserByAdminAsync(RegiterUserByAdminRequest request);
        /// <summary>
        /// The method Login Async
        /// </summary>
        /// <param name="request">request</param>
        /// <returns></returns>
        Task<AuthenticationResult> LoginAsync(UserLoginRequest request);
        /// <summary>
        /// The method Refresh async
        /// </summary>
        /// <param name="request">request</param>
        /// <returns></returns>
        Task<AuthenticationResult> RefreshAsync(RefreshTokenRequest request);
        /// <summary>
        /// The method confirm email
        /// </summary>
        /// <param name="request">request</param>
        /// <returns></returns>
        Task<AuthenticationResult> ConformEmailAsync(ConformEmailRequest request);
        /// <summary>
        /// The method forgot password async
        /// </summary>
        /// <param name="emailAddress">emailAddress</param>
        /// <returns></returns>
        Task<AuthenticationResult> forgotPasswordAsync(string emailAddress);
        /// <summary>
        /// The method Reset System Generated Password Async
        /// </summary>
        /// <param name="request">request</param>
        /// <returns></returns>
        Task<AuthenticationResult> ResetSystemGeneratedPasswordAsync(PasswordResetRequest request);
        /// <summary>
        /// Reset PassWord
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AuthenticationResult> ResetPasswordAsync(PasswordResetRequest request);
        /// <summary>
        /// The method change password async
        /// </summary>
        /// <param name="request">request</param>
        /// <returns></returns>
        Task<AuthenticationResult> ChangePasswordAsync(ChangePasswordRequest request);
        /// <summary>
        /// The method resent email async
        /// </summary>
        /// <returns></returns>
        Task<ResponseResult<bool>> ResendEmailConfirmationTokenAsync();
        /// <summary>
        /// The method resent Mobile async
        /// </summary>
        /// <returns></returns>
        Task<ResponseResult<bool>> ResendMobileConfirmationTokenAsync();
        /// <summary>
        /// The method Reset Password By Admin Async
        /// </summary> 
        /// <param name="request">request</param>
        /// <returns></returns>
        Task<AuthenticationResult> ResetPasswordByAdminAsync(ResetPasswordRequest request);

        /// <summary>
        /// The method set avatar async
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="avatar"></param>
        /// <returns></returns>
        Task<ApplicationUser> SetAvatarAsync(string userId, IFormFile avatar);

        Task<bool> IsValidEmail(string email);
        Task<bool> IsValidPhoneNumber(string phoneNumber);

        Task<ApplicationUser> GetCurrentUserAsync();

        Task<AuthenticationResult> ConformMobileAsync(string token);

        Task<ApplicationUser> GetUserAsync(string userId);
    }
}