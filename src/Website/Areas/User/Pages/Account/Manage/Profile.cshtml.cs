using BrickAtHeart.Communities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Areas.User.Pages.Account.Manage
{
    public class ProfileModel : CommunityBasePageModel
    {
        [BindProperty]
        public bool IsActive { get; set; }

        [BindProperty]
        public bool IsPrimary { get; set; }

        [BindProperty]
        public long MembershipId { get; set;}

        [BindProperty]
        public string SlackMemberId { get; set;}

        public ProfileModel(UserStore userStore,
                            MembershipStore membershipStore,
                            CommunityStore communityStore,
                            ILogger<ProfileModel> logger) :
            base (userStore,
                  membershipStore,
                  communityStore)
        {
            this.logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(long membershipId)
        {
            MembershipId = membershipId;

            Membership membership = await membershipStore.RetrieveMembershipByMembershipIdAsync(membershipId);

            IsActive = membership.IsActive;
            IsPrimary = membership.IsPrimary;
            SlackMemberId = membership.SlackMemberId;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Membership membership = new Membership
            {
                Id = MembershipId,
                IsActive = IsActive,
                IsPrimary = IsPrimary,
                SlackMemberId = SlackMemberId
            };

            await membershipStore.UpdateMembershipAsync(membership);

            StatusMessage = "Profile updated successfully.";

            return Page();
        }

        private readonly ILogger<ProfileModel> logger;
    }
}