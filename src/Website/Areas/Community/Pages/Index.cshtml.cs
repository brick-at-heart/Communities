using BrickAtHeart.Communities.Areas.Community.PageModels;
using BrickAtHeart.Communities.Models;
using BrickAtHeart.Communities.Models.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Areas.Community.Pages
{
    public class IndexModel : CommunityBasePageModel
    {
        [BindProperty]
        public List<CommunityDisplayPageModel> Communities { get; set; }

        public IndexModel(UserStore userStore,
                          MembershipStore membershipStore,
                          CommunityStore communityStore,
                          ILogger<IndexModel> logger) :
        base(userStore,
             membershipStore,
             communityStore)
        {
        }

        public async Task OnGetAsync()
        {
            await Load(User);

            return;
        }

        public async Task<IActionResult> OnPostJoinAsync(long communityid)
        {
            Models.Community community = await communityStore.RetrieveCommunityByCommunityIdAsync(communityid);

            Membership membership = new Membership
            {
                CommunityId = communityid,
                UserId = User.GetUserId(),
                IsActive = community?.JoinType == CommunityJoinType.Open
            };

            await membershipStore.CreateMembershipAsync(membership);

            await Load(User);

            return Page();
        }

        public async Task<IActionResult> OnPostLeaveAsync(long membershipId)
        {
            await membershipStore.DeleteMembershipAsync(membershipId);

            await Load(User);

            return Page();
        }

        private async Task Load(ClaimsPrincipal user)
        {
            var memberships = await membershipStore.RetrieveMembershipsByUserIdAsync(user.GetUserId());
            var allCommunities = (List<Models.Community>)await communityStore.RetrieveCommunitiesByJoinTypesAsync((byte)(CommunityJoinType.Open | CommunityJoinType.Gated));
            Communities = new List<CommunityDisplayPageModel>();

            foreach(Models.Community community in allCommunities)
            {
                CommunityDisplayPageModel model = new CommunityDisplayPageModel
                {
                    DisplayName = community.DisplayName,
                    Id = community.Id,
                };

                if (memberships.Any(m => m.CommunityId == community.Id))
                {
                    model.MembershipId = memberships.First(m => m.CommunityId == community.Id).Id;
                }

                Communities.Add(model);
            }
        }
    }
}
