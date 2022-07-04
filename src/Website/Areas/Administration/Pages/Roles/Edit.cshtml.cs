using BrickAtHeart.Communities.Areas.Administration.PageModels;
using BrickAtHeart.Communities.Models;
using BrickAtHeart.Communities.Models.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Areas.Administration.Pages.Roles
{
    [Authorize(Policy = "CanUpdateRole")]
    public class RoleEditModel : CommunityBasePageModel
    {
        public IList<MembershipRoleDetailsPageModel> AssignedMembers { get; set; }

        public IList<MembershipRoleDetailsPageModel> AvailableMembers { get; set; }

        public IList<RoleRight> Rights { get; set; }

        public RoleDetailsPageModel Role { get; set; }

        public RoleEditModel(UserStore userStore,
                             MembershipStore membershipStore,
                             CommunityStore communityStore,
                             RoleStore roleStore,
                             ILogger<RoleEditModel> logger ) :
            base(userStore,
                 membershipStore,
                 communityStore)
        {
            this.roleStore = roleStore;
            this.logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(long roleId)
        {
            await Load(roleId);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            long roleId = long.Parse(Request.Form["RoleId"].ToString());

            IList<RoleRight> existingRights = await roleStore.RetrieveRightsByRoleIdAsync(roleId);

            foreach (RoleRight right in existingRights)
            {
                RightState submittedValue = Request.Form[string.Concat("Right_", right.Id)].ToRightState();

                if (right.State != submittedValue)
                {
                    right.State = submittedValue;
                    await roleStore.UpdateRoleRightAsync(right);
                }
            }

            IList<MembershipRole> existingMemebrships = await roleStore.RetrieveRoleMembershipByRoleIdAsync(roleId);

            string[] assigned = Request.Form["from"].ToArray();

            foreach(string assignedMember in assigned)
            {
                if (existingMemebrships.All(m => m.MembershipId.ToString() != assignedMember))
                {
                    MembershipRole newRoleMembership = new MembershipRole
                    {
                        RoleId = roleId,
                        MembershipId = long.Parse(assignedMember)
                    };
                    await roleStore.CreateMembershipRoleAsync(newRoleMembership);
                }
            }

            foreach(MembershipRole existingMember in existingMemebrships)
            {
                if (assigned.All(s => s != existingMember.Id.ToString()))
                {
                    await roleStore.DeleteRoleMembershipAsync(existingMember);
                }
            }

            return RedirectToPage(new { roleId = roleId });
        }

        private async Task Load(long roleId)
        {
            RoleDetailsPageModel details = new RoleDetailsPageModel();

            Role role = await roleStore.FindByIdAsync(roleId.ToString());
            details.Id = roleId;
            details.Name = role.Name;
            
            // MembershipId
            List<MembershipRole> AssignedMemberships = (await roleStore.RetrieveRoleMembershipByRoleIdAsync(roleId)).ToList();

            // UserId && Membership
            List<Membership> AllMemberships = (await membershipStore.RetrieveMembershipsByCommunityIdAsync(role.CommunityId)).ToList();

            // UserId
            List<Models.User> AllUsers = (await userStore.FindByCommunityId(role.CommunityId)).ToList();

            AssignedMembers = new List<MembershipRoleDetailsPageModel>();
            AvailableMembers = new List<MembershipRoleDetailsPageModel>();

            foreach(Models.User user in AllUsers)
            {
                Membership membership = AllMemberships.FirstOrDefault(m => m.UserId == user.Id);

                if (membership != null)
                {
                    if (AssignedMemberships.Any(am => am.MembershipId == membership.Id))
                    {
                        AssignedMembers.Add(new MembershipRoleDetailsPageModel
                        {
                            DisplayName = user.DisplayName,
                            MembershipId = membership.Id
                        });
                    }
                    else
                    {
                        AvailableMembers.Add(new MembershipRoleDetailsPageModel
                        {
                            DisplayName = user.DisplayName,
                            MembershipId = membership.Id
                        });
                    }
                }
            }

            Role = details;

            Rights = await roleStore.RetrieveRightsByRoleIdAsync(roleId);
        }

        private readonly RoleStore roleStore;
        private readonly ILogger<RoleEditModel> logger;
    }
}
