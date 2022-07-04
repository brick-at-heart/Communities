using System;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Services.Email
{
    public interface IEmailService
    {
        /// <summary>
        ///  Sends a single email to a single recipient
        /// </summary>
        /// <param name="sender">
        ///  The email address and display name of the sender
        /// </param>
        /// <param name="recipient">
        ///  The email address and display name of the recipient
        /// </param>
        /// <param name="subject">
        ///  THe subject of the email
        /// </param>
        /// <param name="htmlMessage">
        ///  The html version of the email
        /// </param>
        /// <param name="plainTextMessage">
        ///  The plain text version of the email
        /// </param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="EmailServiceException"></exception>
        /// <returns>
        ///  True, if the message sent successfully;
        ///  False, if there was an error
        /// </returns>
        Task<bool> SendSingleEmailAsync(IEmailAddress sender,
                                        IEmailAddress recipient,
                                        string subject,
                                        string htmlMessage,
                                        string plainTextMessage);

        /// <summary>
        ///  Sends a single email to a single recipient based on a template
        /// </summary>
        /// <param name="sender">
        ///  The email address and display name of the sender
        /// </param>
        /// <param name="recipient">
        ///  The email address and display name of the recipient
        /// </param>
        /// <param name="templateId">
        ///  The identifier of the template to use
        /// </param>
        /// <param name="dynamicData">
        ///  The values to use when replacing the variables in the template
        /// </param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="EmailServiceException"></exception>
        /// <returns>
        ///  True, if the message sent successfully;
        ///  False, if there was an error
        /// </returns>
        Task<bool> SendSingleEmailWithTemplateAsync(IEmailAddress sender,
                                                    IEmailAddress recipient,
                                                    string templateId,
                                                    object dynamicData);
    }
}
