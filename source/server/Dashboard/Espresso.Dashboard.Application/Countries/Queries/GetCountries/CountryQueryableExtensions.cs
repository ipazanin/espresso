// CountryQueryableExtensions.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.CountryDataTransferObjects;
using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using Espresso.Domain.Entities;

namespace Espresso.Dashboard.Application.Countries.Queries.GetCountries;

public static class CountryQueryableExtensions
{
    public static IQueryable<Country> OrderCountries(
        this IQueryable<Country> newsPortals,
        PagingParameters pagingParameters)
    {
        return (pagingParameters.OrderType, pagingParameters.SortColumn) switch
        {
            (OrderType.Ascending, nameof(CountryDto.Name)) => newsPortals.OrderBy(newsPortal => newsPortal.Name),
            (OrderType.Descending, nameof(CountryDto.Name)) => newsPortals.OrderByDescending(newsPortal => newsPortal.Name),
            _ => newsPortals.OrderBy(newsPortal => newsPortal.Name),
        };
    }

    public static IQueryable<Country> FilterCountries(
    this IQueryable<Country> newsPortals,
    PagingParameters pagingParameters)
    {
        if (string.IsNullOrWhiteSpace(pagingParameters.SearchString))
        {
            return newsPortals;
        }

        return newsPortals.Where(country => country.Name.Contains(pagingParameters.SearchString));
    }
}
