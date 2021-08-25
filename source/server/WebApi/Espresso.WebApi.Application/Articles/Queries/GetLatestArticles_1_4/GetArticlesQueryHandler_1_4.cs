// GetArticlesQueryHandler_1_4.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Common.Extensions;
using Espresso.Domain.Entities;
using Espresso.Domain.Extensions;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_1_4
{
    public class GetArticlesQueryHandler_1_4 :
        IRequestHandler<GetLatestArticlesQuery_1_4, GetLatestArticlesQueryResponse_1_4>
    {
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetArticlesQueryHandler_1_4"/> class.
        /// </summary>
        /// <param name="memoryCache"></param>
        public GetArticlesQueryHandler_1_4(
            IMemoryCache memoryCache
        )
        {
            _memoryCache = memoryCache;
        }

        public Task<GetLatestArticlesQueryResponse_1_4> Handle(
            GetLatestArticlesQuery_1_4 request,
            CancellationToken cancellationToken
        )
        {
            var articles = _memoryCache.Get<IEnumerable<Article>>(
                key: MemoryCacheConstants.ArticleKey
            );

            var firstArticle = articles.FirstOrDefault(
                article => article.Id.Equals(request.FirstArticleId)
            );

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
                    articleCreateDateTime: firstArticle?.CreateDateTime
                )
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(GetLatestArticlesArticle_1_4.GetProjection().Compile());

            var newsPortals = _memoryCache.Get<IEnumerable<NewsPortal>>(
                key: MemoryCacheConstants.NewsPortalKey
            );

            var newsPortalDtos = newsPortals
                .Where(
                    predicate: NewsPortal.GetLatestSugestedNewsPortalsPredicate(
                        newsPortalIds: newsPortalIds,
                        categoryIds: categoryIds,
                        maxAgeOfNewNewsPortal: request.MaxAgeOfNewNewsPortal
                    ).Compile()
                )
                .Select(selector: GetLatestArticlesNewsPortal_1_4.GetProjection().Compile());

            var random = new Random();

            var response = new GetLatestArticlesQueryResponse_1_4
            {
                Articles = articleDtos,
                NewNewsPortals = newsPortalDtos.OrderBy(newsPortal => random.Next()),
                NewNewsPortalsPosition = request.NewNewsPortalsPosition,
            };

            return Task.FromResult(result: response);
        }
    }
}
