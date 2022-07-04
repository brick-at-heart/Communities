namespace BrickAtHeart.Communities.Models
{
    public class SystemSettings
    {
        public const string Section = "Communities";

        public string SystemEmail { get; set; }

        public string SystemName { get; set;}

        public SystemSettings()
        {
            SystemEmail = "communities@brickatheart.com";
            SystemName = "Communities By Brick@Heart";
        }
    }
}
