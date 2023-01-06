// CreateCategoryBase.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.CategoryDataTransferObjects;
using Espresso.Dashboard.Application.Categories.Commands.UpdateCategory;
using Espresso.Domain.Enums.CategoryEnums;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace Espresso.Dashboard.Pages.CategoryPages.CreateCategory;

/// <summary>
/// NewsPortalDetailsBase.
/// </summary>
public class CreateCategoryBase : ComponentBase
{
    protected CategoryDto Category { get; set; } = null!;

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
        Category = new(
            id: default,
            name: string.Empty,
            color: string.Empty,
            keyWordsRegexPattern: default,
            sortIndex: default,
            position: default,
            categoryType: CategoryType.Normal,
            url: string.Empty);

        StateHasChanged();
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

        _ = await Sender.Send(new CreateCategoryCommand(Category));

        NavigationManager.NavigateTo("/categories");
    }

    protected void OnSuccessChanged(bool success)
    {
        Success = success;
    }
}
