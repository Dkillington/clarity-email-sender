using SendGrid;
using SendGrid.Helpers.Mail;

namespace EmailSender
{
    public class SendFunctionality
    {
        private const int maximumRetries = 3;
        public static async Task<Response> Execute(string apiKey, string senderName, string senderEmail, string recipientName, string recipientEmail, string subject, string body)
        {
            // Create SendGrid connection / Pass over email data
            SendGridClient client = new SendGridClient(apiKey);
            var from = new EmailAddress(senderEmail, senderName);
            var to = new EmailAddress(recipientEmail, recipientName);
            var plainTextContent = body;
            var htmlContent = body;
            SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            // Send Email
            Response response = await client.SendEmailAsync(msg);

            // Retry if failed
            if (!ResponseCodeOK())
            {
                for (int i = 0; i < maximumRetries; i++)
                {
                    response = await client.SendEmailAsync(msg);

                    if (ResponseCodeOK())
                    {
                        return response;
                    }
                }
            }

            return response;


            // Check if HTTP Response Code is ok (Email sent)
            bool ResponseCodeOK()
            {
                if(response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
