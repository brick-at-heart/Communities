namespace BrickAtHeart.Communities.Data.Entity
{
    public interface IMembershipEntity
    {
        //string DisplayName { get; set; }

        //string GivenName { get; set; }

        long Id { get; set; }

        bool IsActive { get; set; }

        bool IsCurrent { get; set; }

        bool IsPrimary { get; set; }

        string? SlackMemberId { get; set; }

        //string SurName { get; set; }

        long UserGroupId { get; set; }

        long UserId { get; set; }
    }
}