using CIDRS.Core.Modules.Identity.Commands;
using CIDRS.Identity.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Core.Modules.Identity.Extensions
{
    public static class IdentityCoreExtension
    {
        public static PasswordResetRequest ToServiceRequest(this ResetPasswordRequestCommand command)
        {
            return new PasswordResetRequest()
            { 
                Email= command.Email,
                newPassword = command.newPassword,
                Token = command.Token
            };
        }


        public static RefreshTokenRequest ToServiceRequest(this RefreshCommand command)
        {
            return new RefreshTokenRequest()
            {
                RefreshToken = command.RefreshToken,
                Token = command.Token
            };
        }



        public static ChangePasswordRequest ToServiceRequest(this ChangePasswordRequestCommand command)
        {
            return new ChangePasswordRequest()
            {
                currentPassword = command.currentPassword,
                Email = command.Email,
                newPassword = command.newPassword
            };
        }

        public static ConformEmailRequest ToServiceRequest(this ConformEmailCommand command)
        {
            return new ConformEmailRequest()
            {
                Token = command.Token
            };
        }
    }


    
}
