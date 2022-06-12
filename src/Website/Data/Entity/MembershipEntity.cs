namespace BrickAtHeart.Communities.Data.Entity
{
    public class MembershipEntity : IMembershipEntity
    {
        public long CommunityId { get; set; }

        public long Id { get; set; }

        public bool IsActive { get; set; }

        public bool IsCurrent { get; set; }

        public bool IsPrimary { get; set; }

        public string SlackMemberId { get; set; }

        public long UserId { get; set; }
    }
}