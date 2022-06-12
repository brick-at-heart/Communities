using BrickAtHeart.Communities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Areas.User.Pages.Account.Manage
{
    public class MembershipModel : PageModel
    {
        [BindProperty]
        public Membership MembershipDetails { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public MembershipModel(MembershipStore membershipStore,
                               UserManager<Models.User> userManager,
                               ILogger<MembershipModel> logger)
        {
            this.membershipStore = membershipStore;
            this.userManager = userManager;
            this.logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            MembershipDetails = await Load(User);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Membership membership = await Load(User);

            if (!ModelState.IsValid)
            {
                MembershipDetails = membership;
                return Page();
            }

            bool membershipChanged = false;

            if (MembershipDetails.IsPrimary != membership.IsPrimary)
            {
                membership.IsPrimary = MembershipDetails.IsPrimary;
                membershipChanged = true;
            }

            if (MembershipDetails.SlackMemberId != membership.SlackMemberId)
            {
                membership.SlackMemberId = MembershipDetails.SlackMemberId;
                membershipChanged = true;
            }

            if (membershipChanged)
            {
                await membershipStore.UpdateMembershipAsync(membership);
                StatusMessage = "Your membership has been updated.";
            }
            else
            {
                StatusMessage = "No changes detected.";
            }

            return RedirectToPage();
        }

        private async Task<Membership> Load(ClaimsPrincipal user)
        {
            long userId = long.Parse(userManager.GetUserId(User));
            IList<Membership> memberships = await membershipStore.RetrieveMembershipsByUserIdAsync(userId);
            return memberships.First();
        }

        private readonly MembershipStore membershipStore;
        private readonly UserManager<Models.User> userManager;
        private readonly ILogger<MembershipModel> logger;
    }
}
