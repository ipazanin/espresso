using System.Threading;
using System.Threading.Tasks;
using Espresso.Domain.IServices;
using MediatR;

namespace Espresso.Application.CQRS.NewsPortals.Commands.NewSourcesRequest
{
    public class NewSourcesRequestCommandHandler : IRequestHandler<NewsSourcesRequestCommand>
    {
        #region Fields
        private readonly ISlackService _slackService;
        #endregion

        #region Constructors
        public NewSourcesRequestCommandHandler(
            ISlackService slackService
        )
        {
            _slackService = slackService;
        }
        #endregion

        #region Methods
        public async Task<Unit> Handle(
            NewsSourcesRequestCommand request,
            CancellationToken cancellationToken
        )
        {
            await _slackService
                .LogNewNewsPortalRequest(
                    newsPortalName: request.NewsPortalName,
                    email: request.Email,
                    url: request.Url,
                    appEnvironment: request.AppEnvironment,
                    cancellationToken: cancellationToken
                );

            return Unit.Value;
        }
        #endregion
    }
}
