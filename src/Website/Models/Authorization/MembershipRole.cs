namespace BrickAtHeart.Communities.Models.Authorization
{
    public class MembershipRole
    {
        public long Id { get; set; }

        public long MembershipId { get; set; }

        public long RoleId { get; set; }
    }
}
