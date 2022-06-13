using BrickAtHeart.Communities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Areas.User.Pages.Account
{
    public class ConfirmEmailChangeModel : CommunityBasePageModel
    {
        [TempData]
        public string StatusMessage { get; set; }

        public ConfirmEmailChangeModel(UserStore userStore,
                                       MembershipStore membershipStore,
                                       CommunityStore communityStore,
                                       UserManager<Models.User> userManager,
                                       SignInManager<Models.User> signInManager,
                                       ILogger<ConfirmEmailChangeModel> logger) :
            base(userStore,
                 membershipStore,
                 communityStore)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(string userId, string email, string code)
        {
            if (userId == null || email == null || code == null)
            {
                _logger.LogWarning("An email change validation was attempted which was missing either the userId, email or code.");
                StatusMessage = "There was an error with your request.";
                return Page();
            }

            Models.User user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                _logger.LogWarning("An email change validation was attempted but the user was not found.");
                StatusMessage = "There was an error retrieving your account.";
                return Page();
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            IdentityResult result = await _userManager.ChangeEmailAsync(user, email, code);

            if (!result.Succeeded)
            {
                StatusMessage = "There was an error changing your email.";
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Thank you for confirming your email change.";
            return Page();
        }

        private readonly UserManager<Models.User> _userManager;
        private readonly SignInManager<Models.User> _signInManager;
        ILogger<ConfirmEmailChangeModel> _logger;
    }
}