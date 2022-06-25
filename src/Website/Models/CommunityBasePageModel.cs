using BrickAtHeart.Communities.Models.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
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

        public async Task<IList<Community>> GetCommunitiesForUser(long userId)
        {
            return await communityStore.RetrieveCommunitiesByUserIdAsync(userId);
        }

        protected readonly UserStore userStore;
        protected readonly MembershipStore membershipStore;
        protected readonly CommunityStore communityStore;
    }
}