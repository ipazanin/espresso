// CategoryDetailsBase.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.CategoryDataTransferObjects;
using Espresso.Dashboard.Application.Categories.Commands.UpdateCategory;
using Espresso.Dashboard.Application.Categories.Queries.GetCategoryDetails;
using Espresso.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace Espresso.Dashboard.Pages.CategoryPages.CategoryDetails;

/// <summary>
/// NewsPortalDetailsBase.
/// </summary>
public class CategoryDetailsBase : ComponentBase
{
    /// <summary>
    /// Gets <see cref="NewsPortal"/> id.
    /// </summary>
    [Parameter]
    public int Id { get; init; }

    protected bool Success { get; set; }

    protected CategoryDto? Category { get; set; }

    /// <summary>
    /// Gets <see cref="Mediator"/> request sender.
    /// </summary>
    [Inject]
    private ISender Sender { get; init; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; init; } = null!;

    /// <inheritdoc/>
    protected override async Task OnInitializedAsync()
    {
        var getCategoryDetailsQueryResponse = await Sender.Send(new GetCategoryDetailsQuery(Id));
        Category = getCategoryDetailsQueryResponse.Category;
    }

    protected async Task OnSaveButtonClick()
    {
        if (!Success)
        {
            return;
        }

        if (Category is null)
        {
            return;
        }

        await Sender.Send(new UpdateCategoryCommand(Category));

        NavigationManager.NavigateTo("/categories");
    }
}
