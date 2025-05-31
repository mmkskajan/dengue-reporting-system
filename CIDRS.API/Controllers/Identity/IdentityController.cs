using CIDRS.API.Contracts.V1;
using CIDRS.API.Controllers.Base;
using CIDRS.Core.Common.Models;
using CIDRS.Core.Modules.Identity.Commands;
using CIDRS.Core.Modules.Identity.Queries;
using CIDRS.Identity.Models.Response;
using CIDRS.Shared.Common.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIDRS.API.Controllers.Identity
{
    /// <summary>
    /// Identity API Endpoints
    /// </summary>
    public class IdentityController : ApiController
    {
        /// <summary>
        /// LogIn Endpoint
        /// </summary>
        /// <returns></returns>
        [HttpPost(ApiRoutes.IdentityUser.LogIn)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult> LogInAsync(LogInRequestCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponce
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }
            var authResponce = await Mediator.Send(command);

            if (!authResponce.Status)
            {
                return BadRequest(new AuthFailedResponce
                {
                    Errors = authResponce.Errors
                });
            }

            return Ok(new AuthSuccessResponce
            {
                Token = authResponce.Token,
                RefreshToken = authResponce.RefreshToken,
                User = authResponce.User
            });
        }

        /// <summary>
        /// Validate Email Endpoint
        /// </summary>
        /// <returns></returns>
        [HttpPost(ApiRoutes.IdentityUser.EmailValidation)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<bool> ValidateEmailAsync(string email)
        {
            var validationResponse = await Mediator.Send(new ValidateEmailCommand() { Email = email});
            return validationResponse;
        }

        /// <summary>
        /// Reset Password Endpoint
        /// </summary>
        /// <returns></returns>
        [HttpPost(ApiRoutes.IdentityUser.ResetPassword)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ResponseResult<bool>> ResetPasswordAsync(ResetPasswordRequestCommand command)
        {
            var validationResponse = await Mediator.Send(command);
            return validationResponse;
        }

        /// <summary>
        /// Validate Phone Number Endpoint
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        [HttpPost(ApiRoutes.IdentityUser.ValidatePhoneNumber)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<bool> ValidatePhoneNumberAsync(string phoneNumber)
        {
            var validationResponse = await Mediator.Send(new ValidatePhoneNumberCommand() {  PhoneNumber= phoneNumber});
            return validationResponse;
        }

        /// <summary>
        /// Refresh Token Endpoint
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost(ApiRoutes.IdentityUser.Refresh)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult> RefreshAsync(RefreshCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponce
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }
            var authResponce = await Mediator.Send(command);

            if (!authResponce.Status)
            {
                return BadRequest(new AuthFailedResponce
                {
                    Errors = authResponce.Errors
                });
            }

            return Ok(new AuthSuccessResponce
            {
                Token = authResponce.Token,
                RefreshToken = authResponce.RefreshToken,
                User = authResponce.User
            });
        }


        
        /// <summary>
        /// Change Password Endpoint
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost(ApiRoutes.IdentityUser.ChangePassword)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ResponseResult<bool>> ChangePasswordAsync(ChangePasswordRequestCommand command)
        {
            var validationResponse = await Mediator.Send(command);
            return validationResponse;
        }

        /// <summary>
        /// Set Avatar Endpoint
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="avatar"></param>
        /// <returns></returns>
        [HttpPost(ApiRoutes.IdentityUser.SetAvatar)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<UserVM> SetAvatarAsync(string userId, IFormFile avatar)
        {
            var userResponse = await Mediator.Send(new SetAvatarCommand() { UserId = userId, avatar = avatar });
            return userResponse;
        }

        /// <summary>
        ///  Endpoint for Forgot Password and request reset password token on email
        /// </summary>
        /// <returns></returns>
        [HttpPost(ApiRoutes.IdentityUser.ForgotPassword)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ResponseResult<bool>>ForgotPasswordAsync(string email)
        {
            return await Mediator.Send(new ForgetPasswordCommand() {Email= email });
        }

        /// <summary>
        ///  Conform Email Endpoint
        /// </summary>
        /// <returns></returns>
        [HttpPost(ApiRoutes.IdentityUser.ConformEmail)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ResponseResult<bool>> ConformEmailAsync(ConformEmailCommand command)
        {
            return await Mediator.Send(command);
        }

        /// <summary>
        ///  Current User Endpoint
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.IdentityUser.CurrentUser)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<UserVM> GetCurrentUserAsync()
        {
            return await Mediator.Send(new GetCurrentUserQuery());
        }

        /// <summary>
        ///  Conform Mobile Endpoint
        /// </summary>
        /// <returns></returns>
        [HttpPost(ApiRoutes.IdentityUser.ConformMobile)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ResponseResult<bool>> ConformMobileAsync(ConfirmMobileCommand command)
        {
            return await Mediator.Send(command);
        }

        /// <summary>
        ///  Resend Conform Mobile Token Endpoint
        /// </summary>
        /// <returns></returns>
        [HttpPost(ApiRoutes.IdentityUser.ResendMobileConformToken)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ResponseResult<bool>> ResendTokenConformMobileAsync()
        {
            return await Mediator.Send(new ReSendConfirmationTokenCommand() { confirmationMedia = ConfirmationMedia.Mobile});
        }

        /// <summary>
        ///  Resend Conform Email Token Endpoint
        /// </summary>
        /// <returns></returns>
        [HttpPost(ApiRoutes.IdentityUser.ResendEmailConformToken)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ResponseResult<bool>> ResendTokenConformEmailAsync()
        {
            return await Mediator.Send(new ReSendConfirmationTokenCommand() { confirmationMedia = ConfirmationMedia.Email });
        }

    }
}
