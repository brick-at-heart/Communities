using BrickAtHeart.Communities.Models;
using Microsoft.Extensions.Logging;

namespace BrickAtHeart.Communities.Pages
{
    public class IndexModel : CommunityBasePageModel
    {
        public IndexModel(UserStore userStore,
                          MembershipStore membershipStore,
                          CommunityStore communityStore,
                          ILogger<IndexModel> logger) :
            base(userStore, membershipStore, communityStore)
        {
            this.logger = logger;
        }

        public void OnGet()
        {

        }

        private readonly ILogger<IndexModel> logger;
    }
}