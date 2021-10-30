using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using SecretSanta.Models;
using System;
using System.Threading.Tasks;

namespace SecretSanta.Services
{
    public class Mailer : IMailer
    {
        private readonly SmtpSettings smtpSettings;
        private SantaMessage messageDetails;

        public Mailer(IOptions<SmtpSettings> options)
        {
            smtpSettings = options.Value;
        }

        public async Task SendEmailAsync(SantaMessage message)
        {
            messageDetails = message;
            try
            {
                var mimeMessage = messageDetails.GetMimeMessage();
                mimeMessage.From.Add(new MailboxAddress(smtpSettings.SenderName, smtpSettings.SenderEmail));
                using var client = new SmtpClient
                {
                    ServerCertificateValidationCallback = (s, c, h, e) => true
                };

                await client.ConnectAsync(smtpSettings.Server, smtpSettings.Port);
                await client.AuthenticateAsync(smtpSettings.SenderEmail, smtpSettings.Password);
                await client.SendAsync(mimeMessage);
                await client.DisconnectAsync(true);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }
    }
}