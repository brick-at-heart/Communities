using BrickAtHeart.Communities.Models.Extensions;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Models.Authorization
{
    public class RequireAnyRightHandler: AuthorizationHandler<RequireAnyRightRequirement>
    {
        public RequireAnyRightHandler(RoleStore roleStore)
        {
            _roleStore = roleStore;
        }

        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                             RequireAnyRightRequirement requirement)
        {
            foreach (Right right in requirement.RequiredRights)
            {
                IList<RoleRight> rightStates = await _roleStore.RetrieveRightByRightIdUserIdAsync((long)right, context.User.GetUserId());

                if (rightStates.Any(r => r.State == RightState.Granted))
                {
                    context.Succeed(requirement);
                    return;
                }
            }
        }

        private readonly RoleStore _roleStore;
    }
}
