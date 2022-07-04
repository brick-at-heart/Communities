using BrickAtHeart.Communities.Areas.User.PageModels;
using BrickAtHeart.Communities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Areas.User.Pages.Account.Manage
{
    public class MembershipsModel : CommunityBasePageModel
    {
        [BindProperty]
        public List<MembershipPageModel> Memberships { get; set; }

        public MembershipsModel(UserStore userStore,
                          MembershipStore membershipStore,
                          CommunityStore communityStore,
                          UserManager<Models.User> userManager,
                          ILogger<MembershipsModel> logger) : 
            base(userStore,
                 membershipStore,
                 communityStore)
        {
            this.userManager = userManager;
            this.logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Memberships = await Load();

            return Page();
        }

        public async Task<IActionResult> OnPostLeaveAsync(long membershipId)
        {
            await membershipStore.DeleteMembershipAsync(membershipId);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSetCurrentAsync(long membershipId)
        {
            Membership membership = await membershipStore.RetrieveMembershipByMembershipIdAsync(membershipId);

            membership.IsCurrent = true;

            await membershipStore.UpdateMembershipAsync(membership);

            return RedirectToPage();
        }

        private async Task<List<MembershipPageModel>> Load()
        {
            long userId = long.Parse(userManager.GetUserId(User));
            IList<Membership> memberships = await membershipStore.RetrieveMembershipsByUserIdAsync(userId);
            IList<Models.Community> communities = await communityStore.RetrieveCommunitiesByUserIdAsync(userId);

            List<MembershipPageModel> result = new List<MembershipPageModel>();

            foreach(Membership membership in memberships)
            {
                MembershipPageModel mem = new MembershipPageModel
                {
                    CommunityId = membership.CommunityId,
                    DisplayName = communities.Single(c => c.Id == membership.CommunityId).DisplayName,
                    Id = membership.Id,
                    IsActive = membership.IsActive,
                    IsCurrent = membership.IsCurrent
                };

                result.Add(mem);
            }

            return result;
        }

        private readonly UserManager<Models.User> userManager;
        private readonly ILogger<MembershipsModel> logger;
    }
}
