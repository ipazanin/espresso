// GetCategoryDetailsQuery.cs
//
// © 2022 Espresso News. All rights reserved.

using MediatR;

namespace Espresso.Dashboard.Application.Categories.Queries.GetCategoryDetails;

public class GetCategoryDetailsQuery : IRequest<GetCategoryDetailsQueryResponse>
{
    public GetCategoryDetailsQuery(int categoryId)
    {
        CategoryId = categoryId;
    }

    public int CategoryId { get; }
}
