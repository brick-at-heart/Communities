using BrickAtHeart.Communities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Areas.User.Pages.Account
{
    public class LogoutModel : CommunityBasePageModel
    {
        public LogoutModel(UserStore userStore,
                           MembershipStore membershipStore,
                           CommunityStore communityStore,
                           SignInManager<Models.User> signInManager,
                           ILogger<LogoutModel> logger) :
            base(userStore,
                 membershipStore,
                 communityStore)
        {
            this.signInManager = signInManager;
            this.logger = logger;
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await signInManager.SignOutAsync();
            logger.LogInformation("User logged out.");

            returnUrl ??= Url.Content("~/");
            return LocalRedirect(returnUrl);
        }

        private readonly SignInManager<Models.User> signInManager;
        private readonly ILogger<LogoutModel> logger;
    }
}
