// GetCategoryArticlesQueryHandler.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Common.Extensions;
using Espresso.Domain.Entities;
using Espresso.Domain.Extensions;
using Espresso.Domain.Infrastructure;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles
{
    public class GetCategoryArticlesQueryHandler : IRequestHandler<GetCategoryArticlesQuery, GetCategoryArticlesQueryResponse>
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ISettingProvider _settingProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCategoryArticlesQueryHandler"/> class.
        /// </summary>
        /// <param name="memoryCache"></param>
        /// <param name="settingProvider"></param>
        public GetCategoryArticlesQueryHandler(IMemoryCache memoryCache, ISettingProvider settingProvider)
        {
            _memoryCache = memoryCache;
            _settingProvider = settingProvider;
        }

        public Task<GetCategoryArticlesQueryResponse> Handle(
            GetCategoryArticlesQuery request,
            CancellationToken cancellationToken)
        {
            var newsPortalIds = request.NewsPortalIds
                ?.Replace(" ", string.Empty)
                ?.Split(',')
                ?.Select(newsPortalIdString => int.TryParse(newsPortalIdString, out var newsPortalId) ? newsPortalId : default)
                ?.Where(newsPortalId => newsPortalId != default);

            var newsPortalDtos = GetNewNewsPortals(
                newsPortalIds: newsPortalIds,
                request: request);

            var keyWordsToFilterOut = request.KeyWordsToFilterOut is null ?
                Enumerable.Empty<string>() :
                request.KeyWordsToFilterOut.Split(',', StringSplitOptions.RemoveEmptyEntries);

            var articleDtos = GetArticles(
                request: request,
                newsPortalIds: newsPortalIds,
                keyWordsToFilterOut: keyWordsToFilterOut);

            var response = new GetCategoryArticlesQueryResponse
            {
                Articles = articleDtos,
                NewNewsPortals = newsPortalDtos,
                NewNewsPortalsPosition = _settingProvider.LatestSetting.NewsPortalSetting.NewNewsPortalsPosition,
            };

            return Task.FromResult(result: response);
        }

        private IEnumerable<GetCategoryArticlesNewsPortal> GetNewNewsPortals(
            IEnumerable<int>? newsPortalIds,
            GetCategoryArticlesQuery request)
        {
            if (request.Skip != 0)
            {
                return Array.Empty<GetCategoryArticlesNewsPortal>();
            }

            var newsPortals = _memoryCache.Get<IEnumerable<NewsPortal>>(
                key: MemoryCacheConstants.NewsPortalKey);

            var random = new Random();

            var newsPortalDtos = newsPortals
                .Where(
                    NewsPortal.GetCategorySugestedNewsPortalsPredicate(
                        newsPortalIds: newsPortalIds,
                        categoryId: request.CategoryId,
                        regionId: request.RegionId,
                        maxAgeOfNewNewsPortal: _settingProvider.LatestSetting.NewsPortalSetting.MaxAgeOfNewNewsPortal)
                    .Compile())
                .Select(selector: GetCategoryArticlesNewsPortal.GetProjection().Compile())
                .OrderBy(_ => random.Next());

            return newsPortalDtos;
        }

        private IEnumerable<IEnumerable<GetCategoryArticlesArticle>> GetArticles(
            GetCategoryArticlesQuery request,
            IEnumerable<int>? newsPortalIds,
            IEnumerable<string> keyWordsToFilterOut)
        {
            var articles = _memoryCache.Get<IEnumerable<Article>>(
                key: MemoryCacheConstants.ArticleKey);

            var firstArticle = articles.FirstOrDefault(
                article => article.Id.Equals(request.FirstArticleId));

            var filteredArticles = articles
                .OrderArticlesByPublishDate()
                .FilterArticles(
                    categoryId: request.CategoryId,
                    newsPortalIds: newsPortalIds,
                    titleSearchTerm: request.TitleSearchQuery,
                    articleCreateDateTime: firstArticle?.CreateDateTime)
                .FilterArticlesContainingKeyWords(keyWordsToFilterOut)
                .Skip(request.Skip)
                .Take(request.Take);

            var projection = GetCategoryArticlesArticle.GetProjection().Compile();
            var articleDtos = filteredArticles
                .Select(article => new List<GetCategoryArticlesArticle>()
                    {
                        projection.Invoke(article),
                    }.Union(article.SubordinateArticles.Select(similarArticle => projection.Invoke(similarArticle.SubordinateArticle!))));

            return articleDtos;
        }
    }
}
