using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.CQRS.Articles.Queries.Common;
using Espresso.Application.CQRS.NewsPortals.Queries.GetNewsPortals;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.Application.CQRS.Articles.Queries.GetCategoryArticles
{
    public class GetCategoryArticleQueryHandler : IRequestHandler<GetCategoryArticlesQuery, GetCategoryArticlesQueryResponse>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        #endregion

        #region Contructors
        public GetCategoryArticleQueryHandler(
            IMemoryCache memoryCache
        )
        {
            _memoryCache = memoryCache;
        }
        #endregion

        #region Methods
        public Task<GetCategoryArticlesQueryResponse> Handle(GetCategoryArticlesQuery request, CancellationToken cancellationToken)
        {
            var articles = _memoryCache.Get<IEnumerable<Article>>(
                key: MemoryCacheConstants.ArticleKey
            );

            var articleDtos = articles
                .OrderByDescending(article => article.PublishDateTime)
                .Where(
                    predicate: Article.GetCategoryArticleExpression(
                        categoryId: request.CategoryId,
                        newsPortalIds: request.NewsPortalIds
                    ).
                    Compile()
                )
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(ArticleViewModel.Projection.Compile());

            var newsPortals = _memoryCache.Get<IEnumerable<NewsPortal>>(key: MemoryCacheConstants.NewsPortalKey);
            var newsPortalDtos = newsPortals
                .Where(
                    NewsPortal.GetIsNewExpression(
                        newsPortalIds: request.NewsPortalIds,
                        categoryIds: new List<int> { request.CategoryId }
                    )
                    .Compile()
                )
                .OrderBy(keySelector: newsPortal => newsPortal.Name)
                .Select(selector: NewsPortalViewModel.Projection.Compile());

            var response = new GetCategoryArticlesQueryResponse(
                articles: articleDtos,
                newNewsPortals: newsPortalDtos,
                newNewsPortalsPosition: DefaultValueConstants.NewNewsPortalsPosition
            );

            return Task.FromResult(result: response);
        }
        #endregion
    }
}
