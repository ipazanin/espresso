// NewsPortalDetailsBase.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Dashboard.Application.NewsPortalImage.Commands.ImportNewsPortalImage;
using Espresso.Dashboard.Application.NewsPortalImage.Queries.GetNewsPortalImage;
using Espresso.Dashboard.Application.NewsPortals.Commands.UpdateNewsPortal;
using Espresso.Dashboard.Application.NewsPortals.GetNewsPortalDetails;
using Espresso.Dashboard.Application.NewsPortals.Queries.GetNewsPortalDetails;
using Espresso.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace Espresso.Dashboard.Pages.NewsPortalPages.NewsPortalDetails;

/// <summary>
/// NewsPortalDetailsBase.
/// </summary>
public class NewsPortalDetailsBase : ComponentBase
{
    /// <summary>
    /// Gets <see cref="NewsPortal"/> id.
    /// </summary>
    [Parameter]
    public int Id { get; init; }

    /// <summary>
    /// Gets or sets <see cref="NewsPortal"/> details.
    /// </summary>
    protected GetNewsPortalDetailsQueryResponse? NewsPortalDetailsQueryResponse { get; set; }

    protected GetNewsPortalImageQueryResponse? NewsPortalImageResponse { get; set; }

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

        NewsPortalDetailsQueryResponse = await sender.Send(new GetNewsPortalDetailsQuery(Id));
        var newsPortalImageResponse = await sender.Send(new GetNewsPortalImageQuery(NewsPortalDetailsQueryResponse.NewsPortal.Id));
        NewsPortalImageResponse = newsPortalImageResponse;
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

        var command = new UpdateNewsPortalCommand(NewsPortalDetailsQueryResponse.NewsPortal);
        await sender.Send(command);

        if (NewsPortalImageResponse is not null)
        {
            await sender.Send(new ImportNewsPortalImageCommand(NewsPortalImageResponse.NewsPortalImage));
        }

        NavigationManager.NavigateTo("news-portals");
    }
}
