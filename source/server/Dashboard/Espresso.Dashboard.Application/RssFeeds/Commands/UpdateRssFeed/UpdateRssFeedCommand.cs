// UpdateRssFeedCommand.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects.RssFeedDataTransferObjects;
using MediatR;

namespace Espresso.Dashboard.Application.RssFeeds.Commands.UpdateRssFeed;

public class UpdateRssFeedCommand : IRequest
{
    public UpdateRssFeedCommand(
        RssFeedDto rssFeed,
        IEnumerable<RssFeedCategoryDto> rssFeedCategories,
        IEnumerable<RssFeedContentModifierDto> rssFeedContentModifiers)
    {
        RssFeed = rssFeed;
        RssFeedCategories = rssFeedCategories;
        RssFeedContentModifiers = rssFeedContentModifiers;
    }

    public RssFeedDto RssFeed { get; }

    public IEnumerable<RssFeedCategoryDto> RssFeedCategories { get; }

    public IEnumerable<RssFeedContentModifierDto> RssFeedContentModifiers { get; }
}
