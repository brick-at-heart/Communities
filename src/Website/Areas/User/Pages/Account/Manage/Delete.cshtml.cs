using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Areas.User.Pages.Account.Manage
{
    public class DeleteAccountModel : PageModel
    {
        [Required]
        [DataType(DataType.Password)]
        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public bool RequirePassword { get; set; }

        public DeleteAccountModel(UserManager<Models.User> userManager,
                                  SignInManager<Models.User> signInManager,
                                  ILogger<DeleteAccountModel> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            Models.User user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            RequirePassword = await userManager.HasPasswordAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Models.User user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            RequirePassword = await userManager.HasPasswordAsync(user);

            if (RequirePassword)
            {
                if (!await userManager.CheckPasswordAsync(user, Password))
                {
                    ModelState.AddModelError(string.Empty, "Incorrect password.");
                    return Page();
                }
            }

            IdentityResult result = await userManager.DeleteAsync(user);
            string userId = await userManager.GetUserIdAsync(user);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleting user.");
            }

            await signInManager.SignOutAsync();

            logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);

            return Redirect("~/");
        }

        private readonly UserManager<Models.User> userManager;
        private readonly SignInManager<Models.User> signInManager;
        private readonly ILogger<DeleteAccountModel> logger;
    }
}
