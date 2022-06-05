namespace BrickAtHeart.Communities.Data.Entity
{
    public interface IMembershipRoleEntity
    {
        public string DisplayName {get; set;}

        public string GivenName {get; set;}

        public long Id {get; set;}

        public long MembershipId {get; set;}

        public long RoleId {get; set;}

        public string SurName {get; set;}
    }
}