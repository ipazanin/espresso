// GetTrendingArticlesQueryHandler.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Common.Extensions;
using Espresso.Domain.Entities;
using Espresso.Domain.Extensions;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Espresso.WebApi.Application.Articles.Queries.GetTrendingArticles
{
    public class GetTrendingArticlesQueryHandler : IRequestHandler<GetTrendingArticlesQuery, GetTrendingArticlesQueryResponse>
    {
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTrendingArticlesQueryHandler"/> class.
        /// </summary>
        /// <param name="memoryCache"></param>
        public GetTrendingArticlesQueryHandler(
            IMemoryCache memoryCache
        )
        {
            _memoryCache = memoryCache;
        }

        public Task<GetTrendingArticlesQueryResponse> Handle(GetTrendingArticlesQuery request, CancellationToken cancellationToken)
        {
            var articles = _memoryCache.Get<IEnumerable<Article>>(
                key: MemoryCacheConstants.ArticleKey
            );

            var firstArticle = articles.FirstOrDefault(
                article => article.Id.Equals(request.FirstArticleId)
            );

            var articleDtos = articles
                .FilterTrendingArticles(
                    maxAgeOfTrendingArticle: request.MaxAgeOfTrendingArticle,
                    articleCreateDateTime: firstArticle?.CreateDateTime
                )
                .OrderArticlesByTrendingScore()
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(GetTrendingArticlesArticle.GetProjection().Compile());

            var response = new GetTrendingArticlesQueryResponse
            {
                Articles = articleDtos,
            };

            return Task.FromResult(result: response);
        }
    }
}
