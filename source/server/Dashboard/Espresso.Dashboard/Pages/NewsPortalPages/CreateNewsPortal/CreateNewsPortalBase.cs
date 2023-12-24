// CreateNewsPortalBase.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects;
using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects.RssFeedDataTransferObjects;
using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using Espresso.Dashboard.Application.Categories.Queries.GetCategories;
using Espresso.Dashboard.Application.NewsPortalImage.Queries.GetNewsPortalImage;
using Espresso.Dashboard.Application.NewsPortals.Commands.CreateNewsPortal;
using Espresso.Dashboard.Application.NewsPortals.Queries.GetNewsPortalDetails;
using Espresso.Dashboard.Application.Regions.Queries.GetRegions;
using Espresso.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace Espresso.Dashboard.Pages.NewsPortalPages.CreateNewsPortal;

/// <summary>
/// NewsPortalDetailsBase.
/// </summary>
public class CreateNewsPortalBase : ComponentBase
{
    /// <summary>
    /// Gets or sets <see cref="NewsPortal"/> details.
    /// </summary>
    protected GetNewsPortalDetailsQueryResponse? NewsPortalDetailsQueryResponse { get; set; }

    protected GetNewsPortalImageQueryResponse NewsPortalImageResponse { get; set; } = null!;

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

        var categories = await sender.Send(new GetCategoriesQuery(pagingParameters));
        var regions = await sender.Send(new GetRegionsQuery(pagingParameters));

        var newsPortal = new NewsPortalDto(
            id: default,
            name: string.Empty,
            baseUrl: string.Empty,
            iconUrl: string.Empty,
            isNewOverride: default,
            createdAt: DateTimeOffset.UtcNow,
            isEnabled: true,
            categoryId: categories.CategoriesPagedList.Items.First().Id,
            regionId: regions.RegionsPagedList.Items.First().Id);

        NewsPortalDetailsQueryResponse = new GetNewsPortalDetailsQueryResponse(
            newsPortal: newsPortal,
            categories: categories.CategoriesPagedList.Items,
            regions: regions.RegionsPagedList.Items,
            rssFeeds: Enumerable.Empty<RssFeedDto>());

        NewsPortalImageResponse = new(null);
    }

    protected async Task OnSaveButtonClick()
    {
        if (!Success)
        {
            return;
        }

        if (NewsPortalDetailsQueryResponse is null)
        {
            return;
        }

        using var scope = ServiceScopeFactory.CreateAsyncScope();
        var sender = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new CreateNewsPortalCommand(
            NewsPortalDetailsQueryResponse.NewsPortal,
            NewsPortalImageResponse.NewsPortalImage);
        await sender.Send(command);

        NavigationManager.NavigateTo("news-portals");
    }
}
