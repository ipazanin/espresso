// GetGroupedCategoryArticlesQueryHandler.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.Extensions;
using Espresso.Domain.Infrastructure;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.Articles.Queries.GetGroupedCategoryArticles;

public class GetGroupedCategoryArticlesQueryHandler : IRequestHandler<GetGroupedCategoryArticlesQuery, GetGroupedCategoryArticlesQueryResponse>
{
    private readonly IMemoryCache _memoryCache;
    private readonly ISettingProvider _settingProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetGroupedCategoryArticlesQueryHandler"/> class.
    /// </summary>
    /// <param name="memoryCache"></param>
    /// <param name="settingProvider"></param>
    public GetGroupedCategoryArticlesQueryHandler(IMemoryCache memoryCache, ISettingProvider settingProvider)
    {
        _memoryCache = memoryCache;
        _settingProvider = settingProvider;
    }

    public Task<GetGroupedCategoryArticlesQueryResponse> Handle(
        GetGroupedCategoryArticlesQuery request,
        CancellationToken cancellationToken)
    {
        var savedArticles = _memoryCache.Get<IEnumerable<Article>>(
            key: MemoryCacheConstants.ArticleKey);

        var firstArticle = savedArticles.FirstOrDefault(
            article => article.Id.Equals(request.FirstArticleId));

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
            savedArticles: savedArticles,
            firstArticleCreateDateTime: firstArticle?.CreateDateTime,
            titleSearchTerm: request.TitleSearchQuery,
            categoryIds: new[] { request.CategoryId },
            newsPortalIds: newsPortalIds,
            keyWordsToFilterOut: keyWordsToFilterOut);

        var response = new GetGroupedCategoryArticlesQueryResponse
        {
            Articles = articleDtos,
            NewNewsPortals = newsPortalDtos,
            NewNewsPortalsPosition = _settingProvider.LatestSetting.NewsPortalSetting.NewNewsPortalsPosition,
        };

        return Task.FromResult(result: response);
    }

    private static IEnumerable<IEnumerable<GetGroupedCategoryArticlesArticle>> GetArticles(
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
            .Select(articles => articles.Select(GetGroupedCategoryArticlesArticle.GetProjection().Compile()));
    }

    private IEnumerable<GetGroupedCategoryArticlesNewsPortal> GetNewNewsPortals(
        IEnumerable<int>? newsPortalIds,
        GetGroupedCategoryArticlesQuery request)
    {
        if (request.Skip != 0)
        {
            return Array.Empty<GetGroupedCategoryArticlesNewsPortal>();
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
            .Select(selector: GetGroupedCategoryArticlesNewsPortal.GetProjection().Compile())
            .OrderBy(_ => random.Next());

        return newsPortalDtos;
    }
}
