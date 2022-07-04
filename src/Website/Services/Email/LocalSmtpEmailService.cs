using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace BrickAtHeart.Communities.Services.Email
{
    public class LocalSmtpEmailService : IEmailService
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
        public Task<bool> SendSingleEmailAsync(IEmailAddress sender,
                                               IEmailAddress recipient,
                                               string subject,
                                               string htmlMessage,
                                               string plainTextMessage)
        {
            MimeMessage message = new MimeMessage();

            message.From.Add(new MailboxAddress(sender.Name, sender.Address));
            message.To.Add(new MailboxAddress(recipient.Name, recipient.Address));
            message.Subject = subject;

            BodyBuilder builder = new BodyBuilder();
            builder.HtmlBody = htmlMessage;
            message.Body = builder.ToMessageBody();

            using (SmtpClient client = new SmtpClient())
            {
                client.Connect("localhost", 25, false);
                client.Send(message);
                client.Disconnect(true);
            }

            return Task.FromResult(true);
        }

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
        public Task<bool> SendSingleEmailWithTemplateAsync(IEmailAddress sender,
                                                           IEmailAddress recipient,
                                                           string templateId,
                                                           object dynamicData)
        {
            throw new NotImplementedException();
        }
    }
}
