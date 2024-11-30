// CreateRssFeedBase.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects.RssFeedDataTransferObjects;
using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using Espresso.Dashboard.Application.Categories.Queries.GetCategories;
using Espresso.Dashboard.Application.NewsPortals.Queries.GetNewsPortals;
using Espresso.Dashboard.Application.RssFeeds.Commands.UpdateRssFeed;
using Espresso.Dashboard.Application.RssFeeds.Queries.GetRssFeedsDetails;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.RssFeedEnums;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace Espresso.Dashboard.Pages.RssFeedPages.CreateRssFeed;

/// <summary>
/// NewsPortalDetailsBase.
/// </summary>
public class CreateRssFeedBase : ComponentBase
{
    /// <summary>
    /// Gets or sets <see cref="NewsPortal"/> details.
    /// </summary>
    protected GetRssFeedDetailsQueryResponse? RssFeedDetails { get; set; }

    protected bool Success { get; set; }

    /// <summary>
    /// Gets <see cref="Mediator"/> request sender.
    /// </summary>
    [Inject]
    private IServiceScopeFactory ServiceScopeFactory { get; init; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; init; } = null!;

    /// <inheritdoc/>
    protected override async Task OnInitializedAsync()
    {
        using var scope = ServiceScopeFactory.CreateAsyncScope();
        var sender = scope.ServiceProvider.GetRequiredService<ISender>();

        var pagingParameters = new PagingParameters
        {
            CurrentPage = 1,
            PageSize = 1000,
        };

        var newsPortals = await sender.Send(new GetNewsPortalsQuery(pagingParameters));
        var categories = await sender.Send(new GetCategoriesQuery(pagingParameters));

        var ampConfiguration = new AmpConfigurationDto(hasAmpArticles: false, templateUrl: string.Empty);
        var categoryParseConfiguration = new CategoryParseConfigurationDto(
            categoryParseStrategy: CategoryParseStrategy.FromRssFeed);
        var imageUrlParseConfiguration = new ImageUrlParseConfigurationDto(
            imageUrlParseStrategy: ImageUrlParseStrategy.FromContent,
            xPath: string.Empty,
            attributeName: string.Empty,
            shouldImageUrlBeWebScraped: false,
            imageUrlWebScrapeType: ImageUrlWebScrapeType.Attribute,
            jsonWebScrapePropertyNames: string.Empty,
            elementExtensionName: string.Empty,
            elementExtensionAttributeName: string.Empty,
            webScrapeRequestType: RequestType.Browser,
            elementExtensionValueParseType: ValueParseType.FullValue,
            elementExtensionValueType: XmlValueType.Value);
        var skipParseConfiguration = new SkipParseConfigurationDto(numberOfSkips: 2);

        var rssFeedDto = new RssFeedDto(
            id: default,
            url: string.Empty,
            requestType: RequestType.Normal,
            ampConfiguration: ampConfiguration,
            categoryParseConfiguration: categoryParseConfiguration,
            imageUrlParseConfiguration: imageUrlParseConfiguration,
            skipParseConfiguration: skipParseConfiguration,
            newsPortalId: newsPortals.NewsPortals.Items.First().NewsPortal.Id,
            categoryId: categories.CategoriesPagedList.Items.First().Id);

        RssFeedDetails = new GetRssFeedDetailsQueryResponse(
            rssFeed: rssFeedDto,
            newsPortals: newsPortals.NewsPortals.Items.Select(newsPortalResponse => newsPortalResponse.NewsPortal),
            categories: categories.CategoriesPagedList.Items,
            rssFeedCategories: [],
            rssFeedContentModifiers: []);
    }

    protected async Task OnSaveButtonClick()
    {
        if (!Success)
        {
            return;
        }

        if (RssFeedDetails is null)
        {
            return;
        }

        using var scope = ServiceScopeFactory.CreateAsyncScope();
        var sender = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new UpdateRssFeedCommand(
            rssFeed: RssFeedDetails.RssFeed,
            rssFeedCategories: RssFeedDetails.RssFeedCategories,
            rssFeedContentModifiers: RssFeedDetails.RssFeedContentModifiers);
        await sender.Send(command);

        NavigationManager.NavigateTo("rss-feeds");
    }
}
