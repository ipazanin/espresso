// GetCountriesQuery.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using MediatR;

namespace Espresso.Dashboard.Application.Countries.Queries.GetCountries;

public class GetCountriesQuery : IRequest<GetCountriesQueryResponse>
{
    public GetCountriesQuery(PagingParameters pagingParameters)
    {
        PagingParameters = pagingParameters;
    }

    public PagingParameters PagingParameters { get; }
}
