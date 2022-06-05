namespace BrickAtHeart.Communities.Services.Slack
{
    public class SlackOptions
    {
        /// <summary>
        ///  The section in the configuration where the options are read
        /// </summary>
        public const string Section = "Slack";

        public string BotToken { get; set; }

        public SlackOptions()
        {
            BotToken = string.Empty;
        }
    }
}