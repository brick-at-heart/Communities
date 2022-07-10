using BrickAtHeart.Communities.Models.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Models
{
    public class CommunityBasePageModel : PageModel
    {
        [TempData]
        public string StatusMessage { get; set; }

        public CommunityBasePageModel(UserStore userStore,
                                      MembershipStore membershipStore,
                                      CommunityStore communityStore)
        {
            this.userStore = userStore;
            this.membershipStore = membershipStore;
            this.communityStore = communityStore;
        }

        public async Task<Community> GetCurrentCommunityForUser(ClaimsPrincipal user)
        {
            Membership membership = (await GetCurrentMembershipForUser(user));

            if (membership == null)
            {
                return null;
            }

            return (await communityStore.RetrieveCommunitiesByUserIdAsync(membership.UserId)).FirstOrDefault(c => c.Id == membership.CommunityId);
        }

        public async Task<Membership> GetCurrentMembershipForUser(ClaimsPrincipal user)
        {
            long userId = user.GetUserId();

            return (await membershipStore.RetrieveMembershipsByUserIdAsync(userId)).FirstOrDefault(m => m.IsCurrent);
        }

        protected readonly UserStore userStore;
        protected readonly MembershipStore membershipStore;
        protected readonly CommunityStore communityStore;
    }
}