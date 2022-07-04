using BrickAtHeart.Communities.Models.Extensions;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Models.Authorization
{
    public class RequiredRightHandler : AuthorizationHandler<RequiredRightRequirement>
    {
        public RequiredRightHandler(RoleStore roleStore)
        {
            _roleStore = roleStore;
        }

        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                             RequiredRightRequirement requirement)
        {
            IList<RoleRight> rightStates = await _roleStore.RetrieveRightByRightIdUserIdAsync((long)requirement.RequiredRight, context.User.GetUserId());

            if (rightStates.Any(r => r.State == RightState.Denied))
            {
                return;
            }

            if (rightStates.Any(r => r.State == RightState.Granted))
            {
                context.Succeed(requirement);
            }

            return;
        }

        private readonly RoleStore _roleStore;
    }
}
