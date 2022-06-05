namespace BrickAtHeart.Communities.Data.Entity
{
    public class MembershipEntity : IMembershipEntity
    {
        //public string DisplayName { get; set; }

        //public string GivenName { get; set; }

        public long Id { get; set; }

        public bool IsActive { get; set; }

        public bool IsCurrent { get; set; }

        public bool IsPrimary { get; set; }

        public string? SlackMemberId { get; set; }

        //public string SurName { get; set; }

        public long UserGroupId { get; set; }

        public long UserId { get; set; }
    }
}