using BrickAtHeart.Communities.Models.Attributes;

namespace BrickAtHeart.Communities.Models.Authorization
{
    public enum Right
    {
        [PolicyName("CanCreateRole")]
        CreateRole = 1,

        [PolicyName("CanUpdateRole")]
        UpdateRole = 2,

        [PolicyName("CanDeleteRole")]
        DeleteRole = 3,

        [PolicyName("CanMaintainUserGroupProfile")]
        MaintainUserGroupProfile = 4,

        [PolicyName("CanMaintainMemberships")]
        MaintainMemberships = 5
    }
}
