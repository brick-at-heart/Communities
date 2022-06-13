using BrickAtHeart.Communities.Areas.User.PageModels;
using BrickAtHeart.Communities.Models;
using BrickAtHeart.Communities.Services.Email;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace BrickAtHeart.Communities.Areas.User.Pages.Account
{
    [AllowAnonymous]
    public class ExternalLoginModel : CommunityBasePageModel
    {
        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public string ProviderDisplayName { get; set; }

        [BindProperty]
        public RegistrationPageModel Registration { get; set; }

        [BindProperty]
        public string ReturnUrl { get; set; }

        public ExternalLoginModel(UserStore userStore,
                                  MembershipStore membershipStore,
                                  CommunityStore communityStore,
                                  SignInManager<Models.User> signInManager,
                                  UserManager<Models.User> userManager,
                                  ILookupNormalizer normalizer,
                                  IEmailService emailService,
                                  IOptions<SystemSettings> systemSettings,
                                  ILogger<ExternalLoginModel> logger) :
            base(userStore,
                 membershipStore,
                 communityStore)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.emailService = emailService;
            this.systemSettings = systemSettings.Value;
            this.normalizer = normalizer;
            this.logger = logger;

            Registration = new RegistrationPageModel();
        }

        public IActionResult OnGet()
        {
            return RedirectToPage("./Login");
        }

        /// <summary>
        ///  Handles the response received from the external identity provider.
        /// </summary>
        /// <param name="returnUrl">
        /// </param>
        /// <param name="remoteError">
        /// </param>
        /// <returns>
        /// </returns>
        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            returnUrl ??= Url.Content("~/");

            if (remoteError != null)
            {
                StatusMessage = $"Error from external provider: {remoteError}";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            ExternalLoginInfo loginInfo = await signInManager.GetExternalLoginInfoAsync();

            if (loginInfo == null)
            {
                StatusMessage = "Error loading extenral login infomration.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            SignInResult signInResult = await signInManager.ExternalLoginSignInAsync(loginInfo.LoginProvider, loginInfo.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                logger.LogInformation($"{loginInfo.Principal?.Identity?.Name} logged in with {loginInfo.LoginProvider} provider.");
                return LocalRedirect(returnUrl);
            }

            if (signInResult.IsLockedOut)
            {
                logger.LogInformation($"{loginInfo.Principal?.Identity?.Name} was blocked from logging in, because they are locked out.");
                return RedirectToPage("./Lockout");
            }

            if (signInResult.IsNotAllowed)
            {
                Models.User user = await userManager.FindByLoginAsync(loginInfo.LoginProvider, loginInfo.ProviderKey);

                if (user != null &&
                    !user.EmailConfirmed)
                {
                    logger.LogInformation($"{loginInfo.Principal?.Identity?.Name} tried to register, but was blocked because they already have an account pending email confirmation.");
                    return RedirectToPage("./RegisterConfirmation", new { user.Email});
                }
            }

            // If the user does not have an account, then ask the user to create an account.
            ReturnUrl = returnUrl;
            ProviderDisplayName = loginInfo.ProviderDisplayName;

            if (loginInfo.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
            {
                Registration = new RegistrationPageModel
                {
                    Email = loginInfo.Principal.FindFirstValue(ClaimTypes.Email),
                    DisplayName = loginInfo.Principal.FindFirstValue(ClaimTypes.Email)
                };
            }

            return Page();
        }

        /// <summary>
        ///  Configures and makes the call to the external identity provider.
        /// </summary>
        /// <param name="provider">
        /// </param>
        /// <param name="returnUrl">
        /// </param>
        /// <returns>
        /// </returns>
        public IActionResult OnPost(string provider, string returnUrl = "")
        {
            string redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });
            AuthenticationProperties properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> OnPostConfirmationAsync(string returnUrl = "~/")
        {
            if (returnUrl.StartsWith("~"))
            {
                returnUrl = Url.Content(returnUrl);
            }

            ExternalLoginInfo loginInfo = await signInManager.GetExternalLoginInfoAsync();

            if (loginInfo == null)
            {
                StatusMessage = "Error loading extenral login infomration.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            if (Registration != null &&
                ModelState.IsValid)
            {
                Models.User newUser = new Models.User(Registration.DisplayName)
                {
                    Id = -1,
                    DateOfBirth = Registration.DateOfBirth,
                    Email = Registration.Email,
                    GivenName = loginInfo.Principal.FindFirstValue(ClaimTypes.GivenName),
                    IsActive = true,
                    NormalizedEmail = normalizer.NormalizeEmail(Registration.Email),
                    NormalizedUserName = normalizer.NormalizeEmail(Registration.Email),
                    SurName = loginInfo.Principal.FindFirstValue(ClaimTypes.Surname),
                    UserName = Registration.Email
                };

                IdentityResult result = await userManager.CreateAsync(newUser);

                if (result.Succeeded)
                {
                    result = await userManager.AddLoginAsync(newUser, loginInfo);

                    if (result.Succeeded)
                    {
                        logger.LogInformation($"User created an account using {loginInfo.LoginProvider} provider.");

                        string userId = await userManager.GetUserIdAsync(newUser);
                        string code = await userManager.GenerateEmailConfirmationTokenAsync(newUser);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        string callbackUrl = Url.Page("/Account/ConfirmEmail",
                                                      pageHandler: null,
                                                      values: new { area = "User", userId, code },
                                                      protocol: Request.Scheme);

                        if (callbackUrl != null)
                        { 
                            IEmailAddress sender = new EmailAddress { Name = systemSettings.SystemName, Address = systemSettings.SystemEmail };
                            IEmailAddress recipient = new EmailAddress { Name = newUser.DisplayName, Address = newUser.Email };
                            await emailService.SendSingleEmailAsync(sender, recipient, "Confirm Your Email Address",
                                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.",
                                $"Please confirm your account by visiting this link: {HtmlEncoder.Default.Encode(callbackUrl)}.");
                        }

                        if (userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            return RedirectToPage("./RegisterConfirmation", new { Registration.Email });
                        }

                        await signInManager.SignInAsync(newUser, isPersistent: false, loginInfo.LoginProvider);

                        return LocalRedirect(returnUrl);
                    }
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ProviderDisplayName = loginInfo.ProviderDisplayName;
            ReturnUrl = returnUrl;

            return Page();
        }

        private readonly SignInManager<Models.User> signInManager;
        private readonly UserManager<Models.User> userManager;
        private readonly ILogger<ExternalLoginModel> logger;
        private readonly ILookupNormalizer normalizer;
        private readonly IEmailService emailService;
        private readonly SystemSettings systemSettings;
    }
}