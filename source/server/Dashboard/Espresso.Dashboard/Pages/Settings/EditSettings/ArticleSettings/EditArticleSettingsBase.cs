// EditArticleSettingsBase.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Dashboard.Pages.Settings.EditSettings.State;
using Microsoft.AspNetCore.Components;

namespace Espresso.Dashboard.Pages.Settings.EditSettings.ArticleSettings;

public class EditArticleSettingsBase : ComponentBase
{
    [Parameter]
    [EditorRequired]
    public SettingsState SettingsState { get; init; } = null!;
}
