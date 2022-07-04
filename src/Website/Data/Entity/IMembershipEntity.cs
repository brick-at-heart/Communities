namespace BrickAtHeart.Communities.Data.Entity
{
    public interface IMembershipEntity
    {
        long CommunityId { get; set; }

        long Id { get; set; }

        bool IsActive { get; set; }

        bool IsCurrent { get; set; }

        bool IsPrimary { get; set; }

        string SlackMemberId { get; set; }

        long UserId { get; set; }
    }
}
