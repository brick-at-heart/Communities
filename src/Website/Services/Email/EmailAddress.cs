namespace BrickAtHeart.Communities.Services.Email
{
    public class EmailAddress : IEmailAddress
    {
        /// <summary>
        ///  The email address where a message will be sent
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        ///  The display name shown in a mail client
        /// </summary>
        public string Name { get; set; }

        public EmailAddress()
        {
            Address = string.Empty;
            Name = string.Empty;
        }
    }
}
