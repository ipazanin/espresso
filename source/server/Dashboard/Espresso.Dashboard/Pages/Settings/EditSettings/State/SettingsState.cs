// SettingsState.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.Dashboard.Pages.Settings.EditSettings.State;

public class SettingsState
{
    public JobsSettingState JobsSettingState { get; } = new();

    public ArticleSettingState ArticleSettingState { get; } = new();

    public SimilarArticlesState SimilarArticlesState { get; } = new();

    public NewsPortalSettingState NewsPortalSettingState { get; } = new();
}
