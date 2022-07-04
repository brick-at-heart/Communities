using Microsoft.AspNetCore.Authorization;

namespace BrickAtHeart.Communities.Models.Authorization
{
    public class RequiredRightRequirement : IAuthorizationRequirement
    {
        public Right RequiredRight { get; }

        public RequiredRightRequirement(Right right)
        {
            RequiredRight = right;
        }
    }
}
