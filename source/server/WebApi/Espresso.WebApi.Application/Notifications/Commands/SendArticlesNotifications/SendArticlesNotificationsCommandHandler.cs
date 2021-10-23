// SendArticlesNotificationsCommandHandler.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.WebApi.Application.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Espresso.WebApi.Application.Notifications.Commands.SendArticlesNotifications
{
    public class SendArticlesNotificationsCommandHandler : IRequestHandler<SendArticlesNotificationsCommand>
    {
        public const string LatestArticlesClientMethodName = "GetNewArticles";
        private readonly IHubContext<ArticlesNotificationHub> _hubContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SendArticlesNotificationsCommandHandler"/> class.
        /// </summary>
        /// <param name="hubContext"></param>
        public SendArticlesNotificationsCommandHandler(
            IHubContext<ArticlesNotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task<Unit> Handle(SendArticlesNotificationsCommand request, CancellationToken cancellationToken)
        {
            if (!request.CreatedArticles.Any())
            {
                return Unit.Value;
            }

            var newArticles = request
                .CreatedArticles
                .Select(article => new NewArticleDto(
                    article.NewsPortalId,
                    article.ArticleCategories.Select(articleCategory => articleCategory.CategoryId)));

            var newArticlesNotificationDto = new NewArticlesNotificationDto(newArticles);

            await _hubContext.Clients.All.SendAsync(
                method: LatestArticlesClientMethodName,
                arg1: newArticlesNotificationDto,
                cancellationToken: cancellationToken);

            return Unit.Value;
        }
    }
}
