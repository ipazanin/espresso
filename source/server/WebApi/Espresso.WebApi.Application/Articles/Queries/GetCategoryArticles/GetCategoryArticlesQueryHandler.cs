// GetCategoryArticlesQueryHandler.cs
//
// © 2021 Espresso News. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Common.Extensions;
using Espresso.Domain.Entities;
using Espresso.Domain.Extensions;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles
{
    public class GetCategoryArticlesQueryHandler : IRequestHandler<GetCategoryArticlesQuery, GetCategoryArticlesQueryResponse>
    {
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCategoryArticlesQueryHandler"/> class.
        /// </summary>
        /// <param name="memoryCache"></param>
        public GetCategoryArticlesQueryHandler(
            IMemoryCache memoryCache
        )
        {
            _memoryCache = memoryCache;
        }

        public Task<GetCategoryArticlesQueryResponse> Handle(
            GetCategoryArticlesQuery request,
            CancellationToken cancellationToken
        )
        {
            var newsPortalIds = request.NewsPortalIds
                ?.Replace(" ", string.Empty)
                ?.Split(',')
                ?.Select(newsPortalIdString => int.TryParse(newsPortalIdString, out var newsPortalId) ? newsPortalId : default)
                ?.Where(newsPortalId => newsPortalId != default);

            var newsPortalDtos = GetNewNewsPortals(
                newsPortalIds: newsPortalIds,
                request: request
            );

            var articleDtos = GetArticles(
                request: request,
                newsPortalIds: newsPortalIds
            );

            var response = new GetCategoryArticlesQueryResponse
            {
                Articles = articleDtos,
                NewNewsPortals = newsPortalDtos,
                NewNewsPortalsPosition = request.NewNewsPortalsPosition,
            };

            return Task.FromResult(result: response);
        }

        private IEnumerable<GetCategoryArticlesNewsPortal> GetNewNewsPortals(
            IEnumerable<int>? newsPortalIds,
            GetCategoryArticlesQuery request
        )
        {
            if (request.Skip != 0)
            {
                return Array.Empty<GetCategoryArticlesNewsPortal>();
            }

            var newsPortals = _memoryCache.Get<IEnumerable<NewsPortal>>(
                key: MemoryCacheConstants.NewsPortalKey
            );

            var random = new Random();

            var newsPortalDtos = newsPortals
                .Where(
                    NewsPortal.GetCategorySugestedNewsPortalsPredicate(
                        newsPortalIds: newsPortalIds,
                        categoryId: request.CategoryId,
                        regionId: request.RegionId,
                        maxAgeOfNewNewsPortal: request.MaxAgeOfNewNewsPortal
                    )
                    .Compile()
                )
                .Select(selector: GetCategoryArticlesNewsPortal.GetProjection().Compile())
                .OrderBy(newsPortal => random.Next());

            return newsPortalDtos;
        }

        private IEnumerable<IEnumerable<GetCategoryArticlesArticle>> GetArticles(
            GetCategoryArticlesQuery request,
            IEnumerable<int>? newsPortalIds
        )
        {
            var articles = _memoryCache.Get<IEnumerable<Article>>(
                key: MemoryCacheConstants.ArticleKey
            );

            var firstArticle = articles.FirstOrDefault(
                article => article.Id.Equals(request.FirstArticleId)
            );

            var filteredArticles = articles
                .OrderArticlesByPublishDate()
                .FilterArticles(
                    categoryId: request.CategoryId,
                    newsPortalIds: newsPortalIds,
                    titleSearchTerm: request.TitleSearchQuery,
                    articleCreateDateTime: firstArticle?.CreateDateTime
                )
                .Skip(request.Skip)
                .Take(request.Take);

            var projection = GetCategoryArticlesArticle.GetProjection().Compile();
            var articleDtos = filteredArticles
                .Select(article => new List<GetCategoryArticlesArticle>()
                    {
                        projection.Invoke(article),
                    }.Union(article.SubordinateArticles.Select(similarArticle => projection.Invoke(similarArticle.SubordinateArticle!)))
                );

            return articleDtos;
        }
    }
}
