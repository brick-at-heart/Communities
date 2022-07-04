namespace BrickAtHeart.Communities.Services.Email
{
    public class SendGridOptions
    {
        /// <summary>
        ///  The section in the configuration where the options are read
        /// </summary>
        public const string Section = "SendGrid";

        /// <summary>
        ///  The SendGrid API Key
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        ///  The base Uri of the SendGrid Api
        /// </summary>
        public string BaseUri { get; set; }
    }
}
