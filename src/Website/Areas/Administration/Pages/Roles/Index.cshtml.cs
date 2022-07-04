using BrickAtHeart.Communities.Areas.Administration.PageModels;
using BrickAtHeart.Communities.Models;
using BrickAtHeart.Communities.Models.Attributes;
using BrickAtHeart.Communities.Models.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Areas.Administration.Pages.Roles
{
    [Authorize(Policy = "MaintainRoles")]
    public class RoleIndexModel : CommunityBasePageModel
    {
        public List<RoleDetailsPageModel> Roles { get; set; }

        public long CommunityId { get; set; }

        public RoleIndexModel(UserStore userStore,
                              MembershipStore membershipStore,
                              CommunityStore communityStore,
                              RoleStore roleStore,
                              IAuthorizationService authorizationService,
                              ILogger<RoleIndexModel> logger ) : 
            base (userStore,
                  membershipStore,
                  communityStore)
        {
            this.roleStore = roleStore;
            this.authorizationService = authorizationService;
            this.logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Roles = await Load(User);

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteRoleAsync(long roleId)
        {
            if (!(await authorizationService.AuthorizeAsync(User, Right.DeleteRole.GetPolicyName())).Succeeded)
            {
                StatusMessage = "ERROR: You are not authorized to delete roles.";
                return RedirectToPage();
            }

            Role role = new Role
            {
                Id = roleId
            };

            await roleStore.DeleteAsync(role);

            return RedirectToPage();
        }

        private async Task<List<RoleDetailsPageModel>> Load(ClaimsPrincipal user)
        {
            Membership membership = await GetCurrentMembershipForUser(user);
            CommunityId = membership.CommunityId;

            List<Role> roles = (await roleStore.RetrieveRolesByCommunityIdAsync(CommunityId)).ToList();
            List<RoleDetailsPageModel> details = new List<RoleDetailsPageModel>();

            foreach(Role role in roles)
            {
                details.Add(new RoleDetailsPageModel
                {
                    Id = role.Id,
                    Name = role.Name
                });
            }

            return details;
        }

        private readonly RoleStore roleStore;
        private readonly IAuthorizationService authorizationService;
        private readonly ILogger<RoleIndexModel> logger;
    }
}
