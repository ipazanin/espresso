// GetGroupedLatestArticlesQueryHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.Extensions;
using Espresso.Domain.Infrastructure;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.Articles.Queries.GetGroupedLatestArticles;

public class GetGroupedLatestArticlesQueryHandler : IRequestHandler<GetGroupedLatestArticlesQuery, GetGroupedLatestArticlesQueryResponse>
{
    private readonly IMemoryCache _memoryCache;
    private readonly ISettingProvider _settingProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetGroupedLatestArticlesQueryHandler"/> class.
    /// </summary>
    /// <param name="memoryCache"></param>
    /// <param name="settingProvider"></param>
    public GetGroupedLatestArticlesQueryHandler(IMemoryCache memoryCache, ISettingProvider settingProvider)
    {
        _memoryCache = memoryCache;
        _settingProvider = settingProvider;
    }

    public Task<GetGroupedLatestArticlesQueryResponse> Handle(
        GetGroupedLatestArticlesQuery request,
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
            titleSearchTerm: request.TitleSearchQuery,
            categoryIds: categoryIds,
            newsPortalIds: newsPortalIds,
            keyWordsToFilterOut: keyWordsToFilterOut);

        var newNewsPortals = GetNewNewsPortals(
            newsPortals: newsPortals,
            newsPortalIds: newsPortalIds,
            categoryIds: categoryIds,
            request: request);

        var featuredArticles = GetFeaturedArticles(
            savedArticles: savedArticles,
            firstArticleCreateDateTime: firstArticle?.CreateDateTime,
            request: request,
            categoryIds: categoryIds,
            keyWordsToFilterOut: keyWordsToFilterOut);

        var response = new GetGroupedLatestArticlesQueryResponse
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

    private static IEnumerable<IEnumerable<GetGroupedLatestArticlesArticle>> GetLatestArticles(
        IEnumerable<Article> savedArticles,
        DateTimeOffset? firstArticleCreateDateTime,
        string? titleSearchTerm,
        IEnumerable<int>? categoryIds,
        IEnumerable<int>? newsPortalIds,
        IEnumerable<string> keyWordsToFilterOut)
    {
        var groupedArticles = savedArticles.GetGroupedArticlesBySimilarity(
            firstArticleCreateDateTime: firstArticleCreateDateTime,
            categoryIds: categoryIds,
            newsPortalIds: newsPortalIds,
            keyWordsToFilterOut: keyWordsToFilterOut,
            titleSearchTerm: titleSearchTerm);

        return groupedArticles
            .Select(articles => articles.Select(GetGroupedLatestArticlesArticle.GetProjection().Compile()));
    }

    private static (IEnumerable<int>? newsPortalIds, IEnumerable<int>? categoryIds) ParseIds(GetGroupedLatestArticlesQuery request)
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

    private IEnumerable<GetGroupedLatestArticlesArticle> GetFeaturedArticles(
        IEnumerable<Article> savedArticles,
        DateTimeOffset? firstArticleCreateDateTime,
        GetGroupedLatestArticlesQuery request,
        IEnumerable<int>? categoryIds,
        IEnumerable<string> keyWordsToFilterOut)
    {
        if (request.Skip != 0)
        {
            return Array.Empty<GetGroupedLatestArticlesArticle>();
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
                articleCreateDateTime: null)
            .OrderArticlesByTrendingScore()
            .Take(trendingArticlesTake)
            .OrderArticlesByCategory(categoryIds);

        var joinedArticles = featuredArticles
            .Union(trendingArticles);

        var articleDtos = joinedArticles
            .Take(featuredArticlesTake)
            .Select(GetGroupedLatestArticlesArticle.GetProjection().Compile());

        return articleDtos;
    }

    private IEnumerable<GetGroupedLatestArticlesNewsPortal> GetNewNewsPortals(
        IEnumerable<NewsPortal> newsPortals,
        IEnumerable<int>? newsPortalIds,
        IEnumerable<int>? categoryIds,
        GetGroupedLatestArticlesQuery request)
    {
        if (request.Skip != 0)
        {
            return Array.Empty<GetGroupedLatestArticlesNewsPortal>();
        }

        var newsPortalDtos = newsPortals
            .Where(
                predicate: NewsPortal.GetLatestSuggestedNewsPortalsPredicate(
                    newsPortalIds: newsPortalIds,
                    categoryIds: categoryIds,
                    maxAgeOfNewNewsPortal: _settingProvider.LatestSetting.NewsPortalSetting.MaxAgeOfNewNewsPortal)
                .Compile())
            .Select(selector: GetGroupedLatestArticlesNewsPortal.GetProjection().Compile());

        var random = new Random();

        return newsPortalDtos.OrderBy(_ => random.Next());
    }
}
