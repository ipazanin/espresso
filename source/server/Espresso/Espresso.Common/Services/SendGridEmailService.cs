using System.Threading.Tasks;
using Espresso.Common.IServices;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Espresso.Common.Services
{
    public class SendGridEmailService : IEmailService
    {
        private const string SenderEmail = "dashboard-noreply@espressonews.co";
        private const string SenderName = "Dashboard NoReply";

        private readonly string _sendGridKey;

        public SendGridEmailService(
            string sendGridKey
        )
        {
            _sendGridKey = sendGridKey;
        }

        public async Task<bool> SendMail(
            string to,
            string subject,
            string content,
            string htmlContent
        )
        {
            var client = new SendGridClient(_sendGridKey);
            var fromEmail = new EmailAddress(SenderEmail, SenderName);
            var toEmail = new EmailAddress(to);

            var message = MailHelper.CreateSingleEmail(
                from: fromEmail,
                to: toEmail,
                subject: subject,
                plainTextContent: content,
                htmlContent: htmlContent
            );

            var response = await client.SendEmailAsync(message);

            return response.IsSuccessStatusCode;
        }
    }
}
