// AnalyticsCronJob.cs
//
// © 2022 Espresso News. All rights reserved.

using Cronos;
using Espresso.Application.Infrastructure.CronJobsInfrastructure;
using Espresso.Application.Models;
using Espresso.Application.Services.Contracts;
using Espresso.Common.Enums;
using Espresso.Domain.Entities;
using Espresso.Domain.Infrastructure;
using Espresso.Domain.Utilities;
using Espresso.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Espresso.WebApi.Jobs.CronJobs;

/// <summary>
///
/// </summary>
public class AnalyticsCronJob : CronJob<AnalyticsCronJob>
{
    private readonly IServiceScopeFactory _scopeFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="AnalyticsCronJob"/> class.
    /// </summary>
    /// <param name="serviceScopeFactory"></param>
    /// <param name="cronJobConfiguration"></param>
    public AnalyticsCronJob(
        IServiceScopeFactory serviceScopeFactory,
        ICronJobConfiguration cronJobConfiguration)
        : base(
        cronJobConfiguration: cronJobConfiguration,
        serviceScopeFactory: serviceScopeFactory)
    {
        _scopeFactory = serviceScopeFactory;
    }

    protected override CronExpression CronExpression
    {
        get
        {
            using var scope = _scopeFactory.CreateScope();
            var settingProvider = scope.ServiceProvider.GetRequiredService<ISettingProvider>();

            return CronExpression.Parse(settingProvider.LatestSetting.JobsSetting.AnalyticsCronExpression);
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="cancellationToken"></param>
    public override async Task DoWork(CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var espressoDatabaseContext = serviceProvider.GetRequiredService<IEspressoDatabaseContext>();
        var slackService = serviceProvider.GetRequiredService<ISlackService>();
        var googleAnalyticsService = serviceProvider.GetRequiredService<IGoogleAnalyticsService>();

        var applicationDownloads = await espressoDatabaseContext.ApplicationDownload.ToListAsync(cancellationToken);
        var (todayAndroidCount, todayIosCount, totalAndroidCount, totalIosCount) = CalculateAppDownloadsPerDeviceType(applicationDownloads);

        var (activeUsersOnIos, activeUsersOnAndroid) = await googleAnalyticsService.GetNumberOfActiveUsersFromYesterday();
        var (androidRevenueToday, iosRevenueToday) = await googleAnalyticsService.GetTotalRevenueFromYesterday();
        var (androidRevenueCurrentMonth, iosRevenueCurrentMonth) = await googleAnalyticsService.GetTotalRevenueForCurrentMonth();
        var (androidRevenuePreviousMonth, iosRevenuePreviousMonth) = await googleAnalyticsService.GetTotalRevenueForPreviousMonth();

        var applicationStatistics = new ApplicationStatistics(
            yesterdayAndroidCount: todayAndroidCount,
            yesterdayIosCount: todayIosCount,
            totalAndroidCount: totalAndroidCount,
            totalIosCount: totalIosCount,
            activeUsersOnAndroid: activeUsersOnAndroid,
            activeUsersOnIos: activeUsersOnIos,
            androidRevenueToday: androidRevenueToday,
            iosRevenueToday: iosRevenueToday,
            revenueCurrentMonth: androidRevenueCurrentMonth + iosRevenueCurrentMonth,
            revenuePreviousMonth: androidRevenuePreviousMonth + iosRevenuePreviousMonth);

        await slackService.LogApplicationStatistics(
                applicationStatistics: applicationStatistics,
                cancellationToken: cancellationToken);
    }

    private static (int todayAndroidCount, int todayIosCount, int totalAndroidCount, int totalIosCount) CalculateAppDownloadsPerDeviceType(
        IEnumerable<ApplicationDownload> applicationDownloads)
    {
        var todayAndroidCount = applicationDownloads.Count(applicationDownloads =>
            applicationDownloads.MobileDeviceType == DeviceType.Android &&
            applicationDownloads.DownloadedTime.Date == DateTimeUtility.YesterdaysDate);

        var todayIosCount = applicationDownloads.Count(applicationDownloads =>
            applicationDownloads.MobileDeviceType == DeviceType.Ios &&
            applicationDownloads.DownloadedTime.Date == DateTimeUtility.YesterdaysDate);

        var totalIosCount = applicationDownloads.Count(applicationDownloads =>
            applicationDownloads.MobileDeviceType == DeviceType.Ios &&
            applicationDownloads.DownloadedTime.Date <= DateTimeUtility.YesterdaysDate);
        var totalAndroidCount = applicationDownloads.Count(applicationDownloads =>
            applicationDownloads.MobileDeviceType == DeviceType.Android &&
            applicationDownloads.DownloadedTime.Date <= DateTimeUtility.YesterdaysDate);

        return (todayAndroidCount, todayIosCount, totalAndroidCount, totalIosCount);
    }
}
