using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Services.Email
{
    public class SendGridEmailService : IEmailService
    {
        /// <summary>
        ///  Initializes a new instance of the SendGridEmailService
        /// </summary>
        /// <param name="options">
        ///  Configuration options for SendGrid
        /// </param>
        /// <param name="logger">
        ///  Logs message
        /// </param>
        public SendGridEmailService(IOptions<SendGridOptions> options,
                                     ILogger<SendGridEmailService> logger)
        {
            this.options = options.Value;
            this.logger = logger;
        }

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
        public async Task<bool> SendSingleEmailAsync([NotNull] IEmailAddress sender, [NotNull] IEmailAddress recipient, [NotNull] string subject, [NotNull] string htmlMessage, [NotNull] string plainTextMessage)
        {
            _ = sender ?? throw new ArgumentNullException(nameof(sender));
            _ = recipient ?? throw new ArgumentNullException(nameof(recipient));
            _ = subject ?? throw new ArgumentNullException(nameof(subject));
            _ = htmlMessage ?? throw new ArgumentNullException(nameof(htmlMessage));
            _ = plainTextMessage ?? throw new ArgumentNullException(nameof(plainTextMessage));

            try
            {
                logger.LogInformation($"Preparing to send a single email message from {sender.Address} to {recipient.Address} with a subject of \"{subject}\".");

                SendGrid.Helpers.Mail.EmailAddress from = new(sender.Address, sender.Name);

                SendGrid.Helpers.Mail.EmailAddress to = new(recipient.Address, recipient.Name);

                SendGrid.Helpers.Mail.SendGridMessage msg = SendGrid.Helpers.Mail.MailHelper.CreateSingleEmail(from, to, subject, plainTextMessage, htmlMessage);
                msg.SetOpenTracking(false);

                SendGridClient client = new(options.ApiKey);
                Response response = await client.SendEmailAsync(msg);

                if (response.IsSuccessStatusCode)
                {
                    logger.LogInformation($"Successfully sent a single email message from {sender.Address} to {recipient.Address} with a subject of \"{subject}\".");
                    return true;
                }

                logger.LogError($"Error encountered sending a single email message from {sender.Address} to {recipient.Address} with a subject of \"{subject}\". The status code was {response.StatusCode} with message of \"{response.Body}\".");
                return false;
            }
            catch (Exception ex)
            {
                throw new EmailServiceException(ex);
            }
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
        public async Task<bool> SendSingleEmailWithTemplateAsync(IEmailAddress sender, IEmailAddress recipient, string templateId, object dynamicData)
        {
            _ = sender ?? throw new ArgumentNullException(nameof(sender));
            _ = recipient ?? throw new ArgumentNullException(nameof(recipient));

            try
            {
                logger.LogInformation($"Preparing to send a single email message from {sender.Address} to {recipient.Address} using template ({templateId}).");

                SendGrid.Helpers.Mail.EmailAddress from = new(sender.Address, sender.Name);

                SendGrid.Helpers.Mail.EmailAddress to = new(recipient.Address, recipient.Name);

                SendGrid.Helpers.Mail.SendGridMessage msg = new SendGrid.Helpers.Mail.SendGridMessage();
                msg.AddTo(to);
                msg.From = from;
                msg.TemplateId = templateId;
                msg.SetTemplateData(dynamicData);
                msg.SetOpenTracking(false);

                SendGridClient client = new(options.ApiKey);
                Response response = await client.SendEmailAsync(msg);

                if (response.IsSuccessStatusCode)
                {
                    logger.LogInformation($"Successfully sent a single email message from {sender.Address} to {recipient.Address} using template ({templateId}).");
                    return true;
                }

                logger.LogError($"Error encountered sending a single email message from {sender.Address} to {recipient.Address} using template ({templateId}). The status code was {response.StatusCode} with message of \"{response.Body}\".");
                return false;
            }
            catch (Exception ex)
            {
                throw new EmailServiceException(ex);
            }
        }

        private readonly SendGridOptions options;
        private readonly ILogger<SendGridEmailService> logger;
    }
}