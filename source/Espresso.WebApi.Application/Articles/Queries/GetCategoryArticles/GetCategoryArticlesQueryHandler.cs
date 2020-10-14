﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles
{
    public class GetCategoryArticlesQueryHandler : IRequestHandler<GetCategoryArticlesQuery, GetCategoryArticlesQueryResponse>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        #endregion

        #region Contructors
        public GetCategoryArticlesQueryHandler(
            IMemoryCache memoryCache
        )
        {
            _memoryCache = memoryCache;
        }
        #endregion

        #region Methods
        public Task<GetCategoryArticlesQueryResponse> Handle(
            GetCategoryArticlesQuery request,
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
                ?.Replace(" ", "")
                ?.Split(',')
                ?.Select(newsPortalIdString => int.TryParse(newsPortalIdString, out var newsPortalId) ? newsPortalId : default)
                ?.Where(newsPortalId => newsPortalId != default);

            var articleDtos = articles
                .OrderByDescending(keySelector: Article.GetOrderByDescendingPublishDateExpression().Compile())
                .Where(
                    predicate: Article.GetFilteredArticlesPredicate(
                        categoryId: request.CategoryId,
                        newsPortalIds: newsPortalIds,
                        titleSearchQuery: request.TitleSearchQuery,
                        articleCreateDateTime: firstArticle?.CreateDateTime
                    ).Compile()
                )
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(GetCategoryArticlesArticle.GetProjection().Compile());

            var newsPortals = _memoryCache.Get<IEnumerable<NewsPortal>>(
                key: MemoryCacheConstants.NewsPortalKey
            );

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
                .Select(selector: GetCategoryArticlesNewsPortal.GetProjection().Compile());

            var random = new Random();

            var response = new GetCategoryArticlesQueryResponse
            {
                Articles = articleDtos,
                NewNewsPortals = newsPortalDtos.OrderBy(newsPortal => random.Next()),
                NewNewsPortalsPosition = request.NewNewsPortalsPosition
            };

            return Task.FromResult(result: response);
        }
        #endregion
    }
}