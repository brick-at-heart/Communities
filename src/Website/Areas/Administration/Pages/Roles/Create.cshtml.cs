using BrickAtHeart.Communities.Areas.Administration.PageModels;
using BrickAtHeart.Communities.Models;
using BrickAtHeart.Communities.Models.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Areas.Administration.Pages.Roles
{
    [Authorize(Policy = "CanCreateRole")]
    public class RoleCreateModel : CommunityBasePageModel
    {
        [BindProperty]
        public RoleDetailsPageModel RoleDetails { get; set; }

        [BindProperty]
        public long CommunityId { get; set; }

        public RoleCreateModel(UserStore userStore,
                               MembershipStore membershipStore,
                               CommunityStore communityStore,
                               RoleStore roleStore,
                               ILogger<RoleCreateModel> logger ) : 
            base(userStore,
                 membershipStore,
                 communityStore)
        {
            this.roleStore = roleStore;
            this.logger = logger;
        }

        public void OnGet(long communityId)
        {
            CommunityId = communityId;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Role newRole = new Role
            {
                Name = RoleDetails.Name,
                NormalizedName = RoleDetails.Name,
                CommunityId = CommunityId
            };

            await roleStore.CreateAsync(newRole);
            return RedirectToPage();
        }

        private readonly RoleStore roleStore;
        private readonly ILogger<RoleCreateModel> logger;
    }
}