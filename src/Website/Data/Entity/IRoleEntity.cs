namespace BrickAtHeart.Communities.Data.Entity
{
    public interface IRoleEntity
    {
        long CommunityId { get; set; }

        long Id { get; set; }

        bool IsDefault { get; set; }

        string Name { get; set; }

        string NormalizedName { get; set; }
    }
}