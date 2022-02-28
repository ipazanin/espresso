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

    protected string AnalyticsCronExpression
    {
        get => SettingsState.JobsSettingState.AnalyticsCronExpression;
        set
        {
            SettingsState.JobsSettingState.AnalyticsCronExpression = value;
            try
            {
                _ = CronExpression.Parse(value);
                AnalyticsCronExpressionValidationClass = "is-valid";
                SettingsState.JobsSettingState.IsFormValid &= true;
            }
            catch
            {
                AnalyticsCronExpressionValidationClass = "is-invalid";
                SettingsState.JobsSettingState.IsFormValid = false;
            }
        }
    }

    protected string AnalyticsCronExpressionValidationClass { get; set; } = string.Empty;

    protected string ParseArticlesCronExpression
    {
        get => SettingsState.JobsSettingState.ParseArticlesCronExpression;
        set
        {
            SettingsState.JobsSettingState.ParseArticlesCronExpression = value;
            try
            {
                _ = CronExpression.Parse(value);
                ParseArticlesCronExpressionValidationClass = "is-valid";
                SettingsState.JobsSettingState.IsFormValid &= true;
            }
            catch
            {
                ParseArticlesCronExpressionValidationClass = "is-invalid";
                SettingsState.JobsSettingState.IsFormValid = false;
            }
        }
    }

    protected string ParseArticlesCronExpressionValidationClass { get; set; } = string.Empty;

    protected string BackendStatisticsCronExpression
    {
        get => SettingsState.JobsSettingState.WebApiReportCronExpression;
        set
        {
            SettingsState.JobsSettingState.WebApiReportCronExpression = value;
            try
            {
                _ = CronExpression.Parse(value);
                BackendStatisticsCronExpressionValidationClass = "is-valid";
                SettingsState.JobsSettingState.IsFormValid &= true;
            }
            catch
            {
                BackendStatisticsCronExpressionValidationClass = "is-invalid";
                SettingsState.JobsSettingState.IsFormValid = false;
            }
        }
    }

    protected string BackendStatisticsCronExpressionValidationClass { get; set; } = string.Empty;
}
