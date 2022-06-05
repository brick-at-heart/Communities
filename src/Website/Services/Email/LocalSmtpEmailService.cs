using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace BrickAtHeart.Communities.Services.Email
{
    public class LocalSmtpEmailService : IEmailService
    {
        public Task<bool> SendSingleEmailAsync(IEmailAddress sender, IEmailAddress recipient, string subject, string htmlMessage, string plainTextMessage)
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

        public Task<bool> SendSingleEmailWithTemplateAsync(IEmailAddress sender, IEmailAddress recipient, string templateId, object dynamicData)
        {
            throw new NotImplementedException();
        }
    }
}