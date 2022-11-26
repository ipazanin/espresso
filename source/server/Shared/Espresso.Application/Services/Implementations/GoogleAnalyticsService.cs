// GoogleAnalyticsService.cs
//
// © 2022 Espresso News. All rights reserved.

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
            .First(row => row.DimensionValues.Any(dimension => dimension.Value == "Android"))
            .MetricValues[0]
            .Value;
        var numberOfActiveUsersOnAndroid = int.Parse(numberOfActiveUsersOnAndroidString);

        var numberOfActiveUsersOnIosString = response
            .Rows
            .First(row => row.DimensionValues.Any(dimension => dimension.Value == "iOS"))
            .MetricValues[0]
            .Value;
        var numberOfActiveUsersOnIos = int.Parse(numberOfActiveUsersOnIosString);

        return (numberOfActiveUsersOnAndroid, numberOfActiveUsersOnIos);
    }

    /// <inheritdoc/>
    public async Task<(decimal androidRevenue, decimal iosRevenue)> GetTotalRevenueFromYesterday()
    {
        var request = new RunReportRequest
        {
            Property = PropertyId,
            Metrics = { new Metric { Name = "totalRevenue" }, },
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

        var androidRevenueString = response
            .Rows
            .First(row => row.DimensionValues.Any(dimension => dimension.Value == "Android"))
            .MetricValues[0]
            .Value;
        var androidRevenue = decimal.Parse(androidRevenueString);

        var iosRevenueString = response
                            .Rows
                            .First(row => row.DimensionValues.Any(dimension => dimension.Value == "iOS"))
                            .MetricValues[0]
                            .Value;
        var iosRevenue = decimal.Parse(iosRevenueString);

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
