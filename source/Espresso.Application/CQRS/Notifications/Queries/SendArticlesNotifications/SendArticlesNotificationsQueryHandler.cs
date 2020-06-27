using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Espresso.Application.CQRS.Notifications.Queries.SendArticlesNotifications
{
    public class SendArticlesNotificationsQueryHandler : IRequestHandler<SendArticlesNotificationsQuery>
    {
        #region Constants
        public const string LatestArticlesClientMethodName = "GetNewArticles";
        #endregion

        #region Fields
        private readonly IHubContext<ArticlesNotificationHub> _hubContext;
        #endregion

        #region Constructors
        public SendArticlesNotificationsQueryHandler(
            IHubContext<ArticlesNotificationHub> hubContext
        )
        {
            _hubContext = hubContext;
        }
        #endregion

        #region Methods
        public async Task<Unit> Handle(SendArticlesNotificationsQuery request, CancellationToken cancellationToken)
        {
            if (request.CreatedArticles.Count() == 0)
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
            ).ConfigureAwait(false);

            return Unit.Value;
        }
        #endregion
    }
}
