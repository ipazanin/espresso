using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.Application.CQRS.Articles.Queries.GetLatestArticles
{
    public class GetArticlesQueryHandler : IRequestHandler<GetLatestArticlesQuery, GetLatestArticlesQueryResponse>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        #endregion

        #region Constructors
        public GetArticlesQueryHandler(
            IMemoryCache memoryCache
        )
        {
            _memoryCache = memoryCache;
        }
        #endregion

        #region Methods
        public Task<GetLatestArticlesQueryResponse> Handle(
            GetLatestArticlesQuery request,
            CancellationToken cancellationToken
        )
        {
            var articles = _memoryCache.Get<IEnumerable<Article>>(
                key: MemoryCacheConstants.ArticleKey
            );

            var firstArticle = articles.FirstOrDefault(
                article => article.Id.Equals(request.FirstArticleId)
            );

            var articleDtos = articles
                .OrderByDescending(keySelector: Article.GetOrderByDescendingPublishDateExpression().Compile())
                .Where(
                    predicate: Article.GetFilteredArticlesPredicate(
                        categoryIds: request.CategoryIds,
                        newsPortalIds: request.NewsPortalIds,
                        titleSearchQuery: request.TitleSearchQuery,
                        articleCreateDateTime: firstArticle?.CreateDateTime
                    ).Compile()
                )
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(GetLatestArticlesArticle.GetProjection().Compile());

            var newsPortals = _memoryCache.Get<IEnumerable<NewsPortal>>(
                key: MemoryCacheConstants.NewsPortalKey
            );

            var newsPortalDtos = newsPortals
                .Where(
                    predicate: NewsPortal.GetLatestSugestedNewsPortalsPredicate(
                        newsPortalIds: request.NewsPortalIds,
                        categoryIds: request.CategoryIds,
                        maxAgeOfNewNewsPortal: request.MaxAgeOfNewNewsPortal
                    ).Compile()
                )
                .OrderBy(keySelector: NewsPortal.GetOrderByExpression().Compile())
                .Select(selector: GetLatestArticlesNewsPortal.GetProjection().Compile());

            var response = new GetLatestArticlesQueryResponse(
                articles: articleDtos,
                newNewsPortals: newsPortalDtos,
                newNewsPortalsPosition: request.NewNewsPortalsPosition
            );

            return Task.FromResult(result: response);
        }
        #endregion
    }
}
