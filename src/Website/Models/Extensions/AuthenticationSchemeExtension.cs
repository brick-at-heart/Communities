using Microsoft.AspNetCore.Authentication;

namespace BrickAtHeart.Communities.Models.Extensions
{
    public static class AuthenticationSchemeExtension
    {
        public static string GetLogoPath(this AuthenticationScheme provider)
        {
            return $"/img/{provider.Name}Logo.png";
        }
    }
}