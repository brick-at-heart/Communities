using BrickAtHeart.Communities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Areas.User.Pages.Account
{
    [AllowAnonymous]
    public class RegisterConfirmationModel : CommunityBasePageModel
    {
        public RegisterConfirmationModel(UserStore userStore,
                                         MembershipStore membershipStore,
                                         CommunityStore communityStore,
                                         UserManager<Models.User> userManager) :
            base(userStore,
                 membershipStore,
                 communityStore)
        {
            this.userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(string email)
        {
            if (email == null)
            {
                return RedirectToPage("/Index");
            }

            Models.User user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound($"Unable to load user with email '{email}'.");
            }

            return Page();
        }

        private readonly UserManager<Models.User> userManager;
    }
}
