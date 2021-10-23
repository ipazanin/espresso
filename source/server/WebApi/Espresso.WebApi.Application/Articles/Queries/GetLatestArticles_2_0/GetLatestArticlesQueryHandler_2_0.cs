// GetLatestArticlesQueryHandler_2_0.cs
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

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_2_0
{
    public class GetArticlesQueryHandler_2_0 : IRequestHandler<GetLatestArticlesQuery_2_0, GetLatestArticlesQueryResponse_2_0>
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ISettingProvider _settingProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetArticlesQueryHandler_2_0"/> class.
        /// </summary>
        /// <param name="memoryCache"></param>
        /// <param name="settingProvider"></param>
        public GetArticlesQueryHandler_2_0(IMemoryCache memoryCache, ISettingProvider settingProvider)
        {
            _memoryCache = memoryCache;
            _settingProvider = settingProvider;
        }

        public Task<GetLatestArticlesQueryResponse_2_0> Handle(
            GetLatestArticlesQuery_2_0 request,
            CancellationToken cancellationToken)
        {
            var (newsPortalIds, categoryIds) = ParseIds(request);

            var savedArticles = _memoryCache.Get<IEnumerable<Article>>(
                key: MemoryCacheConstants.ArticleKey);

            var firstArticle = savedArticles.FirstOrDefault(
                article => article.Id.Equals(request.FirstArticleId));

            var newsPortals = _memoryCache.Get<IEnumerable<NewsPortal>>(
                key: MemoryCacheConstants.NewsPortalKey);
            var keyWordsToFilterOut = request.KeyWordsToFilterOut is null ?
                Enumerable.Empty<string>() :
                request.KeyWordsToFilterOut.Split(',', StringSplitOptions.RemoveEmptyEntries);

            var articles = GetLatestArticles(
                savedArticles: savedArticles,
                firstArticleCreateDateTime: firstArticle?.CreateDateTime,
                request: request,
                categoryIds: categoryIds,
                newsPortalIds: newsPortalIds,
                keyWordsToFilterOut: keyWordsToFilterOut);

            var newNewsPortals = GetNewNewsPortals(
                newsPortals: newsPortals,
                request: request);

            var featuredArticles = GetFeaturedArticles(
                savedArticles: savedArticles,
                firstArticleCreateDateTime: firstArticle?.CreateDateTime,
                request: request,
                categoryIds: categoryIds,
                keyWordsToFilterOut: keyWordsToFilterOut);

            var response = new GetLatestArticlesQueryResponse_2_0
            {
                Articles = articles,
                FeaturedArticles = featuredArticles,
                NewNewsPortals = newNewsPortals,
                NewNewsPortalsPosition = _settingProvider.LatestSetting.NewsPortalSetting.NewNewsPortalsPosition,
                LastAddedNewsPortalDate = newsPortals
                    .OrderByDescending(newsPortal => newsPortal.CreatedAt)
                    .First()
                    .CreatedAt
                    .ToString(DateTimeConstants.MobileAppDateTimeFormat),
            };

            return Task.FromResult(result: response);
        }

        private static IEnumerable<GetLatestArticlesArticle_2_0> GetLatestArticles(
            IEnumerable<Article> savedArticles,
            DateTime? firstArticleCreateDateTime,
            GetLatestArticlesQuery_2_0 request,
            IEnumerable<int>? categoryIds,
            IEnumerable<int>? newsPortalIds,
            IEnumerable<string> keyWordsToFilterOut)
        {
            var articles = savedArticles
                .OrderArticlesByPublishDate()
                .FilterArticles_2_0(
                    categoryIds: categoryIds,
                    newsPortalIds: newsPortalIds,
                    titleSearchTerm: request.TitleSearchQuery,
                    articleCreateDateTime: firstArticleCreateDateTime)
                .FilterArticlesContainingKeyWords(keyWordsToFilterOut)
                // .FilterArticlesWithCoronaVirusContentForIosRelease(request.DeviceType, request.TargetedApiVersion)
                .Skip(request.Skip)
                .Take(request.Take);

            var articleDtos = articles
                .Select(GetLatestArticlesArticle_2_0.GetProjection().Compile());

            return articleDtos;
        }

        private static (IEnumerable<int>? newsPortalIds, IEnumerable<int>? categoryIds) ParseIds(GetLatestArticlesQuery_2_0 request)
        {
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

            return (newsPortalIds, categoryIds);
        }

        private IEnumerable<GetLatestArticlesNewsPortal_2_0> GetNewNewsPortals(
            IEnumerable<NewsPortal> newsPortals,
            GetLatestArticlesQuery_2_0 request)
        {
            if (request.Skip != 0)
            {
                return Array.Empty<GetLatestArticlesNewsPortal_2_0>();
            }

            var (newsPortalIds, categoryIds) = ParseIds(request);

            var newsPortalDtos = newsPortals
                .Where(
                    predicate: NewsPortal.GetLatestSugestedNewsPortalsPredicate(
                        newsPortalIds: newsPortalIds,
                        categoryIds: categoryIds,
                        maxAgeOfNewNewsPortal: _settingProvider.LatestSetting.NewsPortalSetting.MaxAgeOfNewNewsPortal)
                    .Compile())
                .Select(selector: GetLatestArticlesNewsPortal_2_0.GetProjection().Compile());

            var random = new Random();

            return newsPortalDtos.OrderBy(_ => random.Next());
        }

        private IEnumerable<GetLatestArticlesArticle_2_0> GetFeaturedArticles(
            IEnumerable<Article> savedArticles,
            DateTime? firstArticleCreateDateTime,
            GetLatestArticlesQuery_2_0 request,
            IEnumerable<int>? categoryIds,
            IEnumerable<string> keyWordsToFilterOut)
        {
            if (request.Skip != 0)
            {
                return Array.Empty<GetLatestArticlesArticle_2_0>();
            }

            var featuredArticles = savedArticles
                .FilterArticlesContainingKeyWords(keyWordsToFilterOut)
                .FilterFeaturedArticles(
                    categoryIds: null,
                    newsPortalIds: null,
                    maxAgeOfFeaturedArticle: _settingProvider.LatestSetting.ArticleSetting.MaxAgeOfFeaturedArticle,
                    articleCreateDateTime: firstArticleCreateDateTime)
                // .FilterArticlesWithCoronaVirusContentForIosRelease(request.DeviceType, request.TargetedApiVersion)
                .OrderFeaturedArticles(categoryIds);

            var featuredArticlesTake = _settingProvider.LatestSetting.ArticleSetting.FeaturedArticlesTake;
            var trendingArticlesTake = Math.Max(featuredArticlesTake - featuredArticles.Count(), 0);

            var trendingArticles = savedArticles
                .FilterArticlesContainingKeyWords(keyWordsToFilterOut)
                .FilterTrendingArticles(
                    maxAgeOfTrendingArticle: _settingProvider.LatestSetting.ArticleSetting.MaxAgeOfTrendingArticle,
                    articleCreateDateTime: null)
                // .FilterArticlesWithCoronaVirusContentForIosRelease(request.DeviceType, request.TargetedApiVersion)
                .OrderArticlesByTrendingScore()
                .Take(trendingArticlesTake)
                .OrderArticlesByCategory(categoryIds);

            var articleDtos = featuredArticles
                .Union(trendingArticles)
                .Take(featuredArticlesTake)
                .Select(GetLatestArticlesArticle_2_0.GetProjection().Compile());

            return articleDtos;
        }
    }
}
