// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using BrickAtHeart.Communities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Areas.User.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public LogoutModel(SignInManager<Models.User> signInManager,
                           ILogger<LogoutModel> logger)
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
