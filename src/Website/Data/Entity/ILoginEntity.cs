namespace BrickAtHeart.Communities.Data.Entity
{
    public interface ILoginEntity
    {
        string? ProviderDisplayName { get; set; }

        string ProviderId { get; set; }

        string ProviderKey { get; set; }

        long UserId { get; set; }
    }
}