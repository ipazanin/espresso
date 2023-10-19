﻿// GetNewsPortalDetailsQueryResponse.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.CategoryDataTransferObjects;
using Espresso.Application.DataTransferObjects.CountryDataTransferObjects;
using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects;
using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects.RssFeedDataTransferObjects;

namespace Espresso.Dashboard.Application.NewsPortals.Queries.GetNewsPortalDetails;

/// <summary>
/// Get news portal details query response.
/// </summary>
public class GetNewsPortalDetailsQueryResponse
{
    public GetNewsPortalDetailsQueryResponse(
        NewsPortalDto newsPortal,
        IEnumerable<CategoryDto> categories,
        IEnumerable<RegionDto> regions,
        IEnumerable<RssFeedDto> rssFeeds,
        IReadOnlyList<CountryDto> countries)
    {
        NewsPortal = newsPortal;
        Categories = categories;
        Regions = regions;
        RssFeeds = rssFeeds;
        Countries = countries;
    }

    public NewsPortalDto NewsPortal { get; }

    public IEnumerable<CategoryDto> Categories { get; }

    public IEnumerable<RegionDto> Regions { get; }

    public IEnumerable<RssFeedDto> RssFeeds { get; }

    public IReadOnlyList<CountryDto> Countries { get; }
}
