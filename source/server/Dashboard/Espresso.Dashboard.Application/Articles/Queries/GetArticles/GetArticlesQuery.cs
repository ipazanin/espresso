// GetArticlesQuery.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using Espresso.Application.Infrastructure.MediatorInfrastructure;
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
