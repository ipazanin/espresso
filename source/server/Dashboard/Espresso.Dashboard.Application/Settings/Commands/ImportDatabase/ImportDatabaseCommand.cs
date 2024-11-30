// ImportDatabaseCommand.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.CategoryDataTransferObjects;
using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects;
using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects.RssFeedDataTransferObjects;
using Espresso.Application.DataTransferObjects.SettingDataTransferObjects;
using MediatR;

namespace Espresso.Dashboard.Application.Settings.Commands.ImportDatabase;

public class ImportDatabaseCommand : IRequest
{
    public ImportDatabaseCommand(
        SettingDto setting,
        IEnumerable<NewsPortalDto> newsPortals,
        IEnumerable<RegionDto> regions,
        IEnumerable<CategoryDto> categories,
        IEnumerable<RssFeedDto> rssFeeds,
        IEnumerable<RssFeedContentModifierDto> rssFeedContentModifiers,
        IEnumerable<RssFeedCategoryDto> rssFeedCategories,
        IEnumerable<NewsPortalImageDto> newsPortalImages)
    {
        Setting = setting;
        NewsPortals = newsPortals;
        Regions = regions;
        Categories = categories;
        RssFeeds = rssFeeds;
        RssFeedContentModifiers = rssFeedContentModifiers;
        RssFeedCategories = rssFeedCategories;
        NewsPortalImages = newsPortalImages;
    }

    public SettingDto Setting { get; }

    public IEnumerable<NewsPortalDto> NewsPortals { get; }

    public IEnumerable<RegionDto> Regions { get; }

    public IEnumerable<CategoryDto> Categories { get; }

    public IEnumerable<RssFeedDto> RssFeeds { get; }

    public IEnumerable<RssFeedContentModifierDto> RssFeedContentModifiers { get; }

    public IEnumerable<RssFeedCategoryDto> RssFeedCategories { get; }

    public IEnumerable<NewsPortalImageDto> NewsPortalImages { get; }
}
