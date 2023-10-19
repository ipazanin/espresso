// CountryDetailsBase.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.CountryDataTransferObjects;
using Espresso.Dashboard.Application.NewsPortalImages.Commands.ImportNewsPortalImage;
using Espresso.Dashboard.Application.NewsPortalImages.Queries.GetNewsPortalImage;
using Espresso.Dashboard.Application.NewsPortals.Commands.UpdateNewsPortal;
using Espresso.Dashboard.Application.NewsPortals.GetNewsPortalDetails;
using Espresso.Dashboard.Application.NewsPortals.Queries.GetNewsPortalDetails;
using Espresso.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace Espresso.Dashboard.Pages.CountryPages.CountryDetails;

public class CountryDetailsBase : ComponentBase
{
    [Parameter]
    public int Id { get; init; }

    protected CountryDto? Country { get; set; }

    protected CountryImageDto? CountryImage { get; set; }

    protected bool Success { get; set; }

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

        if (Country is null || CountryImage is null)
        {
            return;
        }

        using var scope = ServiceScopeFactory.CreateAsyncScope();
        var sender = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new UpdateNewsPortalCommand(NewsPortalDetailsQueryResponse.NewsPortal);
        _ = await sender.Send(command);

        if (NewsPortalImageResponse is not null)
            _ = await sender.Send(new ImportNewsPortalImageCommand(NewsPortalImageResponse.NewsPortalImage));

        NavigationManager.NavigateTo("news-portals");
    }
}
