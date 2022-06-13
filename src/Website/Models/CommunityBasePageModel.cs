using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace BrickAtHeart.Communities.Models
{
    public class CommunityBasePageModel : PageModel
    {
        public long CurrentComunityId { get; private set; }

        public List<Community> Communities { get; private set; }        

        public CommunityBasePageModel(UserStore userStore,
                                      MembershipStore membershipStore,
                                      CommunityStore communityStore)
        {
            this.userStore = userStore;
            this.membershipStore = membershipStore;
            this.communityStore = communityStore;

            CurrentComunityId = 0;
            Communities = new List<Community>();
        }

        protected readonly UserStore userStore;
        protected readonly MembershipStore membershipStore;
        protected readonly CommunityStore communityStore;
    }
}