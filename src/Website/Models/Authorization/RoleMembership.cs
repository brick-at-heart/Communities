namespace BrickAtHeart.Communities.Models.Authorization
{
    public class RoleMembership
    {
        public string DisplayName { get; set; }

        public string GivenName { get; set; }

        public long Id { get; set; }

        public long MembershipId { get; set; }

        public long RoleId { get; set; }

        public string SurName { get; set; }

        public RoleMembership(string displayName, string givenName, string surName)
        {
            DisplayName = displayName;
            GivenName = givenName;
            SurName = surName;
        }
    }
}