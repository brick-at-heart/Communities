using BrickAtHeart.Communities.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Areas.User.Pages.Account
{
    public class LoginModel : CommunityBasePageModel
    {
        public IList<AuthenticationScheme> IdentityProviders { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public string ReturnUrl { get; set; }

        public LoginModel(UserStore userStore,
                          MembershipStore membershipStore,
                          CommunityStore communityStore,
                          SignInManager<Models.User> signInManager,
                          ILogger<LoginModel> logger) :
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
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ReturnUrl = returnUrl ?? Url.Content("~/");
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
        private readonly ILogger<LoginModel> logger;
    }
}
