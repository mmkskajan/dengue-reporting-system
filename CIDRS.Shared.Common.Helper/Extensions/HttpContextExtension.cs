using CIDRS.Shared.Common.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CIDRS.Shared.Common.Helper.Extensions
{
    public static class HttpContextExtension
    {
        public static string GetUserId(this HttpContext context)
        {
            if (context == null)
                return string.Empty;

            return context.User.Claims.Single(x => x.Type == CustomClaimTypes.Id).Value;
        }

        public static List<string> GetUserPermisions(this HttpContext context)
        {
            if (context == null)
                return new List<string>();

            return context.User.Claims.Where(x => x.Type == CustomClaimTypes.Permissions).Select(x => x.Value).ToList();
        }

        public static bool HasPermisions(this HttpContext context, string permission)
        {
            if (context == null)
                return false;

            List<string> permissions = context.User.Claims.Where(x => x.Type == CustomClaimTypes.Permissions).Select(x => x.Value).ToList();
            return permissions.Contains(permission);
        }
    }
}
