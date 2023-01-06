// RssFeedQueryableExtensions.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.CategoryDataTransferObjects;
using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects;
using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects.RssFeedDataTransferObjects;
using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using Espresso.Domain.Entities;

namespace Espresso.Dashboard.Application.RssFeeds.Queries.GetRssFeeds;

public static class RssFeedQueryableExtensions
{
    public static IQueryable<RssFeed> FilterRssFeeds(this IQueryable<RssFeed> rssFeeds, PagingParameters pagingParameters)
    {
        if (string.IsNullOrWhiteSpace(pagingParameters.SearchString))
        {
            return rssFeeds;
        }

        return rssFeeds.Where(rssFeed => rssFeed.Url.Contains(pagingParameters.SearchString) ||
                rssFeed.NewsPortal!.Name.Contains(pagingParameters.SearchString) ||
                rssFeed.Category!.Name.Contains(pagingParameters.SearchString));
    }

    public static IQueryable<RssFeed> OrderRssFeeds(
        this IQueryable<RssFeed> rssFeeds,
        PagingParameters pagingParameters)
    {
        return (pagingParameters.OrderType, pagingParameters.SortColumn) switch
        {
            (OrderType.Ascending, nameof(RssFeedDto.Url)) => rssFeeds.OrderBy(rssFeed => rssFeed.Url),
            (OrderType.Descending, nameof(RssFeedDto.Url)) => rssFeeds.OrderByDescending(rssFeed => rssFeed.Url),
            (OrderType.Ascending, nameof(NewsPortalDto)) => rssFeeds.OrderBy(rssFeed => rssFeed.NewsPortal!.Name),
            (OrderType.Descending, nameof(NewsPortalDto)) => rssFeeds.OrderByDescending(rssFeed => rssFeed.NewsPortal!.Name),
            (OrderType.Ascending, nameof(CategoryDto)) => rssFeeds.OrderBy(rssFeed => rssFeed.Category!.Name),
            (OrderType.Descending, nameof(CategoryDto)) => rssFeeds.OrderByDescending(rssFeed => rssFeed.Category!.Name),
            _ => rssFeeds.OrderBy(rssFeed => rssFeed.Url),
        };
    }
}
