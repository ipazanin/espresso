using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Espresso.Application.Utility;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;

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
                id: article.Id,
                articleId: article.ArticleId,
                url: article.Url,
                summary: article.Summary,
                title: article.Title,
                imageUrl: article.ImageUrl,
                createDateTime: article.CreateDateTime,
                updateDateTime: article.UpdateDateTime,
                publishDateTime: article.PublishDateTime,
                numberOfClicks: article.NumberOfClicks,
                trendingScore: trendingScoreUtility.CalculateTrendingScore(article.NumberOfClicks, article.PublishDateTime),
                isHidden: article.IsHidden,
                newsPortalId: article.NewsPortalId,
                rssFeedId: article.RssFeedId,
                articleCategories: article.ArticleCategories,
                newsPortal: article.NewsPortal,
                rssFeed: null
            )).ToList();

            _ = _memoryCache.Set(MemoryCacheConstants.ArticleKey, articlesWithUpdatedTrendingScore);

            return Unit.Task;
        }
        #endregion
    }
}
