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

            var articleDtos = articles
                .OrderByDescending(keySelector: Article.GetOrderByDescendingTrendingScoreExpression().Compile())
                .Where(
                    predicate: Article.GetFilteredArticlesPredicate(
                        categoryIds: request.CategoryIds,
                        newsPortalIds: request.NewsPortalIds,
                        titleSearchQuery: null
                    ).Compile()
                )
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(GetFeaturedArticlesArticle.GetProjection().Compile());

            var response = new GetFeaturedArticlesQueryResponse(
                articles: articleDtos
            );

            return Task.FromResult(result: response);
        }
        #endregion
    }
}
