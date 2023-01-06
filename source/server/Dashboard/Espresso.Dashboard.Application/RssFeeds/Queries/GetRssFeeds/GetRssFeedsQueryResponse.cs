// GetRssFeedsQueryResponse.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;

namespace Espresso.Dashboard.Application.RssFeeds.Queries.GetRssFeeds;

public class GetRssFeedsQueryResponse
{
    public GetRssFeedsQueryResponse(PagedList<GetRssFeedsQueryRssFeed> rssFeeds)
    {
        RssFeeds = rssFeeds;
    }

    public PagedList<GetRssFeedsQueryRssFeed> RssFeeds { get; }
}
