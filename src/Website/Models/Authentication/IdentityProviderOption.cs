namespace BrickAtHeart.Communities.Models.Authentication
{
    public class IdentityProviderOption
    {
        public const string Section = "BrickAtHeart:Communities:IdentityProviders";

        public string? ClientId { get; set; }

        public string? ClientSecret { get; set; }

        public string? DisplayName { get; set; }

        public string? Type { get; set; }
    }
}
