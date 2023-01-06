// GetArticlesQueryHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Dashboard.Application.Articles.Queries.GetArticles;

public class GetArticlesQueryHandler : IRequestHandler<GetArticlesQuery, GetArticlesQueryResponse>
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;

    public GetArticlesQueryHandler(IEspressoDatabaseContext espressoDatabaseContext)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
    }

    public async Task<GetArticlesQueryResponse> Handle(
        GetArticlesQuery request,
        CancellationToken cancellationToken)
    {
        var skip = request.PagingParameters.GetSkip();
        var take = request.PagingParameters.GetTake();

        var articles = await _espressoDatabaseContext
            .Articles
            .AsNoTracking()
            .FilterArticles(request.PagingParameters)
            .OrderArticles(request.PagingParameters)
            .Skip(skip)
            .Take(take)
            .Select(GetArticlesArticle.Projection)
            .ToArrayAsync(cancellationToken: cancellationToken);

        var totalCount = await _espressoDatabaseContext
            .Articles
            .AsNoTracking()
            .FilterArticles(request.PagingParameters)
            .CountAsync(cancellationToken);

        var pagingMetadata = new PagingMetadata(
            currentPage: request.PagingParameters.CurrentPage,
            pageSize: request.PagingParameters.PageSize,
            totalCount: totalCount);

        var articlesPagingList = new PagedList<GetArticlesArticle>(
            items: articles,
            pagingMetadata: pagingMetadata);

        var response = new GetArticlesQueryResponse(articles: articlesPagingList);

        return response;
    }
}
