// NewsPortalQueryableExtensions.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.CategoryDataTransferObjects;
using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects;
using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using Espresso.Domain.Entities;

namespace Espresso.Dashboard.Application.NewsPortals.Queries.GetNewsPortals;

public static class NewsPortalQueryableExtensions
{
    public static IQueryable<NewsPortal> OrderNewsPortals(
        this IQueryable<NewsPortal> newsPortals,
        PagingParameters pagingParameters)
    {
        return (pagingParameters.OrderType, pagingParameters.SortColumn) switch
        {
            (OrderType.Ascending, nameof(NewsPortalDto.Name)) => newsPortals.OrderBy(newsPortal => newsPortal.Name),
            (OrderType.Descending, nameof(NewsPortalDto.Name)) => newsPortals.OrderByDescending(newsPortal => newsPortal.Name),
            (OrderType.Ascending, nameof(NewsPortalDto.BaseUrl)) => newsPortals.OrderBy(newsPortal => newsPortal.BaseUrl),
            (OrderType.Descending, nameof(NewsPortalDto.BaseUrl)) => newsPortals.OrderByDescending(newsPortal => newsPortal.BaseUrl),
            (OrderType.Ascending, nameof(NewsPortalDto.IsEnabled)) => newsPortals.OrderBy(newsPortal => newsPortal.IsEnabled),
            (OrderType.Descending, nameof(NewsPortalDto.IsEnabled)) => newsPortals.OrderByDescending(newsPortal => newsPortal.IsEnabled),
            (OrderType.Ascending, nameof(CategoryDto)) => newsPortals.OrderBy(newsPortal => newsPortal.Category!.Name),
            (OrderType.Descending, nameof(CategoryDto)) => newsPortals.OrderByDescending(newsPortal => newsPortal.Category!.Name),
            _ => newsPortals.OrderBy(newsPortal => newsPortal.Name),
        };
    }

    public static IQueryable<NewsPortal> FilterNewsPortals(
    this IQueryable<NewsPortal> newsPortals,
    PagingParameters pagingParameters)
    {
        if (string.IsNullOrWhiteSpace(pagingParameters.SearchString))
        {
            return newsPortals;
        }

        return newsPortals.Where(newsPortal => newsPortal.Name.Contains(pagingParameters.SearchString) ||
            newsPortal.BaseUrl.Contains(pagingParameters.SearchString) ||
            newsPortal.Category!.Name.Contains(pagingParameters.SearchString));
    }
}
