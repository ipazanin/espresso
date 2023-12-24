// GoogleAnalyticsService.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Globalization;
using Espresso.Application.Services.Contracts;
using Espresso.Domain.IServices;
using Google.Analytics.Data.V1Beta;
using Microsoft.Extensions.Logging;

namespace Espresso.Application.Services.Implementations;

/// <summary>
/// GoogleAnalyticsService.
/// </summary>
public class GoogleAnalyticsService : IGoogleAnalyticsService
{
    private const string PropertyId = "properties/232499903";
    private const string GoogleAnalyticsSecretsFileName = "google-analytics-key.json";
    private const string AndroidKey = "Android";
    private const string IosKey = "iOS";
    private const string GoogleAnalyticsApiDateFormat = "yyyy-MM-dd";

    private readonly ILoggerService<GoogleAnalyticsService> _loggerService;
    private readonly BetaAnalyticsDataClient _client;

    /// <summary>
    /// Initializes a new instance of the <see cref="GoogleAnalyticsService"/> class.
    /// GoogleAnalyticsService Constructor.
    /// </summary>
    /// <param name="loggerService">Logger service.</param>
    public GoogleAnalyticsService(
        ILoggerService<GoogleAnalyticsService> loggerService)
    {
        var googleAnalyticsSecretsFilePath = Path.Combine(
            path1: AppDomain.CurrentDomain.BaseDirectory ?? string.Empty,
            path2: GoogleAnalyticsSecretsFileName);

        _loggerService = loggerService;
        _client = new BetaAnalyticsDataClientBuilder
        {
            CredentialsPath = googleAnalyticsSecretsFilePath,
        }.Build();
    }

    /// <inheritdoc/>
    public async Task<(int androidUsers, int iosUsers)> GetNumberOfActiveUsersFromYesterday()
    {
        var request = new RunReportRequest
        {
            Property = PropertyId,
            Metrics = { new Metric { Name = "activeUsers" }, },
            DateRanges =
                {
                    new DateRange
                    {
                        StartDate = "1daysAgo",
                        EndDate = "today",
                    },
                },
            Dimensions = { new Dimension { Name = "operatingSystem" } },
        };

        var response = await RunReport(request);

        var numberOfActiveUsersOnAndroidString = response
            .Rows
            .First(row => row.DimensionValues.Any(dimension => dimension.Value == AndroidKey))
            .MetricValues[0]
            .Value;
        var numberOfActiveUsersOnAndroid = int.Parse(numberOfActiveUsersOnAndroidString, CultureInfo.InvariantCulture);

        var numberOfActiveUsersOnIosString = response
            .Rows
            .First(row => row.DimensionValues.Any(dimension => dimension.Value == IosKey))
            .MetricValues[0]
            .Value;
        var numberOfActiveUsersOnIos = int.Parse(numberOfActiveUsersOnIosString, CultureInfo.InvariantCulture);

        return (numberOfActiveUsersOnAndroid, numberOfActiveUsersOnIos);
    }

    /// <inheritdoc/>
    public Task<(decimal androidRevenue, decimal iosRevenue)> GetTotalRevenueFromYesterday()
    {
        var dateRange = new DateRange
        {
            StartDate = "1daysAgo",
            EndDate = "today",
        };

        return GetRevenue(dateRange);
    }

    public Task<(decimal androidRevenue, decimal iosRevenue)> GetTotalRevenueForCurrentMonth()
    {
        var currentDate = DateTimeOffset.UtcNow.Date;
        var beginningOfCurrentMonth = new DateOnly(currentDate.Year, currentDate.Month, 1);
        var dateRange = new DateRange
        {
            StartDate = beginningOfCurrentMonth.ToString(GoogleAnalyticsApiDateFormat, CultureInfo.InvariantCulture),
            EndDate = "today",
        };

        return GetRevenue(dateRange);
    }

    public Task<(decimal androidRevenue, decimal iosRevenue)> GetTotalRevenueForPreviousMonth()
    {
        var currentDate = DateTimeOffset.UtcNow.Date;
        var beginningOfCurrentMonth = new DateOnly(currentDate.Year, currentDate.Month, 1);
        var beginningOfPreviousMonth = beginningOfCurrentMonth.AddMonths(-1);
        var endingOfPreviousMonth = beginningOfCurrentMonth.AddDays(-1);

        var dateRange = new DateRange
        {
            StartDate = beginningOfPreviousMonth.ToString(GoogleAnalyticsApiDateFormat, CultureInfo.InvariantCulture),
            EndDate = endingOfPreviousMonth.ToString(GoogleAnalyticsApiDateFormat, CultureInfo.InvariantCulture),
        };

        return GetRevenue(dateRange);
    }

    private async Task<(decimal androidRevenue, decimal iosRevenue)> GetRevenue(DateRange dateRange)
    {
        var request = new RunReportRequest
        {
            Property = PropertyId,
            Metrics = { new Metric { Name = "totalRevenue" }, },
            DateRanges = { dateRange },
            Dimensions = { new Dimension { Name = "operatingSystem" } },
        };

        var response = await RunReport(request);

        var androidRevenueString = response
            .Rows
            .First(row => row.DimensionValues.Any(dimension => dimension.Value == AndroidKey))
            .MetricValues[0]
            .Value;
        var androidRevenue = decimal.Parse(androidRevenueString, System.Globalization.CultureInfo.InvariantCulture);

        var iosRevenueString = response
                            .Rows
                            .First(row => row.DimensionValues.Any(dimension => dimension.Value == IosKey))
                            .MetricValues[0]
                            .Value;
        var iosRevenue = decimal.Parse(iosRevenueString, System.Globalization.CultureInfo.InvariantCulture);

        return (androidRevenue, iosRevenue);
    }

    private async Task<RunReportResponse> RunReport(RunReportRequest request)
    {
        try
        {
            var response = await _client.RunReportAsync(request);

            return response;
        }
        catch (Exception exception)
        {
            _loggerService.Log(
                eventName: "GoogleAnalyticsRequestFailure",
                exception: exception,
                logLevel: LogLevel.Warning);

            throw;
        }
    }
}
