// GetArticlesQuery.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using MediatR;

namespace Espresso.Dashboard.Application.Articles.Queries.GetArticles;

public class GetArticlesQuery : IRequest<GetArticlesQueryResponse>
{
    public GetArticlesQuery(PagingParameters pagingParameters)
    {
        PagingParameters = pagingParameters;
    }

    public PagingParameters PagingParameters { get; }
}
