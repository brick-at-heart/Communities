namespace BrickAtHeart.Communities.Data.Entity
{
    public interface ICommunityEntity
    {
        string FullName { get; set; }

        long Id { get; set; }

        byte JoinType { get; set; }

        string NormalizedFullName { get; set; }

        string NormalizedShortName { get; set; }

        string ShortName { get; set; }

        string SlackWorkspaceId { get; set; }
    }
}
