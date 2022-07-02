using BrickAtHeart.Communities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Areas.Administration.Pages
{
    public class ProfileModel : CommunityBasePageModel
    {

        [BindProperty]
        public Models.Community Community { get; set; }

        public ProfileModel(UserStore userStore,
                            MembershipStore membershipStore,
                            CommunityStore communityStore) :
            base(userStore,
                 membershipStore,
                 communityStore)
        {

        }

        public async Task<IActionResult> OnGetAsync()
        {
            Community = await GetCurrentCommunityForUser(User);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Community = await GetCurrentCommunityForUser(User);
                return Page();
            }

            Models.Community community = await GetCurrentCommunityForUser(User);
            bool communityChanged = false;

            if (community.FullName != Community.FullName)
            {
                community.FullName = Community.FullName;
                communityChanged = true;
            }

            if (community.ShortName != Community.ShortName)
            {
                community.ShortName = Community.ShortName;
                communityChanged = true;
            }

            if (community.SlackWorkspaceId != Community.SlackWorkspaceId)
            {
                community.SlackWorkspaceId = Community.SlackWorkspaceId;
                communityChanged = true;
            }

            if (community.JoinType != Community.JoinType)
            {
                community.JoinType = Community.JoinType;
                communityChanged = true;
            }

            if (communityChanged)
            {
                await communityStore.UpdateCommunityAsync(community);
                StatusMessage = "The Community was saved successfully.";
            }
            else
            {
                StatusMessage = "The Community was not changed.";
            }

            return RedirectToPage();
        }
    }
}