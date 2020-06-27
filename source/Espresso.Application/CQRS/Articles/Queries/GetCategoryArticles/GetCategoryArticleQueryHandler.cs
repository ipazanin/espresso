using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.CQRS.Articles.Queries.Common;
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
                    predicate: article =>
                        article
                            .ArticleCategories
                            .Any(articleCategory => articleCategory.CategoryId.Equals(request.CategoryId)) &&
                        (request.NewsPortalIds is null || request.NewsPortalIds.Contains(article.NewsPortalId))
                )
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(ArticleViewModel.Projection.Compile());

            var response = new GetCategoryArticlesQueryResponse(articleDtos);

            return Task.FromResult(result: response);
        }
        #endregion
    }
}
