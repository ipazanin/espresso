// GetCategoriesQuery.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using MediatR;

namespace Espresso.Dashboard.Application.Categories.Queries.GetCategories;

public class GetCategoriesQuery : IRequest<GetCategoriesQueryResponse>
{
    public GetCategoriesQuery(PagingParameters pagingParameters)
    {
        PagingParameters = pagingParameters;
    }

    public PagingParameters PagingParameters { get; }
}
