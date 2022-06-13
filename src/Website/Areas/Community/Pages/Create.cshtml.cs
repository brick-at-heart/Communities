using BrickAtHeart.Communities.Models;
using Microsoft.Extensions.Logging;

namespace BrickAtHeart.Communities.Areas.Community.Pages
{
    public class CreateModel : CommunityBasePageModel
    {
        public CreateModel(UserStore userStore,
                          MembershipStore membershipStore,
                          CommunityStore communityStore,
                          ILogger<CreateModel> logger) :
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
