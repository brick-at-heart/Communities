using BrickAtHeart.Communities.Models;
using Microsoft.Extensions.Logging;

namespace BrickAtHeart.Communities.Pages
{
    public class PrivacyModel : CommunityBasePageModel
    {
        public PrivacyModel(UserStore userStore,
                            MembershipStore membershipStore,
                            CommunityStore communityStore, 
                            ILogger<PrivacyModel> logger) : 
            base(userStore, membershipStore, communityStore)
        {
            this.logger = logger;
        }

        public void OnGet()
        {
        }

        private readonly ILogger<PrivacyModel> logger;
    }
}