namespace BrickAtHeart.Communities.Data.Entity
{
    public class MembershipRoleEntity : IMembershipRoleEntity
    {
        public string DisplayName { get; set; }

        public string GivenName { get; set; }

        public long Id { get; set; }

        public long MembershipId {get; set;}

        public long RoleId {get; set;}

        public string SurName { get; set; }

        public MembershipRoleEntity(string displayName, string givenName, string surName)
        {
            DisplayName = displayName;
            GivenName = givenName;
            SurName = surName;
        }
    }
}