// GetCategoryArticlesQueryHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Security.Cryptography;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.Extensions;
using Espresso.Domain.Infrastructure;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles;

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
        var articles = _memoryCache.Get<IEnumerable<Article>>(
            key: MemoryCacheConstants.ArticleKey)!;

        var firstArticle = articles.FirstOrDefault(
            article => article.Id.Equals(request.FirstArticleId));

        var newsPortalIds = request.NewsPortalIds
            ?.Replace(" ", string.Empty, StringComparison.Ordinal)
            .Split(',')
            .Select(newsPortalIdString => int.TryParse(newsPortalIdString, out var newsPortalId) ? newsPortalId : default)
            .Where(newsPortalId => newsPortalId != default);

        var keyWordsToFilterOut = request.KeyWordsToFilterOut is null ?
            Enumerable.Empty<string>() :
            request.KeyWordsToFilterOut.Split(',', StringSplitOptions.RemoveEmptyEntries);

        var filteredArticles = articles
            .OrderArticlesByPublishDate()
            .FilterArticlesContainingKeyWords(keyWordsToFilterOut)
            .FilterArticles(
                categoryId: request.CategoryId,
                newsPortalIds: newsPortalIds,
                titleSearchTerm: request.TitleSearchQuery,
                articleCreateDateTime: firstArticle?.CreateDateTime)
            .Skip(request.Skip)
            .Take(request.Take);

        var articleDtos = filteredArticles.Select(GetCategoryArticlesArticle.GetProjection().Compile());

        var newsPortalDtos = GetNewNewsPortals(
            newsPortalIds: newsPortalIds,
            request: request);

        var response = new GetCategoryArticlesQueryResponse
        {
            Articles = articleDtos,
            NewNewsPortals = newsPortalDtos.OrderBy(_ => RandomNumberGenerator.GetInt32(100)),
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
            key: MemoryCacheConstants.NewsPortalKey)!;

        var newsPortalDtos = newsPortals
            .Where(
                NewsPortal.GetCategorySugestedNewsPortalsPredicate(
                    newsPortalIds: newsPortalIds,
                    categoryId: request.CategoryId,
                    regionId: request.RegionId,
                    maxAgeOfNewNewsPortal: _settingProvider.LatestSetting.NewsPortalSetting.MaxAgeOfNewNewsPortal)
                .Compile())
            .Select(selector: GetCategoryArticlesNewsPortal.GetProjection().Compile());

        return newsPortalDtos;
    }
}
