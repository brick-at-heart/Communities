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
    }
}
