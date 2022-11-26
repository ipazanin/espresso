// EditSimilarArticlesSettingsBase.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Dashboard.Pages.Settings.EditSettings.State;
using Microsoft.AspNetCore.Components;

namespace Espresso.Dashboard.Pages.Settings.EditSettings.SimilarArticlesSettings;

public class EditSimilarArticlesSettingsBase : ComponentBase
{
    [Parameter]
    [EditorRequired]
    public SimilarArticlesState SimilarArticlesState { get; init; } = null!;

    [Parameter]
    [EditorRequired]
    public EventCallback<(bool IsValid, string uniqueId)> IsValidChanged { get; set; }
}
