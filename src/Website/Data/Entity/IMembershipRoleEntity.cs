namespace BrickAtHeart.Communities.Data.Entity
{
    public interface IMembershipRoleEntity
    {
        public long Id {get; set;}

        public long MembershipId {get; set;}

        public long RoleId {get; set;}
    }
}