using BrickAtHeart.Communities.Models;
using Microsoft.Extensions.Logging;

namespace BrickAtHeart.Communities.Areas.Community.Pages
{
    public class IndexModel : CommunityBasePageModel
    {
        public IndexModel(UserStore userStore,
                          MembershipStore membershipStore,
                          CommunityStore communityStore,
                          ILogger<IndexModel> logger) :
        base(userStore,
             membershipStore,
             communityStore)
        {
        }

        public void OnGet()
        {
        }
    }
}