namespace BrickAtHeart.Communities.Data
{
    public class SqlServerDataClientOptions
    {
        public const string Section = "BrickAtHeart:Communities:Data";

        public string? ConnectionString { get; set; }
    }
}