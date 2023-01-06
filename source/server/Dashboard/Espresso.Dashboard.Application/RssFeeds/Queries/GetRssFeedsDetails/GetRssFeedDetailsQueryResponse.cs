// GetRssFeedDetailsQueryResponse.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.CategoryDataTransferObjects;
using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects;
using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects.RssFeedDataTransferObjects;

namespace Espresso.Dashboard.Application.RssFeeds.Queries.GetRssFeedsDetails;

public class GetRssFeedDetailsQueryResponse
{
    public GetRssFeedDetailsQueryResponse(
        RssFeedDto rssFeed,
        IEnumerable<NewsPortalDto> newsPortals,
        IEnumerable<CategoryDto> categories,
        IList<RssFeedCategoryDto> rssFeedCategories,
        IList<RssFeedContentModifierDto> rssFeedContentModifiers)
    {
        RssFeed = rssFeed;
        NewsPortals = newsPortals;
        Categories = categories;
        RssFeedCategories = rssFeedCategories;
        RssFeedContentModifiers = rssFeedContentModifiers;
    }

    public RssFeedDto RssFeed { get; }

    public IList<RssFeedCategoryDto> RssFeedCategories { get; }

    public IList<RssFeedContentModifierDto> RssFeedContentModifiers { get; }

    public IEnumerable<NewsPortalDto> NewsPortals { get; }

    public IEnumerable<CategoryDto> Categories { get; }
}
