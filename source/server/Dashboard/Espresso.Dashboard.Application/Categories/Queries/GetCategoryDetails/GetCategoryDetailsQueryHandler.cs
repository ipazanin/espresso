// GetCategoryDetailsQueryHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.CategoryDataTransferObjects;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Dashboard.Application.Categories.Queries.GetCategoryDetails;

public class GetCategoryDetailsQueryHandler : IRequestHandler<GetCategoryDetailsQuery, GetCategoryDetailsQueryResponse>
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;

    public GetCategoryDetailsQueryHandler(IEspressoDatabaseContext espressoDatabaseContext)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
    }

    public async Task<GetCategoryDetailsQueryResponse> Handle(GetCategoryDetailsQuery request, CancellationToken cancellationToken)
    {
        var category = await _espressoDatabaseContext
            .Categories
            .AsNoTracking()
            .Select(CategoryDto.Projection)
            .FirstAsync(category => category.Id == request.CategoryId, cancellationToken);

        return new GetCategoryDetailsQueryResponse(category);
    }
}
