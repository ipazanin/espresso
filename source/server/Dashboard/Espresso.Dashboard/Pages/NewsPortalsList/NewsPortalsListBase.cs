// NewsPortalsListBase.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using Espresso.Common.Constants;
using Espresso.Dashboard.Application.NewsPortals.GetNewsPortals;
using Espresso.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Espresso.Dashboard.Pages.NewsPortalsList;

/// <summary>
/// Base class for News Portal List Component.
/// </summary>
[Authorize(Roles = RoleConstants.AdminRoleName)]
public class NewsPortalsListBase : ComponentBase
{
    /// <summary>
    /// Gets <see cref="NewsPortal"/> request response.
    /// </summary>
    protected GetNewsPortalsQueryResponse? Response { get; private set; }

    /// <summary>
    /// Gets or sets paging parameters.
    /// </summary>
    protected PagingParameters PagingParameters { get; set; } = new PagingParameters
    {
        CurrentPage = 1,
        PageSize = 10,
    };

    /// <summary>
    /// Gets <see cref="Mediator"/> request sender.
    /// </summary>
    [Inject]
    private ISender Sender { get; init; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; init; } = null!;

    /// <inheritdoc/>
    protected override Task OnInitializedAsync()
    {
        return FetchNewsPortals();
    }

    /// <summary>
    /// Selects page.
    /// </summary>
    /// <param name="page">Page number.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    protected Task SelectedPage(int page)
    {
        PagingParameters = PagingParameters with { CurrentPage = page };
        return FetchNewsPortals();
    }

    /// <summary>
    /// Deletes <see cref="NewsPortal"/>.
    /// </summary>
    /// <param name="newsPortalId">Id of <see cref="NewsPortal"/> to delete.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
#pragma warning disable RCS1163 // Unused parameter.
#pragma warning disable IDE0060 // Remove unused parameter
    protected Task DeleteNewsPortal(int newsPortalId)
#pragma warning restore IDE0060 // Remove unused parameter
#pragma warning restore RCS1163 // Unused parameter.
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Opens News portals details page.
    /// </summary>
    /// <param name="newsPortalId">Id of <see cref="NewsPortal"/> to open.</param>
    protected void OpenNewsPortalDetails(int newsPortalId)
    {
        NavigationManager.NavigateTo(uri: $"/news-portals/{newsPortalId}");
    }

    /// <summary>
    /// Fetches news portals.
    /// </summary>
    private async Task FetchNewsPortals()
    {
        Response = await Sender.Send(
            request: new GetNewsPortalsQuery(
                pagingParameters: PagingParameters));
    }
}
