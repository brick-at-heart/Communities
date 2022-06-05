namespace BrickAtHeart.Communities.Data.Entity
{
    public interface ICatalogEntity
    {
        long Id { get; set; }

        string Name { get; set; }

        string NormalizedName { get; set; }

        long UserGroupId { get; set; }
    }
}