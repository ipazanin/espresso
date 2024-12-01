// GetTrendingArticlesQueryHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.Extensions;
using Espresso.Domain.Infrastructure;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.Articles.Queries.GetTrendingArticles;

public class GetTrendingArticlesQueryHandler : IRequestHandler<GetTrendingArticlesQuery, GetTrendingArticlesQueryResponse>
{
    private readonly IMemoryCache _memoryCache;
    private readonly ISettingProvider _settingProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetTrendingArticlesQueryHandler"/> class.
    /// </summary>
    /// <param name="memoryCache"></param>
    /// <param name="settingProvider"></param>
    public GetTrendingArticlesQueryHandler(IMemoryCache memoryCache, ISettingProvider settingProvider)
    {
        _memoryCache = memoryCache;
        _settingProvider = settingProvider;
    }

    public Task<GetTrendingArticlesQueryResponse> Handle(GetTrendingArticlesQuery request, CancellationToken cancellationToken)
    {
        var articles = _memoryCache.Get<IEnumerable<Article>>(
            key: MemoryCacheConstants.ArticleKey)!;

        var firstArticle = articles.FirstOrDefault(
            article => article.Id.Equals(request.FirstArticleId));

        var maxAgeOfTrendingArticle = request.MaxAgeOfTrendingArticleInHours switch
        {
            not null => TimeSpan.FromHours(request.MaxAgeOfTrendingArticleInHours.Value),
            _ => _settingProvider.LatestSetting.ArticleSetting.MaxAgeOfTrendingArticle
        };

        var articleDtos = articles
            .FilterTrendingArticles(
                maxAgeOfTrendingArticle: maxAgeOfTrendingArticle,
                articleCreateDateTime: firstArticle?.CreateDateTime,
                categoryId: request.CategoryId)
            .OrderArticlesByNumberOfClicks()
            .Skip(request.Skip)
            .Take(request.Take)
            .Select(GetTrendingArticlesArticle.GetProjection().Compile());

        var response = new GetTrendingArticlesQueryResponse
        {
            Articles = articleDtos,
        };

        return Task.FromResult(result: response);
    }
}
