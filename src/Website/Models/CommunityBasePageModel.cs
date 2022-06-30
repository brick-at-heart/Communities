using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
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

        public async Task<Community> GetCurrentCommunityForUser(long userId)
        {
            Membership membership = (await membershipStore.RetrieveMembershipsByUserIdAsync(userId)).FirstOrDefault(m => m.IsCurrent);
            return (await communityStore.RetrieveCommunitiesByUserIdAsync(userId)).FirstOrDefault(c => c.Id == membership.CommunityId);
        }

        protected readonly UserStore userStore;
        protected readonly MembershipStore membershipStore;
        protected readonly CommunityStore communityStore;
    }
}