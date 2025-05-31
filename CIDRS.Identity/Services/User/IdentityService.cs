using CIDRS.Identity.Domain.Enums;
using CIDRS.Identity.Domain.Models.Entity;
using CIDRS.Identity.Enums;
using CIDRS.Identity.Extensions;
using CIDRS.Identity.Helpers;
using CIDRS.Identity.Infrastructure;
using CIDRS.Identity.Models.Request;
using CIDRS.Identity.Models.Response;
using CIDRS.Identity.Options;
using CIDRS.Shared.Common.Api.Models;
using CIDRS.Shared.Middleware.ExceptionHandler.Exceptions;
using CIDRS.Shared.Utility.EmailManipulator.Extensions;
using CIDRS.Shared.Utility.EmailManipulator.Models;
using CIDRS.Shared.Utility.EmailManipulator.Services;
using CIDRS.Shared.Utility.FileManipulator.Interfaces;
using CIDRS.Shared.Utility.SmsManipulator.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CIDRS.Identity.Services.User
{
    /// <summary>
    /// The class that contain service method Identity
    /// </summary>
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IdentityDataContext _dataContext;
        private readonly IFileWriter _fileWriter;
        private readonly IEmailSenderService _emailSender;
        private readonly HttpContext _httpContext;
        private readonly ISmsManipulatorService _smsSender;
        private const string parentFolderName = "Images";
        private const string folderName = "Avatars";
        /// <summary>
        /// The Constructor Identity Service 
        /// </summary>
        /// <param name="userManager">userManager</param>
        /// <param name="jwtSettings">jwtSettings</param>
        /// <param name="tokenValidationParameters">tokenValidationParameters</param>
        /// <param name="dataContext">dataContext</param>
        /// <param name="emailSender">emailSender</param>
        public IdentityService(UserManager<ApplicationUser> userManager, JwtSettings jwtSettings, TokenValidationParameters tokenValidationParameters, IdentityDataContext dataContext, IEmailSenderService emailSender, IFileWriter fileWriter,IHttpContextAccessor httpContextAccessor, ISmsManipulatorService smsSender)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _tokenValidationParameters = tokenValidationParameters;
            _emailSender = emailSender;
            _dataContext = dataContext;
            _fileWriter = fileWriter;
            _httpContext = httpContextAccessor.HttpContext;
            _smsSender = smsSender;
        }

        /// <summary>
        ///  Method to register the user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AuthenticationResult> RegisterUserAsync(UserRegistrationRequest request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if(existingUser == null)
            existingUser = await _dataContext.Users.Where(x => x.PhoneNumber == request.PhoenNumber).SingleOrDefaultAsync();
            // Existing User
            if (existingUser != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User with the Email Address or Phone Number Already Exits" }
                };
            }
           
            var newUser = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email,
                UserType = request.UserType,
                FullName = request.FullName,
                PhoneNumber = request.PhoenNumber,
                hasTempPassword = false,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
            };
            newUser.SetCreatedTime();
            newUser.SetAvatar();

            var createdUser = _userManager.CreateAsync(newUser, request.Password);
            // Creare user
            if (!createdUser.Result.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = createdUser.Result.Errors.Select(x => x.Description)
                };
            }
            await SendEmailConformationAsync(newUser, AuthenticationOption.EmailConformation);
            await SendPhoneConformationAsync(newUser);
            var authResult = await GenerateAuthenticationResultForUserAsync(newUser);

            return new RegisterResult()
            {
                UserId = newUser.Id,
                Errors = authResult?.Errors,
                RefreshToken = authResult.RefreshToken,
                Status = authResult.Status,
                Token = authResult.Token,
                User = authResult.User
            };

        }

        /// <summary>
        /// Method to register the user by admin
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AuthenticationResult> RegisterUserByAdminAsync(RegiterUserByAdminRequest request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);

            if(existingUser == null)
                existingUser =await _dataContext.Users.SingleOrDefaultAsync(x => x.PhoneNumber == request.PhoenNumber);
            // Existing User
            if (existingUser != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User with the Email Address or Phone Number Already Exits" }
                };
            }
           
            var newUser = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email,
                UserType = request.UserType,
                FullName = request.FullName,
                PhoneNumber = request.PhoenNumber,
                hasTempPassword = true,
                EmailConfirmed =true,
                PhoneNumberConfirmed = true
            };
            newUser.SetCreatedTime();
            newUser.SetAvatar();
            var tempPassWord = PasswordHelper.GenerateRandomPassword();
            var createdUser = _userManager.CreateAsync(newUser, tempPassWord);
            // create User
            if (!createdUser.Result.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = createdUser.Result.Errors.Select(x => x.Description)
                };
            }
            await SendEmailConformationAsync(newUser, AuthenticationOption.RegisterUserByAdmin,tempPassWord);


            var authResult = await GenerateAuthenticationResultForUserAsync(newUser);

            return new RegisterResult()
            {
                UserId = newUser.Id,
                Errors = authResult?.Errors,
                RefreshToken = authResult.RefreshToken,
                Status = authResult.Status,
                Token = authResult.Token,
                User = authResult.User                
            };
        }

        /// <summary>
        /// Method to Conform the Email after Register
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AuthenticationResult> ConformEmailAsync(ConformEmailRequest request)
        {
            var useId = _httpContext.GetUserId();

            var user = await _userManager.FindByIdAsync(useId);

            if (user == null)

                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist" }
                };

            var result = await _userManager.ConfirmEmailAsync(user, request.Token);
            if (!result.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Invalid Token" }
                };
            }


            return await GenerateAuthenticationResultForUserAsync(user);
        }
        /// <summary>
        /// Method to Conform the Mobile after Register
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<AuthenticationResult> ConformMobileAsync(string token)
        {
            var useId = _httpContext.GetUserId();

            var user = await _userManager.FindByIdAsync(useId);

            if (user == null)

                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist" }
                };

            var result = await _userManager.VerifyChangePhoneNumberTokenAsync(user, token,user.PhoneNumber);
            if (!result)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Invalid Token" }
                };
            }
            user.PhoneNumberConfirmed = true;
            await _userManager.UpdateAsync(user);
            return await GenerateAuthenticationResultForUserAsync(user);
        }

        /// <summary>
        ///  Method to login the user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AuthenticationResult> LoginAsync(UserLoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist" }
                };
            }

            //if (!user.EmailConfirmed)
            //{
            //    return new AuthenticationResult
            //    {
            //        Errors = new[] { "Email is not Conformed Yet" }
            //    };
            //}
            var userHasValidPassword = _userManager.CheckPasswordAsync(user, request.Password);

            if (!userHasValidPassword.Result)
                return new AuthenticationResult
                {
                    Errors = new[] { "Email/PassWord combination is wrong!" }
                };

            return await GenerateAuthenticationResultForUserAsync(user);
        }
        /// <summary>
        /// Method to request new password if the password forgot
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public async Task<AuthenticationResult> forgotPasswordAsync(String Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);

            if (user == null)

                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist" }
                };


            await SendEmailConformationAsync(user, AuthenticationOption.forgotPassword);

            return await GenerateAuthenticationResultForUserAsync(user);
        }

        /// <summary>
        /// Method to resend Conformation conformation Email
        /// </summary>
        /// <param name="EmailAddress"></param>
        /// <returns></returns>
        public async Task<ResponseResult<bool>> ResendEmailConfirmationTokenAsync()
        {
            var useId = _httpContext.GetUserId();
            var user = await _userManager.FindByIdAsync(useId);

            if (user == null)

                return new ResponseResult<bool>
                {
                    Errors = new[] { "User does not exist" }
                };

            if (user.EmailConfirmed)
            {
                return new ResponseResult<bool>
                {
                    Errors = new[] { "User Email Address is Already Conformed" }
                };
            }
            await SendEmailConformationAsync(user, AuthenticationOption.EmailConformation);

            return new ResponseResult<bool>()
            {
                Errors = null,
                Result = true,
                Succeeded = true
            }; 
        }

        /// <summary>
        /// Method to resend Conformation conformation Email
        /// </summary>
        /// <param name="EmailAddress"></param>
        /// <returns></returns>
        public async Task<ResponseResult<bool>> ResendMobileConfirmationTokenAsync()
        {
            var useId = _httpContext.GetUserId();
            var user = await _userManager.FindByIdAsync(useId);

            if (user == null)

                return new ResponseResult<bool>
                {
                    Errors = new[] { "User does not exist" }
                };

            if (user.PhoneNumberConfirmed)
            {
                return new ResponseResult<bool>
                {
                    Errors = new[] { "User Mobile Number is Already Conformed" }
                };
            }
            await SendPhoneConformationAsync(user);

            return new ResponseResult<bool>()
            {
                Errors = null,
                Result = true,
                Succeeded = true
            };
        }
        /// <summary>
        /// Method to Reset to new password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AuthenticationResult> ResetSystemGeneratedPasswordAsync(PasswordResetRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)

                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist" }
                };

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.newPassword);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (!result.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = result.Errors.Select(x => x.Description)
                };
            }
            if (!user.EmailConfirmed)
            {
                await _userManager.ConfirmEmailAsync(user, token);

            }

            return await GenerateAuthenticationResultForUserAsync(user);
        }
        /// <summary>
        ///  Method to Reset to new password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AuthenticationResult> ResetPasswordAsync(PasswordResetRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)

                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist" }
                };

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.newPassword);

            if (!result.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = result.Errors.Select(x => x.Description)
                };
            }

            return await GenerateAuthenticationResultForUserAsync(user);
        }
        /// <summary>
        /// Reset Password By Admin
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AuthenticationResult> ResetPasswordByAdminAsync(ResetPasswordRequest request)
        {

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)

                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist" }
                };
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, request.Password);
            if (!result.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = result.Errors.Select(x => x.Description)
                };
            }
            if (!user.EmailConfirmed)
            {
                await _userManager.ConfirmEmailAsync(user, token);

            }

            return await GenerateAuthenticationResultForUserAsync(user);
        }

        /// <summary>
        /// The Method Chang ePassword Async
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AuthenticationResult> ChangePasswordAsync(ChangePasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)

                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist" }
                };
            var result = await _userManager.ChangePasswordAsync(user, request.currentPassword, request.newPassword);
            if (!result.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Old password is mismatch" }
                };
            }
            user.hasTempPassword = false;
            _dataContext.Users.Update(user);
            await _dataContext.SaveChangesAsync();

            return await GenerateAuthenticationResultForUserAsync(user);
        }

        /// <summary>
        /// The Method Refresh Async
        /// </summary>
        /// <param name="request">request</param>
        /// <returns></returns>
        public async Task<AuthenticationResult> RefreshAsync(RefreshTokenRequest request)
        {
            var validatedToken = GetClaimsPrincipalFromToken(request.Token);

            if (validatedToken == null)
                return new AuthenticationResult
                {
                    Errors = new[] { "Invalid Token!" }
                };

            var expiryDateUnix = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expiryDateTimeUtc = new DateTime(year: 1970, month: 1, day: 1, hour: 0, minute: 0, second: 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);

            if (expiryDateTimeUtc > DateTime.UtcNow)
                return new AuthenticationResult
                {
                    Errors = new[] { "This Token hasn't expired yet!" }
                };

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            var storedRefreshToken = await _dataContext.RefreshTokens.SingleOrDefaultAsync(x => x.Token == request.RefreshToken);

            if (storedRefreshToken == null)
                return new AuthenticationResult
                {
                    Errors = new[] { "The Refresh Token does not exist" }
                };

            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
                return new AuthenticationResult
                {
                    Errors = new[] { "The Refresh Token has expired" }
                };

            if (storedRefreshToken.Invalidated)
                return new AuthenticationResult
                {
                    Errors = new[] { "The Refresh Token has expired" }
                };

            if (storedRefreshToken.Used)
                return new AuthenticationResult
                {
                    Errors = new[] { "The Refresh Token Already Used" }
                };
            if (storedRefreshToken.JwtId != jti)
                return new AuthenticationResult
                {
                    Errors = new[] { "The Refresh Token does not match with JWT" }
                };

            storedRefreshToken.Used = true;
            _dataContext.RefreshTokens.Update(storedRefreshToken);
            await _dataContext.SaveChangesAsync();

            var user = _userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == "id").Value);
            return await GenerateAuthenticationResultForUserAsync(user.Result);
        }

        /// <summary>
        /// Method to set Avatar
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="avatar">avatar</param>
        /// <returns></returns>
        public async Task<ApplicationUser> SetAvatarAsync(string userId, IFormFile avatar)
        {
            var user = await _userManager.FindByIdAsync(userId);           

            if (user == null)
                throw new NotFoundException($"The User Not Found with requested userId");

            if (user.ArchivedAt != null)
                throw new BusinessLogicException($"The User is Archived, Could not update avatar for archived User");

            if (avatar == null)
                throw new NotFoundException($"The avatar not submitted!!");

            var result = await _fileWriter.UploadImageAsync(userId, folderName, avatar);

            if (result != GetExpectedAvatarName(userId, avatar))
                throw new BusinessLogicException($"The avatar image format that requested is not accepted! ");

            string path = Path.Combine(parentFolderName, folderName);
            path = Path.Combine(path, result);
            user.SetAvatar(path);
            _dataContext.Users.Update(user);
            await _dataContext.SaveChangesAsync();

            return user;
        }

        public async Task<bool> IsValidEmail(string email)
        {
            var isValidEmailFormat = Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$",RegexOptions.IgnoreCase);
            if (isValidEmailFormat)
            {
                var user = await _userManager.FindByEmailAsync(email);
                return user == null;
            }
            else
                return false;

        }

        public async Task<bool> IsValidPhoneNumber(string phoneNumber)
        {
            var isValidPnoneNumberFormat = Regex.IsMatch(phoneNumber, @"94\d{9}$", RegexOptions.IgnoreCase);
            if (isValidPnoneNumberFormat)
            {
                var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
                return user == null;
            }
            else
                return false;

        }

        public async Task<ApplicationUser> GetCurrentUserAsync()
        {
            var userId = _httpContext.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            return user;
        }

        public async Task<ApplicationUser> GetUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user;
        }

        #region Private Methods
        /// <summary>
        /// The Method Generate Authentication Result For User Async
        /// </summary>
        /// <param name="user">user</param>
        /// <returns></returns>
        private async Task<AuthenticationResult> GenerateAuthenticationResultForUserAsync(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var claims = new List<Claim>
            {
                new Claim(type: JwtRegisteredClaimNames.Sub,value: user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(CustomClaimTypes.Email, user.Email),
                new Claim(CustomClaimTypes.Username, user.UserName),
                new Claim(CustomClaimTypes.Id, user.Id),
                new Claim(CustomClaimTypes.UserType,((int) user.UserType).ToString()),
                new Claim(CustomClaimTypes.Avatar, user.Avatar)
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);
             
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),

                Expires = DateTime.Now.Add(_jwtSettings.TokenLifeTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), algorithm: SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6)
            };

            await _dataContext.AddAsync(refreshToken);
            await _dataContext.SaveChangesAsync();

            return new AuthenticationResult
            {
                Status = true,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token,
                User = user?.ToViewModel()
            };
        }
        /// <summary>
        /// The Method Get Claims Principal From Token
        /// </summary>
        /// <param name="token">token</param>
        /// <returns></returns>
        private ClaimsPrincipal GetClaimsPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                    return null;

                return principal;
            }
            catch
            {

                return null;
            }
        }
        /// <summary>
        /// The Method Jwt With Valid Security Algorithm
        /// </summary>
        /// <param name="validatedToken">validatedToken</param>
        /// <returns></returns>
        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                jwtSecurityToken.Header.Alg.Equals(value: SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        ///  Method to Send Email To Verify password or Reset Password link
        /// </summary>
        /// <param name="user">user</param>
        /// <param name="option">option</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        private async Task SendEmailConformationAsync(ApplicationUser user, AuthenticationOption option, string password = "")
        {
            try
            {

                string subject = string.Empty;
                string token = string.Empty;
                string emailSubject = string.Empty;
                string fileName = string.Empty;

                switch (option)
                {
                    case AuthenticationOption.login:
                        break;
                    case AuthenticationOption.forgotPassword:
                        subject = "Reset Password Conformation";
                        token = await _userManager.GeneratePasswordResetTokenAsync(user);
                        emailSubject = "CIDRS – Reset Password Code";
                        fileName = "ForgetPassWord.html";
                        break;
                    case AuthenticationOption.EmailConformation:
                        subject = "Conform your Email to Verify";
                        token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        emailSubject = "CIDRS – Email Verification Code";
                        fileName = "EmailVerification.html";
                        break;
                    case AuthenticationOption.RegisterUserByAdmin:
                        emailSubject = "CIDRS – Welcome New User!";
                        fileName = "PhiRegisration.html";
                        break;
                    default:
                        break;
                }
                var templatePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Templates\\Email", fileName);
                var content = await templatePath.Render(new string[] { user.FullName, user.Email, user.PhoneNumber, password, token });
                var html = new NotificationMessage(new string[] { user.Email }, emailSubject, content);
                await _emailSender.SendEmailAsync(html);
                await _userManager.ConfirmEmailAsync(user, token); // Added for auto confirmation
            }
            catch 
            {

            }

        }

        private async Task SendPhoneConformationAsync(ApplicationUser user)
        {
            try
            {

                var token = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
                string userName = user.FullName;
                string fileName = "PhoneConfirmation.txt";

                var templatePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Templates\\SMS", fileName);
                var content = await templatePath.Render(new string[] { userName, token });

                await _smsSender.SendSmsAsync(user.PhoneNumber, content);
                await _userManager.VerifyChangePhoneNumberTokenAsync(user, token, user.PhoneNumber);
                
                user.PhoneNumberConfirmed = true;
                await _userManager.UpdateAsync(user);
            }
            catch 
            {

            }
        }

        /// <summary>
        /// Get Expected Avatar Name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        private string GetExpectedAvatarName(string id, IFormFile file)
        {
            //Geting extension 
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];

            return id + extension;// Combained image file Name
        }

        #endregion



    }
}
