// JobsSettingDto.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Domain.ValueObjects.SettingsValueObjects;

namespace Espresso.Application.DataTransferObjects.SettingDataTransferObjects;

public class JobsSettingDto
{
    public JobsSettingDto(string analyticsCronExpression, string parseArticlesCronExpression, string webApiReportCronExpression)
    {
        AnalyticsCronExpression = analyticsCronExpression;
        ParseArticlesCronExpression = parseArticlesCronExpression;
        WebApiReportCronExpression = webApiReportCronExpression;
    }

    public static Expression<Func<JobsSetting, JobsSettingDto>> Projection
    {
        get => jobsSetting => new JobsSettingDto(
            jobsSetting.AnalyticsCronExpression,
            jobsSetting.ParseArticlesCronExpression,
            jobsSetting.WebApiReportCronExpression);
    }

    public string AnalyticsCronExpression { get; set; }

    public string ParseArticlesCronExpression { get; set; }

    public string WebApiReportCronExpression { get; set; }

    public JobsSetting CreateJobsSetting()
    {
        return new JobsSetting(
            analyticsCronExpression: AnalyticsCronExpression,
            webApiReportCronExpression: WebApiReportCronExpression,
            parseArticlesCronExpression: ParseArticlesCronExpression);
    }
}
