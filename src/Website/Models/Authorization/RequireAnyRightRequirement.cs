using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace BrickAtHeart.Communities.Models.Authorization
{
    public class RequireAnyRightRequirement: IAuthorizationRequirement
    {
        public IList<Right> RequiredRights { get; private set; }

        public RequireAnyRightRequirement(IList<Right> rights)
        {
            RequiredRights = rights;
        }
    }
}
