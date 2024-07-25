using System.Net.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace EmailSender
{
    public class SendFunctionality
    {
        public static async Task Execute(string apiKey, string senderEmail, string recipientEmail, string subject, string body)
        {
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(senderEmail, "David Killian");
            var to = new EmailAddress(recipientEmail, "Test User");
            var plainTextContent = body;
            var htmlContent = body;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
