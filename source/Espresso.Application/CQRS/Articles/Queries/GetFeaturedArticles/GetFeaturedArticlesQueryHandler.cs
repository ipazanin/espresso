using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.Application.CQRS.Articles.Queries.GetFeaturedArticles
{
    public class GetFeaturedArticlesQueryHandler :
        IRequestHandler<GetFeaturedArticlesQuery, GetFeaturedArticlesQueryResponse>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        #endregion

        #region Constructors
        public GetFeaturedArticlesQueryHandler(
            IMemoryCache memoryCache
        )
        {
            _memoryCache = memoryCache;
        }
        #endregion

        #region Methods
        public Task<GetFeaturedArticlesQueryResponse> Handle(GetFeaturedArticlesQuery request, CancellationToken cancellationToken)
        {
            var articles = _memoryCache.Get<IEnumerable<Article>>(
                key: MemoryCacheConstants.ArticleKey
            );

            var featuredArticleDtos = articles
                .Where(
                    predicate: Article.GetFilteredFeaturedArticlesPredicate(
                        categoryIds: request.CategoryIds,
                        newsPortalIds: request.NewsPortalIds,
                        titleSearchQuery: null,
                        maxAgeOfFeaturedArticle: request.MaxAgeOfFeaturedArticle
                    ).Compile()
                )
                .OrderByDescending(
                    keySelector: Article
                        .GetOrderByDescendingTrendingScoreExpression()
                        .Compile()
                )
                .ThenByDescending(
                    keySelector: Article
                        .GetOrderByDescendingTrendingScoreExpression()
                        .Compile()
                )
                .Select(GetFeaturedArticlesArticle.GetProjection().Compile());

            var trendingArticleDtos = articles
                .Where(
                    predicate: Article.GetTrendingArticlePredicate(
                        maxAgeOfTrendingArticle: request.MaxAgeOfTrendingArticle
                    )
                    .Compile()
                )
                .OrderByDescending(
                    keySelector: Article
                        .GetOrderByDescendingTrendingScoreExpression()
                        .Compile()
                    )
                .Select(GetFeaturedArticlesArticle.GetProjection().Compile());

            var articleDtos = featuredArticleDtos
                .Union(trendingArticleDtos)
                .Skip(request.Skip)
                .Take(request.Take);

            var response = new GetFeaturedArticlesQueryResponse(
                articles: articleDtos
            );

            return Task.FromResult(result: response);
        }
        #endregion
    }
}
