using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Espresso.Application.Utility;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Persistence.Database;

using MediatR;

using Microsoft.Extensions.Caching.Memory;

namespace Espresso.Application.CQRS.Articles.Commands.CalculateTrendingScore
{
    public class CalculateTrendingScoreCommandHandler : IRequestHandler<CalculateTrendingScoreCommand>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        #endregion

        #region Constructors
        public CalculateTrendingScoreCommandHandler(
            IMemoryCache memoryCache
        )
        {
            _memoryCache = memoryCache;
        }
        #endregion

        #region Methods
        public Task<Unit> Handle(CalculateTrendingScoreCommand request, CancellationToken cancellationToken)
        {
            var articles = _memoryCache.Get<IEnumerable<Article>>(
                key: MemoryCacheConstants.ArticleKey
            );

            var clicksPerArticle = articles.Select(articleKey => articleKey.NumberOfClicks);
            var trendingScoreUtility = new TrendingScoreUtility(clicksPerArticle);

            var articlesWithUpdatedTrendingScore = articles.Select(article => new Article(
                article.Id,
                article.ArticleId,
                article.Url,
                article.Summary,
                article.Title,
                article.ImageUrl,
                article.CreateDateTime,
                article.UpdateDateTime,
                article.PublishDateTime,
                article.NumberOfClicks,
                trendingScoreUtility.CalculateTrendingScore(article.NumberOfClicks, article.PublishDateTime),
                article.NewsPortalId,
                article.RssFeedId,
                article.ArticleCategories,
                article.NewsPortal,
                rssFeed: null
            )).ToList();

            _memoryCache.Set(MemoryCacheConstants.ArticleKey, articlesWithUpdatedTrendingScore);

            return Unit.Task;
        }
        #endregion
    }
}
