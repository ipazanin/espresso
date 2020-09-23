using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.WebApi.Application.Utilities;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;

using MediatR;

using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.Articles.Commands.CalculateTrendingScore
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

            foreach (var article in articles)
            {
                article.UpdateTrendingScore(
                    trendingScore: trendingScoreUtility.CalculateTrendingScore(
                        clicks: article.NumberOfClicks,
                        publishDateTime: article.PublishDateTime
                    )
                );
            }

            _ = _memoryCache.Set(MemoryCacheConstants.ArticleKey, articles.ToList());

            return Unit.Task;
        }
        #endregion
    }
}
