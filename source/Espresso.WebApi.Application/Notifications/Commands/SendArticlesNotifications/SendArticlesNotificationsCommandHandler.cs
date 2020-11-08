using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Persistence.Database;
using Espresso.WebApi.Application.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Espresso.WebApi.Application.Notifications.Commands.SendArticlesNotifications
{
    public class SendArticlesNotificationsCommandHandler : IRequestHandler<SendArticlesNotificationsCommand>
    {
        #region Constants
        public const string LatestArticlesClientMethodName = "GetNewArticles";
        #endregion

        #region Fields
        private readonly IHubContext<ArticlesNotificationHub> _hubContext;
        private readonly IApplicationDatabaseContext _applicationDatabaseContext;
        #endregion

        #region Constructors
        public SendArticlesNotificationsCommandHandler(
            IHubContext<ArticlesNotificationHub> hubContext,
            IApplicationDatabaseContext applicationDatabaseContext
        )
        {
            _hubContext = hubContext;
            _applicationDatabaseContext = applicationDatabaseContext;
        }
        #endregion

        #region Methods
        public async Task<Unit> Handle(SendArticlesNotificationsCommand request, CancellationToken cancellationToken)
        {
            if (!request.CreatedArticleIds.Any())
            {
                return Unit.Value;
            }

            var createdArticles = await _applicationDatabaseContext
                .Articles
                .Include(article => article.ArticleCategories)
                .ThenInclude(articleCategory => articleCategory.Category)
                .Include(article => article.NewsPortal)
                .Where(article => request.CreatedArticleIds.Contains(article.Id))
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var newArticles = createdArticles.Select(article => new NewArticleDto(
                    article.NewsPortal!.Id,
                    article.ArticleCategories.Select(articleCategory => articleCategory.CategoryId)
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
