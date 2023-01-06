// CreateRegionBase.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects;
using Espresso.Dashboard.Application.Regions.Commands.CreateRegion;
using Espresso.Dashboard.Application.Regions.Queries.GetRegionDetails;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace Espresso.Dashboard.Pages.RegionPages.CreateRegion;

/// <summary>
/// NewsPortalDetailsBase.
/// </summary>
public class CreateRegionBase : ComponentBase
{
    protected GetRegionDetailsQueryResponse RegionDetails { get; set; } = null!;

    protected bool Success { get; set; }

    /// <summary>
    /// Gets <see cref="Mediator"/> request sender.
    /// </summary>
    [Inject]
    private ISender Sender { get; init; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; init; } = null!;

    protected override void OnInitialized()
    {
        var region = new RegionDto(
            id: default,
            name: string.Empty,
            subtitle: string.Empty);

        RegionDetails = new(
            region: region,
            newsPortals: Enumerable.Empty<NewsPortalDto>());

        StateHasChanged();
    }

    protected async Task OnSaveButtonClick()
    {
        if (!Success)
        {
            return;
        }

        if (RegionDetails is null)
        {
            return;
        }

        _ = await Sender.Send(new CreateRegionCommand(RegionDetails.Region));

        NavigationManager.NavigateTo("/categories");
    }

    protected void OnSuccessChanged(bool success)
    {
        Success = success;
    }
}
