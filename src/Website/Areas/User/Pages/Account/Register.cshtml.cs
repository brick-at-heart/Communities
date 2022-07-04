using BrickAtHeart.Communities.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Areas.User.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : CommunityBasePageModel
    {
        public IList<AuthenticationScheme> IdentityProviders { get; set; }

        public string ReturnUrl { get; set; }

        public RegisterModel(UserStore userStore,
                             MembershipStore membershipStore,
                             CommunityStore communityStore,
                             SignInManager<Models.User> signInManager,
                             ILogger<RegisterModel> logger) :
            base(userStore,
                 membershipStore,
                 communityStore)
        {
            this.signInManager = signInManager;
            this.logger = logger;

            IdentityProviders = new List<AuthenticationScheme>();
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            IdentityProviders = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (IdentityProviders != null &&
                IdentityProviders.Count > 0)
            {
                foreach (AuthenticationScheme provider in IdentityProviders)
                {
                    logger.LogInformation($"{provider.DisplayName} available as an external identity provider.");
                }
            }
        }

        private readonly SignInManager<Models.User> signInManager;
        private readonly ILogger<RegisterModel> logger;
    }
}
