// GetLatestArticlesQueryHandler_1_3.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Common.Extensions;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;
using Espresso.Domain.Extensions;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_1_3
{
#pragma warning disable S101 // Types should be named in PascalCase
    public class GetLatestArticlesQueryHandler_1_3 : IRequestHandler<GetLatestArticlesQuery_1_3, GetLatestArticlesQueryResponse_1_3>
#pragma warning restore S101 // Types should be named in PascalCase
    {
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetLatestArticlesQueryHandler_1_3"/> class.
        /// </summary>
        /// <param name="memoryCache"></param>
        public GetLatestArticlesQueryHandler_1_3(
            IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Task<GetLatestArticlesQueryResponse_1_3> Handle(
            GetLatestArticlesQuery_1_3 request,
            CancellationToken cancellationToken)
        {
            var articles = _memoryCache.Get<IEnumerable<Article>>(
                key: MemoryCacheConstants.ArticleKey);

            var newsPortalIds = request.NewsPortalIds
                ?.Replace(" ", string.Empty)
                ?.Split(',')
                ?.Select(newsPortalIdString => int.TryParse(newsPortalIdString, out var newsPortalId) ? newsPortalId : default)
                ?.Where(newsPortalId => newsPortalId != default);

            var categoryIds = request.CategoryIds
                ?.Replace(" ", string.Empty)
                ?.Split(',')
                ?.Select(categoryIdString => int.TryParse(categoryIdString, out var categoryId) ? categoryId : default)
                ?.Where(categoryId => categoryId != default);

            var articleDtos = articles
                .OrderArticlesByPublishDate()
                .FilterArticles_2_0(
                    categoryIds: categoryIds,
                    newsPortalIds: newsPortalIds,
                    titleSearchTerm: request.TitleSearchQuery,
                    articleCreateDateTime: null)
                .Where(
                    article => !article.ArticleCategories
                        .Any(articleCategory => articleCategory.CategoryId.Equals((int)CategoryId.Local)))
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(GetLatestArticlesArticle_1_3.GetProjection().Compile());

            var response = new GetLatestArticlesQueryResponse_1_3
            {
                Articles = articleDtos,
            };

            return Task.FromResult(result: response);
        }
    }
}
