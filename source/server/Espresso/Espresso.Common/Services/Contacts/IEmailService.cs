using System.Threading.Tasks;

namespace Espresso.Common.Services.Contracts
{
    public interface IEmailService
    {
        public Task<bool> SendMail(
            string to,
            string subject,
            string content,
            string htmlContent
        );
    }
}
