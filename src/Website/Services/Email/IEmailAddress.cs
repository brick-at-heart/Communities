namespace BrickAtHeart.Communities.Services.Email
{
    public interface IEmailAddress
    {
        /// <summary>
        ///  The email address where a message will be sent
        /// </summary>
        string Address { get; set; }

        /// <summary>
        ///  The display name shown in a mail client
        /// </summary>
        string Name { get; set; }
    }
}