using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.WebApi.Application.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Espresso.WebApi.Application.CQRS.Notifications.Commands.SendArticlesNotifications
{
    public class SendArticlesNotificationsCommandHandler : IRequestHandler<SendArticlesNotificationsCommand>
    {
        #region Constants
        public const string LatestArticlesClientMethodName = "GetNewArticles";
        #endregion

        #region Fields
        private readonly IHubContext<ArticlesNotificationHub> _hubContext;
        #endregion

        #region Constructors
        public SendArticlesNotificationsCommandHandler(
            IHubContext<ArticlesNotificationHub> hubContext
        )
        {
            _hubContext = hubContext;
        }
        #endregion

        #region Methods
        public async Task<Unit> Handle(SendArticlesNotificationsCommand request, CancellationToken cancellationToken)
        {
            if (!request.CreatedArticles.Any())
            {
                return Unit.Value;
            }

            var newArticles = request.CreatedArticles.Select(article => new NewArticleDto(
                article.NewsPortal.Id,
                article.Categories.Select(category => category.Id)
            ));

            var newArticlesNotificationDto = new NewArticlesNotificationDto(newArticles);

            await _hubContext.Clients.All.SendAsync(
                method: LatestArticlesClientMethodName,
                arg1: newArticlesNotificationDto,
                cancellationToken: cancellationToken
            );

            return Unit.Value;
        }
        #endregion
    }
}
