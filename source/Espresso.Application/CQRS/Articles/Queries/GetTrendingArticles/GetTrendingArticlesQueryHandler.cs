using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Configuration;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.Application.CQRS.Articles.Queries.GetTrendingArticles
{
    public class GetTrendingArticlesQueryHandler : IRequestHandler<GetTrendingArticlesQuery, GetTrendingArticlesQueryResponse>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        #endregion

        #region Constructors
        public GetTrendingArticlesQueryHandler(
            IMemoryCache memoryCache
        )
        {
            _memoryCache = memoryCache;
        }
        #endregion

        #region Methods
        public Task<GetTrendingArticlesQueryResponse> Handle(GetTrendingArticlesQuery request, CancellationToken cancellationToken)
        {
            var maxTrendingDateTime = DateTime.UtcNow - DateTimeConstants.MaxAgeOfTrendingArticle;

            var articles = _memoryCache.Get<IEnumerable<Article>>(
                key: MemoryCacheConstants.ArticleKey
            );

            var articleDtos = articles
                .Where(predicate: article => article.PublishDateTime > maxTrendingDateTime)
                .OrderByDescending(article => article.TrendingScore)
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(ArticleTrendingViewModel.Projection.Compile());

            var response = new GetTrendingArticlesQueryResponse(articleDtos);

            return Task.FromResult(result: response);
        }
        #endregion
    }
}
