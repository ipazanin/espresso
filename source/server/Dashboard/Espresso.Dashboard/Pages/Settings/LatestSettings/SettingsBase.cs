// SettingsBase.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Infrastructure;
using Microsoft.AspNetCore.Components;

namespace Espresso.Dashboard.Pages.Settings;

public class SettingsBase : ComponentBase
{
    [Inject]
    protected ISettingProvider SettingProvider { get; init; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; init; } = null!;

    protected void EditSettings()
    {
        var settingId = SettingProvider.LatestSetting.Id;
        NavigationManager.NavigateTo(uri: $"/settings/edit/{settingId}");
    }
}
