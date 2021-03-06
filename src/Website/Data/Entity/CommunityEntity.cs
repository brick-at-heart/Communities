namespace BrickAtHeart.Communities.Data.Entity
{
    public class CommunityEntity : ICommunityEntity
    {
        public long Id { get; set; }

        public string FullName { get; set; }

        public byte JoinType { get; set; }

        public string NormalizedFullName { get; set; }

        public string NormalizedShortName { get; set; }

        public string ShortName { get; set; }

        public string SlackWorkspaceId { get; set; }
    }
}
