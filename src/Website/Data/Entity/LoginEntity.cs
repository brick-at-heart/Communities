namespace BrickAtHeart.Communities.Data.Entity
{
    public class LoginEntity : ILoginEntity
    {
        public string ProviderDisplayName { get; set; }

        public string ProviderId { get; set; }

        public string ProviderKey { get; set; }

        public long UserId { get; set; }

        public LoginEntity(string providerId, string providerKey)
        {
            ProviderId = providerId;
            ProviderKey = providerKey;
        }
    }
}