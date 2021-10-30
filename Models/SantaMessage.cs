using MimeKit;
using MimeKit.Utils;

namespace SecretSanta.Models
{
    public class SantaMessage
    {
        public SantaMessage(Participant gifter, Participant recipient, string bodyTemplate, string minPrice, string mailingDate)
        {
            Gifter = gifter;
            Recipient = recipient;
            BodyTemplate = bodyTemplate;
            MinPrice = minPrice;
            MailingDate = mailingDate;
        }

        public Participant Recipient { get; }
        public Participant Gifter { get; }
        public string MinPrice { get; }
        public string MailingDate { get; }
        public string GifterEmail => Gifter.Email;
        public string Subject => ComposeSubject();
        public string BodyTemplate { get; }

        public MimeMessage GetMimeMessage()
        {
            var message = new MimeMessage();

            message.To.Add(new MailboxAddress(Gifter.Name, Gifter.Email));
            message.Subject = Subject;

            var builder = new BodyBuilder();

            builder.TextBody = ComposeTextBody();

            var img = builder.LinkedResources.Add(@".\wwwroot\images\drunk_santa.jpg");
            img.ContentId = MimeUtils.GenerateMessageId();
            builder.HtmlBody = ComposeHtmlBody(img.ContentId);  //TODO: Use GUID?

            message.Body = builder.ToMessageBody();

            return message;
        }

        private string ComposeSubject()
        {
            return $"Your Secret Santa Pick is {Recipient.Name}";
        }

        private string ComposeHtmlBody(string contentId)
        {
            string body = BodyTemplate;

            body = body.Replace("{Gifter}", Gifter.Name);
            body = body.Replace("{Recipient}", Recipient.Name);
            body = body.Replace("{RecipientAddress}", Recipient.GetAddress());
            body = body.Replace("{Minimum}", MinPrice);
            body = body.Replace("{MailingDate}", MailingDate);
            body = body.Replace("{ContentID}", contentId);

            return body;
        }

        private string ComposeTextBody()
        {
            string body = $"Ok {Gifter.Name}, here's your Secret Santa Pick. \r\n You got {Recipient.Name}. Their address is: \r\n {Recipient.GetAddress()} \r\n Remember, the minimum spending is ${MinPrice} and there is no maximum. All gifts should be mailed by {MailingDate}. The goal is to have everyone get their gifts within the same week. So take into account shipping that mail-order midget tranny eskimo hooker.";

            return body;
        }
    }
}