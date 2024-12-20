﻿// GetLatestArticlesQueryHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Security.Cryptography;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.Extensions;
using Espresso.Domain.Infrastructure;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles;
#pragma warning disable S101 // Types should be named in PascalCase
public class GetArticlesQueryHandler_2_0 : IRequestHandler<GetLatestArticlesQuery, GetLatestArticlesQueryResponse>
#pragma warning restore S101 // Types should be named in PascalCase
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

    public Task<GetLatestArticlesQueryResponse> Handle(
        GetLatestArticlesQuery request,
        CancellationToken cancellationToken)
    {
        var (newsPortalIds, categoryIds) = ParseIds(request);

        var savedArticles = _memoryCache.Get<IEnumerable<Article>>(
            key: MemoryCacheConstants.ArticleKey)!;

        var firstArticle = savedArticles.FirstOrDefault(
            article => article.Id.Equals(request.FirstArticleId));

        var newsPortals = _memoryCache.Get<IEnumerable<NewsPortal>>(
            key: MemoryCacheConstants.NewsPortalKey)!;
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

        var response = new GetLatestArticlesQueryResponse
        {
            Articles = articles,
            FeaturedArticles = featuredArticles,
            NewNewsPortals = newNewsPortals,
            NewNewsPortalsPosition = _settingProvider.LatestSetting.NewsPortalSetting.NewNewsPortalsPosition,
            LastAddedNewsPortalDate = newsPortals
                .OrderByDescending(newsPortal => newsPortal.CreatedAt)
                .First()
                .CreatedAt
                .ToString(DateTimeConstants.MobileAppDateTimeFormat, CultureInfo.InvariantCulture),
        };

        return Task.FromResult(result: response);
    }

    private static IEnumerable<GetLatestArticlesArticle> GetLatestArticles(
        IEnumerable<Article> savedArticles,
        DateTimeOffset? firstArticleCreateDateTime,
        GetLatestArticlesQuery request,
        IEnumerable<int>? categoryIds,
        IEnumerable<int>? newsPortalIds,
        IEnumerable<string> keyWordsToFilterOut)
    {
        var articles = savedArticles
            .OrderArticlesByPublishDate()
            .FilterArticles(
                categoryIds: categoryIds,
                newsPortalIds: newsPortalIds,
                titleSearchTerm: request.TitleSearchQuery,
                articleCreateDateTime: firstArticleCreateDateTime)
            .FilterArticlesContainingKeyWords(keyWordsToFilterOut)
            .Skip(request.Skip)
            .Take(request.Take);

        var articleDtos = articles
            .Select(GetLatestArticlesArticle.GetProjection().Compile());

        return articleDtos;
    }

    private static (IEnumerable<int>? newsPortalIds, IEnumerable<int>? categoryIds) ParseIds(GetLatestArticlesQuery request)
    {
        var newsPortalIds = request.NewsPortalIds
            ?.Replace(" ", string.Empty, StringComparison.Ordinal)
            .Split(',')
            .Select(newsPortalIdString => int.TryParse(newsPortalIdString, out var newsPortalId) ? newsPortalId : default)
            .Where(newsPortalId => newsPortalId != default);

        var categoryIds = request.CategoryIds
            ?.Replace(" ", string.Empty, StringComparison.Ordinal)
            .Split(',')
            .Select(categoryIdString => int.TryParse(categoryIdString, out var categoryId) ? categoryId : default)
            .Where(categoryId => categoryId != default);

        return (newsPortalIds, categoryIds);
    }

    private IEnumerable<GetLatestArticlesNewsPortal> GetNewNewsPortals(
        IEnumerable<NewsPortal> newsPortals,
        GetLatestArticlesQuery request)
    {
        if (request.Skip != 0)
        {
            return [];
        }

        var (newsPortalIds, categoryIds) = ParseIds(request);

        var newsPortalDtos = newsPortals
            .Where(
                predicate: NewsPortal.GetLatestSugestedNewsPortalsPredicate(
                    newsPortalIds: newsPortalIds,
                    categoryIds: categoryIds,
                    maxAgeOfNewNewsPortal: _settingProvider.LatestSetting.NewsPortalSetting.MaxAgeOfNewNewsPortal)
                .Compile())
            .Select(selector: GetLatestArticlesNewsPortal.GetProjection().Compile());

        return newsPortalDtos.OrderBy(_ => RandomNumberGenerator.GetInt32(100));
    }

    private IEnumerable<GetLatestArticlesArticle> GetFeaturedArticles(
        IEnumerable<Article> savedArticles,
        DateTimeOffset? firstArticleCreateDateTime,
        GetLatestArticlesQuery request,
        IEnumerable<int>? categoryIds,
        IEnumerable<string> keyWordsToFilterOut)
    {
        if (request.Skip != 0)
        {
            return [];
        }

        var featuredArticles = savedArticles
            .FilterArticlesContainingKeyWords(keyWordsToFilterOut)
            .FilterFeaturedArticles(
                categoryIds: null,
                newsPortalIds: null,
                maxAgeOfFeaturedArticle: _settingProvider.LatestSetting.ArticleSetting.MaxAgeOfFeaturedArticle,
                articleCreateDateTime: firstArticleCreateDateTime)
            .OrderFeaturedArticles(categoryIds);

        var featuredArticlesTake = _settingProvider.LatestSetting.ArticleSetting.FeaturedArticlesTake;
        var trendingArticlesTake = Math.Max(featuredArticlesTake - featuredArticles.Count(), 0);

        var trendingArticles = savedArticles
            .FilterArticlesContainingKeyWords(keyWordsToFilterOut)
            .FilterTrendingArticles(
                maxAgeOfTrendingArticle: _settingProvider.LatestSetting.ArticleSetting.MaxAgeOfTrendingArticle,
                articleCreateDateTime: null,
                categoryId: null)
            .OrderArticlesByTrendingScore()
            .Take(trendingArticlesTake)
            .OrderArticlesByCategory(categoryIds);

        var articleDtos = featuredArticles
            .Union(trendingArticles)
            .Take(featuredArticlesTake)
            .Select(GetLatestArticlesArticle.GetProjection().Compile());

        return articleDtos;
    }
}
