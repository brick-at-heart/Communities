using BrickAtHeart.Communities.Areas.Community.PageModels;
using BrickAtHeart.Communities.Models;
using BrickAtHeart.Communities.Models.Authorization;
using BrickAtHeart.Communities.Models.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Areas.Community.Pages
{
    public class CreateModel : CommunityBasePageModel
    {
        [BindProperty]
        public CommunityCreationPageModel NewCommunity { get; set;}

        public CreateModel(UserStore userStore,
                           MembershipStore membershipStore,
                           CommunityStore communityStore,
                           RoleStore roleStore,
                           ILogger<CreateModel> logger) :
            base(userStore,
                 membershipStore,
                 communityStore)
        {
            this.roleStore = roleStore;
            this.logger = logger;
        }

        public void OnGet()
        {
            NewCommunity = new CommunityCreationPageModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await ValidateUniqueness();

            if (ModelState.IsValid)
            {
                Models.Community community = new Models.Community
                {
                    Id = -1,
                    FullName = NewCommunity.FullName,
                    JoinType = NewCommunity.JoinType,
                    ShortName = NewCommunity.ShortName,
                    SlackWorkspaceId = NewCommunity.SlackWorkspaceId
                };

                await communityStore.CreateCommunityAsync(community);

                Membership membership = new Membership
                {
                    CommunityId = community.Id,
                    UserId = User.GetUserId(),
                    IsActive = true
                };

                await membershipStore.CreateMembershipAsync(membership);

                Role ownerRole = new Role
                {
                    CommunityId = community.Id,
                    Name = "Owner",
                    IsCommunityDefault = false,
                    IsSystemGeneratedOwner = true
                };

                await roleStore.CreateAsync(ownerRole);

                MembershipRole membershipRole = new MembershipRole
                {
                    RoleId = ownerRole.Id,
                    MembershipId = membership.Id
                };

                await roleStore.CreateMembershipRoleAsync(membershipRole);

                Role memberRole = new Role
                {
                    CommunityId = community.Id,
                    Name = "Member",
                    IsCommunityDefault = true,
                    IsSystemGeneratedOwner = false
                };

                await roleStore.CreateAsync(memberRole);

                StatusMessage = $"{community.DisplayName} was created successfully.";
            }

            return Page();
        }

        public IActionResult OnPostClear()
        {
            return RedirectToPage();
        }

        private async Task ValidateUniqueness()
        {
            if (!string.IsNullOrWhiteSpace(NewCommunity?.FullName) && await communityStore.RetrieveCommunityByFullNameAsync(NewCommunity.FullName) != null)
            {
                ModelState.AddModelError("FullName", "The Full Name must be globally unique.");
            }

            if (!string.IsNullOrWhiteSpace(NewCommunity?.ShortName) && await communityStore.RetrieveCommunityByShortNameAsync(NewCommunity.ShortName) != null)
            {
                ModelState.AddModelError("ShortName", "The Short Name must be globally unique.");
            }
        }

        private readonly RoleStore roleStore;
        private readonly ILogger<CreateModel> logger;
    }
}