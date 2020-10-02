using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.Articles.Queries.GetTrendingArticles
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

            var articles = _memoryCache.Get<IEnumerable<Article>>(
                key: MemoryCacheConstants.ArticleKey
            );

            var firstArticle = articles.FirstOrDefault(
                article => article.Id.Equals(request.FirstArticleId)
            );

            var articleDtos = articles
                .Where(
                    predicate: Article.GetTrendingArticlePredicate(
                        maxAgeOfTrendingArticle: request.MaxAgeOfTrendingArticle,
                        articleCreateDateTime: firstArticle?.CreateDateTime
                    )
                    .Compile()
                )
                .OrderByDescending(keySelector: Article.GetOrderByDescendingTrendingScoreExpression().Compile())
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(GetTrendingArticlesArticle.GetProjection().Compile());

            var response = new GetTrendingArticlesQueryResponse
            {
                Articles = articleDtos
            };

            return Task.FromResult(result: response);
        }
        #endregion
    }
}
