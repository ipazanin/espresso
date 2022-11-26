// GetCategoryArticlesQueryHandler_1_3.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.Extensions;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles_1_3;
#pragma warning disable S101 // Types should be named in PascalCase
public class GetCategoryArticlesQueryHandler_1_3 : IRequestHandler<GetCategoryArticlesQuery_1_3, GetCategoryArticlesQueryResponse_1_3>
#pragma warning restore S101 // Types should be named in PascalCase
{
    private readonly IMemoryCache _memoryCache;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetCategoryArticlesQueryHandler_1_3"/> class.
    /// </summary>
    /// <param name="memoryCache"></param>
    public GetCategoryArticlesQueryHandler_1_3(
        IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public Task<GetCategoryArticlesQueryResponse_1_3> Handle(GetCategoryArticlesQuery_1_3 request, CancellationToken cancellationToken)
    {
        var articles = _memoryCache.Get<IEnumerable<Article>>(
            key: MemoryCacheConstants.ArticleKey)!;

        var newsPortalIds = request.NewsPortalIds
            ?.Replace(" ", string.Empty)
            ?.Split(',')
            ?.Select(newsPortalIdString => int.TryParse(newsPortalIdString, out var newsPortalId) ? newsPortalId : default)
            ?.Where(newsPortalId => newsPortalId != default);

        var articleDtos = articles
            .OrderArticlesByPublishDate()
            .FilterArticles(
                categoryId: request.CategoryId,
                newsPortalIds: newsPortalIds,
                titleSearchTerm: null,
                articleCreateDateTime: null)
            .Skip(request.Skip)
            .Take(request.Take)
            .Select(GetCategoryArticlesArticle_1_3.GetProjection().Compile());

        var response = new GetCategoryArticlesQueryResponse_1_3
        {
            Articles = articleDtos,
        };

        return Task.FromResult(result: response);
    }
}
