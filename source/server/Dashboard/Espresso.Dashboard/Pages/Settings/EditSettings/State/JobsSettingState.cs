// JobsSettingState.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.Dashboard.Pages.Settings.EditSettings.State;

public class JobsSettingState
{
    public string AnalyticsCronExpression { get; set; } = string.Empty;

    public string ParseArticlesCronExpression { get; set; } = string.Empty;

    public string WebApiReportCronExpression { get; set; } = string.Empty;
}
