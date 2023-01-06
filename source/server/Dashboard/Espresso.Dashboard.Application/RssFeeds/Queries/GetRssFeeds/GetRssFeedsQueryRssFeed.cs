// GetRssFeedsQueryRssFeed.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Application.DataTransferObjects.CategoryDataTransferObjects;
using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects;
using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects.RssFeedDataTransferObjects;
using Espresso.Domain.Entities;

namespace Espresso.Dashboard.Application.RssFeeds.Queries.GetRssFeeds;

public class GetRssFeedsQueryRssFeed
{
    public GetRssFeedsQueryRssFeed(RssFeedDto rssFeed, NewsPortalDto newsPortal, CategoryDto category)
    {
        RssFeed = rssFeed;
        NewsPortal = newsPortal;
        Category = category;
    }

    public static Expression<Func<RssFeed, GetRssFeedsQueryRssFeed>> Projection
    {
        get
        {
            var rssFeedProjection = RssFeedDto.GetProjection().Compile();
            var newsPortalProjection = NewsPortalDto.GetProjection().Compile();
            var categoryProjection = CategoryDto.Projection.Compile();
            return rssFeed => new GetRssFeedsQueryRssFeed(
                rssFeedProjection.Invoke(rssFeed),
                newsPortalProjection.Invoke(rssFeed.NewsPortal!),
                categoryProjection.Invoke(rssFeed.Category!));
        }
    }

    public RssFeedDto RssFeed { get; }

    public NewsPortalDto NewsPortal { get; }

    public CategoryDto Category { get; set; }
}
