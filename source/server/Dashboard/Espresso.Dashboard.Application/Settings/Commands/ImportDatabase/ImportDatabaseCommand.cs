// ImportDatabaseCommand.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.CategoryDataTransferObjects;
using Espresso.Application.DataTransferObjects.CountryDataTransferObjects;
using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects;
using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects.RssFeedDataTransferObjects;
using Espresso.Application.DataTransferObjects.SettingDataTransferObjects;
using Espresso.Domain.Entities;
using MediatR;

namespace Espresso.Dashboard.Application.Settings.ImportDatabase;

public class ImportDatabaseCommand : IRequest
{
    public ImportDatabaseCommand(
        SettingDto setting,
        IReadOnlyList<NewsPortalDto> newsPortals,
        IReadOnlyList<RegionDto> regions,
        IReadOnlyList<CategoryDto> categories,
        IReadOnlyList<RssFeedDto> rssFeeds,
        IReadOnlyList<RssFeedContentModifierDto> rssFeedContentModifiers,
        IReadOnlyList<RssFeedCategoryDto> rssFeedCategories,
        IReadOnlyList<NewsPortalImageDto> newsPortalImages,
        IReadOnlyList<CountryDto>? countries,
        IReadOnlyList<CountryImageDto>? countryImages)
    {
        Setting = setting;
        NewsPortals = newsPortals;
        Regions = regions;
        Categories = categories;
        RssFeeds = rssFeeds;
        RssFeedContentModifiers = rssFeedContentModifiers;
        RssFeedCategories = rssFeedCategories;
        NewsPortalImages = newsPortalImages;
        Countries = countries ?? Array.Empty<CountryDto>();
        CountryImages = countryImages ?? Array.Empty<CountryImageDto>();
    }

    public SettingDto Setting { get; }

    public IReadOnlyList<NewsPortalDto> NewsPortals { get; }

    public IReadOnlyList<RegionDto> Regions { get; }

    public IReadOnlyList<CategoryDto> Categories { get; }

    public IReadOnlyList<RssFeedDto> RssFeeds { get; }

    public IReadOnlyList<RssFeedContentModifierDto> RssFeedContentModifiers { get; }

    public IReadOnlyList<RssFeedCategoryDto> RssFeedCategories { get; }

    public IReadOnlyList<NewsPortalImageDto> NewsPortalImages { get; }

    public IReadOnlyList<CountryDto> Countries { get; }

    public IReadOnlyList<CountryImageDto> CountryImages { get; }
}
