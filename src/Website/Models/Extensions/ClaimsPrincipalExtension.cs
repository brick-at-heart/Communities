using System;
using System.Security.Claims;

namespace BrickAtHeart.Communities.Models.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static long GetUserId(this ClaimsPrincipal principal)
        {
            string claimValue = principal?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (long.TryParse(claimValue, out long userId))
            {
                return userId;
            }

            return 0;
        }

        public static TimeZoneInfo GetTimeZoneInfo(this ClaimsPrincipal principal, UserStore userStore)
        { 
            long userId = principal.GetUserId();

            User user = userStore.FindByIdAsync(userId.ToString()).Result;
            
            string timeZone = string.IsNullOrWhiteSpace(user.TimeZone) ? "UTC" : user.TimeZone;

            return TimeZoneInfo.FindSystemTimeZoneById(timeZone);
        }
    }
}
