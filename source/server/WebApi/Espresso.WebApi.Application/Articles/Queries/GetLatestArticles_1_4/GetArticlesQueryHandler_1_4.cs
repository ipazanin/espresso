// GetArticlesQueryHandler_1_4.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.Extensions;
using Espresso.Domain.Infrastructure;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_1_4;
#pragma warning disable S101 // Types should be named in PascalCase
public class GetArticlesQueryHandler_1_4 :
#pragma warning restore S101 // Types should be named in PascalCase
        IRequestHandler<GetLatestArticlesQuery_1_4, GetLatestArticlesQueryResponse_1_4>
{
    private readonly IMemoryCache _memoryCache;
    private readonly ISettingProvider _settingProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetArticlesQueryHandler_1_4"/> class.
    /// </summary>
    /// <param name="memoryCache"></param>
    /// <param name="settingProvider"></param>
    public GetArticlesQueryHandler_1_4(IMemoryCache memoryCache, ISettingProvider settingProvider)
    {
        _memoryCache = memoryCache;
        _settingProvider = settingProvider;
    }

    public Task<GetLatestArticlesQueryResponse_1_4> Handle(
        GetLatestArticlesQuery_1_4 request,
        CancellationToken cancellationToken)
    {
        var articles = _memoryCache.Get<IEnumerable<Article>>(
            key: MemoryCacheConstants.ArticleKey);

        var firstArticle = articles.FirstOrDefault(
            article => article.Id.Equals(request.FirstArticleId));

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
            .FilterArticles(
                categoryIds: categoryIds,
                newsPortalIds: newsPortalIds,
                titleSearchTerm: request.TitleSearchQuery,
                articleCreateDateTime: firstArticle?.CreateDateTime)
            .Skip(request.Skip)
            .Take(request.Take)
            .Select(GetLatestArticlesArticle_1_4.GetProjection().Compile());

        var newsPortals = _memoryCache.Get<IEnumerable<NewsPortal>>(
            key: MemoryCacheConstants.NewsPortalKey);

        var newsPortalDtos = newsPortals
            .Where(
                predicate: NewsPortal.GetLatestSugestedNewsPortalsPredicate(
                    newsPortalIds: newsPortalIds,
                    categoryIds: categoryIds,
                    maxAgeOfNewNewsPortal: _settingProvider.LatestSetting.NewsPortalSetting.MaxAgeOfNewNewsPortal)
                .Compile())
            .Select(selector: GetLatestArticlesNewsPortal_1_4.GetProjection().Compile());

        var random = new Random();

        var response = new GetLatestArticlesQueryResponse_1_4
        {
            Articles = articleDtos,
            NewNewsPortals = newsPortalDtos.OrderBy(_ => random.Next()),
            NewNewsPortalsPosition = _settingProvider.LatestSetting.NewsPortalSetting.NewNewsPortalsPosition,
        };

        return Task.FromResult(result: response);
    }
}
