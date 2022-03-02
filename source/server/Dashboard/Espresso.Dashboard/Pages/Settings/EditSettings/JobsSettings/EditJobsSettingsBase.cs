// EditJobsSettingsBase.cs
//
// © 2021 Espresso News. All rights reserved.

using Cronos;
using Espresso.Dashboard.Pages.Settings.EditSettings.State;
using Microsoft.AspNetCore.Components;

namespace Espresso.Dashboard.Pages.Settings.EditSettings.JobsSettings;

public class EditJobsSettingsBase : ComponentBase
{
    [Parameter]
    [EditorRequired]
    public SettingsState SettingsState { get; init; } = null!;

    [Parameter]
    [EditorRequired]
    public EventCallback<(bool IsValid, string uniqueId)> IsValidChanged { get; set; }
}
