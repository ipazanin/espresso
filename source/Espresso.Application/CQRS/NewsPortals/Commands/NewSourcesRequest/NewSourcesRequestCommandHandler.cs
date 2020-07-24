using System.Threading;
using System.Threading.Tasks;
using Espresso.Domain.IServices;
using MediatR;

namespace Espresso.Application.CQRS.NewsPortals.Commands.NewSourcesRequest
{
    public class NewSourcesRequestCommandHandler : IRequestHandler<NewsSourcesRequestCommand>
    {
        private readonly ILoggerService _loggerService;

        public NewSourcesRequestCommandHandler(
            ILoggerService loggerService
        )
        {
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle(
            NewsSourcesRequestCommand request,
            CancellationToken cancellationToken
        )
        {
            await _loggerService.LogNewNewsPortalRequest(
                newsPortalName: request.NewsPortalName,
                email: request.Email,
                url: request.Url,
                cancellationToken: cancellationToken
            );

            return Unit.Value;
        }
    }
}
