// EditNewsPortalSettingsBase.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Dashboard.Pages.Settings.EditSettings.State;
using Microsoft.AspNetCore.Components;

namespace Espresso.Dashboard.Pages.Settings.EditSettings.NewsPortalSettings;

public class EditNewsPortalSettingsBase : ComponentBase
{
    [Parameter]
    [EditorRequired]
    public NewsPortalSettingState NewsPortalSettingState { get; init; } = null!;

    [Parameter]
    [EditorRequired]
    public EventCallback<(bool IsValid, string uniqueId)> IsValidChanged { get; set; }
}
