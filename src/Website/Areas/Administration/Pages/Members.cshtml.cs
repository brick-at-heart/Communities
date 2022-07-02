using BrickAtHeart.Communities.Areas.Administration.PageModels;
using BrickAtHeart.Communities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Areas.Administration.Pages
{
    [Authorize(Policy = "CanMaintainMemberships")]
    public class MembersModel : CommunityBasePageModel
    {
        [BindProperty(SupportsGet = true)]
        public List<MembershipDetailsPageModel> MembershipDetails { get; set; }

        public MembersModel(UserStore userStore,
                            MembershipStore membershipStore,
                            CommunityStore communityStore)
            : base(userStore,
                   membershipStore,
                   communityStore)
        {
            StatusMessage = string.Empty;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            MembershipDetails = await Load();

            return Page();
        }

        public async Task<IActionResult> OnPostExpelAsync(long membershipId)
        {
            return await Task.FromResult(RedirectToPage());
        }

        public async Task<IActionResult> OnPostToggleActivationAsync(long membershipId)
        {
            Membership membership = await membershipStore.RetrieveMembershipByMembershipIdAsync(membershipId);

            membership.IsActive = !membership.IsActive;

            await membershipStore.UpdateMembershipAsync(membership);

            MembershipDetails = await Load();

            return RedirectToPage();
        }

        private async Task<List<MembershipDetailsPageModel>> Load()
        {
            Membership membership = await GetCurrentMembershipForUser(User);
            List<Membership> members = new List<Membership>(await membershipStore.RetrieveMembershipsByCommunityIdAsync(membership.CommunityId));
            List<MembershipDetailsPageModel> details = new List<MembershipDetailsPageModel>();

            foreach (Membership member in members)
            {
                Models.User user = await userStore.FindByIdAsync(member.UserId.ToString());
                details.Add(new MembershipDetailsPageModel
                {
                    Id = member.Id,
                    FullName = user.GivenName + " " + user.SurName + " (" + user.DisplayName + ")",
                    IsActive = member.IsActive,
                    IsPrimary = member.IsPrimary
                });
            }

            return details;
        }
    }
}
