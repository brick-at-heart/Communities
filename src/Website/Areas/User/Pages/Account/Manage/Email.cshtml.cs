using BrickAtHeart.Communities.Models;
using BrickAtHeart.Communities.Services.Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Areas.User.Pages.Account.Manage
{
    public class EmailModel : PageModel
    {
        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "New Email")]
        public string NewEmail { get; set; }

        public string OldEmail { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public EmailModel(UserManager<Models.User> userManager,
                          IEmailService emailService,
                          IOptions<SystemSettings> systemSettings)
        {
            this.userManager = userManager;
            this.emailService = emailService;
            this.systemSettings = systemSettings.Value;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Models.User user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                StatusMessage = "There was an error loading your account.";
                return RedirectToPage();
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostChangeEmailAsync()
        {
            Models.User user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                StatusMessage = "There was an error loading your account.";
                return RedirectToPage();
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            string email = await userManager.GetEmailAsync(user);

            if (NewEmail != null &&
                NewEmail != email)
            {
                string userId = await userManager.GetUserIdAsync(user);
                string code = await userManager.GenerateChangeEmailTokenAsync(user, NewEmail);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                string callbackUrl = Url.Page("/Account/ConfirmEmailChange",
                                                pageHandler: null,
                                                values: new { area = "User", userId = userId, email = NewEmail, code = code },
                                                protocol: Request.Scheme);

                if (callbackUrl != null)
                { 
                    IEmailAddress sender = new EmailAddress { Name = systemSettings.SystemName, Address = systemSettings.SystemEmail };
                    IEmailAddress recipient = new EmailAddress { Name = user.DisplayName, Address = NewEmail };

                    await emailService.SendSingleEmailAsync(sender, recipient, "Confirm Your Email Address",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.",
                        $"Please confirm your account by visiting this link: {HtmlEncoder.Default.Encode(callbackUrl)}.");

                    StatusMessage = "Confirmation link to change email sent. Please check your email.";
                }
                else
                {
                    StatusMessage = "An error occurred.";
                }

                return RedirectToPage();
            }

            StatusMessage = "Your email is unchanged.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            Models.User user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                StatusMessage = "There was an error loading your account.";
                return RedirectToPage();
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            string userId = await userManager.GetUserIdAsync(user);
            string email = await userManager.GetEmailAsync(user);
            string code = await userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            string callbackUrl = Url.Page("/Account/ConfirmEmail",
                                           pageHandler: null,
                                           values: new { area = "User", userId = userId, code = code },
                                           protocol: Request.Scheme);

            if (NewEmail != null &&
                callbackUrl != null)
            {
                IEmailAddress sender = new EmailAddress { Name = systemSettings.SystemName, Address = systemSettings.SystemEmail };
                IEmailAddress recipient = new EmailAddress { Name = user.DisplayName, Address = NewEmail };

                await emailService.SendSingleEmailAsync(sender, recipient, "Confirm Your Email Address",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.",
                    $"Please confirm your account by visiting this link: {HtmlEncoder.Default.Encode(callbackUrl)}.");

                StatusMessage = "Confirmation link to change email sent. Please check your email.";
            }
            else
            {
                StatusMessage = "An error occurred.";
            }

            return RedirectToPage();
        }

        private async Task LoadAsync(Models.User user)
        {
            string email = await userManager.GetEmailAsync(user);

            NewEmail = email;
            OldEmail = email;

            IsEmailConfirmed = await userManager.IsEmailConfirmedAsync(user);
        }

        private readonly UserManager<Models.User> userManager;
        private readonly IEmailService emailService;
        private readonly SystemSettings systemSettings;
    }
}