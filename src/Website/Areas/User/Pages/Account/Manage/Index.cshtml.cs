using BrickAtHeart.Communities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Areas.User.Pages.Account.Manage.Profile
{
    public class IndexModel : CommunityBasePageModel
    {
        [BindProperty]
        [Display(Name = "City")]
        public string City { get; set; }

        [BindProperty]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [BindProperty]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [BindProperty]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        [BindProperty]
        [EmailAddress]
        [Display(Name = "Email")]
        [ReadOnly(true)]
        public string Email { get; set; }

        [BindProperty]
        [Display(Name = "Family Name")]
        public string SurName { get; set; }

        [BindProperty]
        [Display(Name = "Given Name")]
        public string GivenName { get; set; }

        [BindProperty]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [BindProperty]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [BindProperty]
        [Display(Name = "State")]
        public string Region { get; set; }

        [BindProperty]
        [Display(Name = "Address")]
        public string StreetAddressLine1 { get; set; }

        [BindProperty]
        [Display(Name = "Address 2")]
        public string StreetAddressLine2 { get; set; }

        public IndexModel(UserStore userStore,
                          MembershipStore membershipStore,
                          CommunityStore communityStore,
                          UserManager<Models.User> userManager,
                          SignInManager<Models.User> signInManager,
                          ILogger<IndexModel> logger) :
            base(userStore,
                 membershipStore,
                 communityStore)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Models.User user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            Load(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Models.User user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                Load(user);
                return Page();
            }

            bool userChanged = false;

            if (Country != user.Country)
            {
                user.Country = Country;
                userChanged = true;
            }

            if (StreetAddressLine1 != user.StreetAddressLine1)
            {
                user.StreetAddressLine1 = StreetAddressLine1;
                userChanged = true;
            }

            if (StreetAddressLine2 != user.StreetAddressLine2)
            {
                user.StreetAddressLine2 = StreetAddressLine2;
                userChanged = true;
            }

            if (City != user.City)
            {
                user.City = City;
                userChanged = true;
            }

            if (Region != user.Region)
            {
                user.Region = Region;
                userChanged = true;
            }

            if (PostalCode != user.PostalCode)
            {
                user.PostalCode = PostalCode;
                userChanged = true;
            }

            if (PhoneNumber != user.PhoneNumber)
            {
                user.PhoneNumber = PhoneNumber;
                user.PhoneNumberConfirmed = false;
                userChanged = true;
            }

            if (GivenName != user.GivenName)
            {
                user.GivenName = GivenName;
                userChanged = true;
            }

            if (SurName != user.SurName)
            {
                user.SurName = SurName;
                userChanged = true;
            }

            if (DateOfBirth != user.DateOfBirth)
            {
                user.DateOfBirth = DateOfBirth;
                userChanged = true;
            }

            if (userChanged)
            {
                await userManager.UpdateAsync(user);
            }

            await signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated.";
            return RedirectToPage();
        }

        public IActionResult OnPostDeleteAsync()
        {
            StatusMessage = "Delete functionality not yet available.";

            return RedirectToPage();
        }

        private void Load(Models.User user)
        {
            City = user.City;
            Country = user.Country;
            DateOfBirth = user.DateOfBirth;
            DisplayName = user.DisplayName;
            Email = user.Email;
            GivenName = user.GivenName;
            PhoneNumber = user.PhoneNumber;
            SurName = user.SurName;
            PostalCode = user.PostalCode;
            Region = user.Region;
            StreetAddressLine1 = user.StreetAddressLine1;
            StreetAddressLine2 = user.StreetAddressLine2;
        }

        private UserManager<Models.User> userManager;
        private SignInManager<Models.User> signInManager;
        private ILogger<IndexModel> logger;
    }
}
