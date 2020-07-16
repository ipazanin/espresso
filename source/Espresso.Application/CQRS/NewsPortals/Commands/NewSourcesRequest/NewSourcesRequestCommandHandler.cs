using System.Threading;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MediatR;
using MimeKit;

namespace Espresso.Application.CQRS.NewsPortals.Commands.NewSourcesRequest
{
    public class NewSourcesRequestCommandHandler : IRequestHandler<NewsSourcesRequestCommand>
    {

        public NewSourcesRequestCommandHandler()
        {
        }

        public async Task<Unit> Handle(
            NewsSourcesRequestCommand request,
            CancellationToken cancellationToken
        )
        {


            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Ivan", "ivan.pazanin1996@gmail.com"));
            message.To.Add(new MailboxAddress("Ivan", "iferencak@profico.hr"));
            message.Subject = "Hajduk 1950";
            message.Body = new TextPart("plain")
            {
                Text = "Test123"
            };

            using var client = new SmtpClient();

            await client.ConnectAsync(
                host: "smtp.gmail.com",
                port: 587,
                useSsl: false,
                cancellationToken: cancellationToken
            );
            client.Authenticate(
                userName: "ivan.pazanin1996@gmail.com",
                password: "Jh72078N"
            );

            await client.SendAsync(
                message: message,
                cancellationToken: cancellationToken
            );

            await client.DisconnectAsync(
                quit: true,
                cancellationToken: cancellationToken
            );

            return Unit.Value;
        }
    }
}
