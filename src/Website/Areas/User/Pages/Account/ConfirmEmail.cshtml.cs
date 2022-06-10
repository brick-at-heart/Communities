﻿using BrickAtHeart.Communities.Services.Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Areas.User.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        [TempData]
        public string? StatusMessage { get; set; }

        public ConfirmEmailModel(UserManager<Models.User> userManager,
                                 IEmailService emailService,
                                 ILogger<ConfirmEmailModel> logger)
        {
            this.userManager = userManager;
            this.emailService = emailService;
            this.logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                logger.LogWarning("An email validation was attempted which was missing either the userId or code.");
                StatusMessage = "There was an error with your request.";
                return Page();
            }

            Models.User user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                logger.LogWarning("An email validation was attempted but the user was not found.");
                StatusMessage = "There was an error retrieving your account.";
                return Page();
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            IdentityResult result = await userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                StatusMessage = "Thank you for confirming your email. In order to fully engage, the LUG Administrator must approve your account.";
            }
            else
            {
                StatusMessage = "There was an error validatig your email.";
            }

            return Page();
        }

        private readonly UserManager<Models.User> userManager;
        private readonly ILogger<ConfirmEmailModel> logger;
        private readonly IEmailService emailService;
    }
}