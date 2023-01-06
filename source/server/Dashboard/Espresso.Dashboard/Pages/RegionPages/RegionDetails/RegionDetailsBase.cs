// RegionDetailsBase.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Dashboard.Application.Regions.Commands.UpdateRegion;
using Espresso.Dashboard.Application.Regions.Queries.GetRegionDetails;
using Espresso.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace Espresso.Dashboard.Pages.RegionPages.RegionDetails;

/// <summary>
/// NewsPortalDetailsBase.
/// </summary>
public class RegionDetailsBase : ComponentBase
{
    /// <summary>
    /// Gets <see cref="NewsPortal"/> id.
    /// </summary>
    [Parameter]
    public int Id { get; init; }

    protected bool Success { get; set; }

    protected GetRegionDetailsQueryResponse? RegionDetails { get; set; }

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
        using var scope = ServiceScopeFactory.CreateScope();
        var sender = scope.ServiceProvider.GetRequiredService<ISender>();
        RegionDetails = await sender.Send(new GetRegionDetailsQuery(Id));
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

        using var scope = ServiceScopeFactory.CreateScope();
        var sender = scope.ServiceProvider.GetRequiredService<ISender>();

        _ = await sender.Send(new UpdateRegionCommand(RegionDetails.Region));

        NavigationManager.NavigateTo("/regions");
    }
}
