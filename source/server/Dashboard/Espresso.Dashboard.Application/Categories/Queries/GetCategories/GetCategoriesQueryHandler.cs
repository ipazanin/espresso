// GetCategoriesQueryHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.CategoryDataTransferObjects;
using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Dashboard.Application.Categories.Queries.GetCategories;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, GetCategoriesQueryResponse>
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;

    public GetCategoriesQueryHandler(IEspressoDatabaseContext espressoDatabaseContext)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
    }

    public async Task<GetCategoriesQueryResponse> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _espressoDatabaseContext
            .Categories
            .AsNoTracking()
            .OrderBy(category => category.SortIndex)
            .Skip(request.PagingParameters.GetSkip())
            .Take(request.PagingParameters.GetTake())
            .Select(CategoryDto.Projection)
            .ToArrayAsync(cancellationToken);

        var totalNumberOfCategories = await _espressoDatabaseContext
            .Categories
            .AsNoTracking()
            .CountAsync(cancellationToken);

        var pagingMetadata = new PagingMetadata(
            currentPage: request.PagingParameters.CurrentPage,
            pageSize: request.PagingParameters.PageSize,
            totalCount: totalNumberOfCategories);

        var pagedList = new PagedList<CategoryDto>(
            items: categories,
            pagingMetadata: pagingMetadata);

        return new GetCategoriesQueryResponse(pagedList);
    }
}
