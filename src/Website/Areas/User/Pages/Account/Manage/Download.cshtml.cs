using BrickAtHeart.Communities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Areas.User.Pages.Account.Manage
{
    public class DownloadModel : CommunityBasePageModel
    {
        public DownloadModel(UserStore userStore,
                             MembershipStore membershipStore,
                             CommunityStore communityStore,
                             UserManager<Models.User> userManager,
                             ILogger<DownloadModel> logger) : 
            base(userStore,
                 membershipStore,
                 communityStore)
        {
            this.userManager = userManager;
            this.logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Models.User user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            logger.LogInformation("User with ID '{UserId}' asked for their personal data.", userManager.GetUserId(User));

            // Only include personal data for download
            Dictionary<string, string> profileData = new Dictionary<string, string>();
            IEnumerable<System.Reflection.PropertyInfo> personalDataProps = typeof(Models.User).GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));

            foreach (System.Reflection.PropertyInfo p in personalDataProps)
            {
                profileData.Add(p.Name, p.GetValue(user)?.ToString() ?? "null");
            }

            IList<UserLoginInfo> logins = await userManager.GetLoginsAsync(user);

            foreach (UserLoginInfo login in logins)
            {
                profileData.Add($"{login.LoginProvider} external login provider key", login.ProviderKey);
            }

            // profileData.Add($"Authenticator Key", await userManager.GetAuthenticatorKeyAsync(user));

            Response.Headers.Add("Content-Disposition", "attachment; filename=CommunityProfileData.json");
            return new FileContentResult(JsonSerializer.SerializeToUtf8Bytes(profileData), "application/json");
        }

        private readonly UserManager<Models.User> userManager;
        private readonly ILogger<DownloadModel> logger;
    }
}