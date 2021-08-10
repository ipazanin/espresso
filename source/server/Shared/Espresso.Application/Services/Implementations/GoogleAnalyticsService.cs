// GoogleAnalyticsService.cs
//
// © 2021 Espresso News. All rights reserved.

using System;
using System.IO;
using System.Threading.Tasks;
using Espresso.Application.Services.Contracts;
using Espresso.Domain.IServices;
using Google.Analytics.Data.V1Beta;
using Microsoft.Extensions.Logging;

namespace Espresso.Application.Services.Implementations
{
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
            ILoggerService<GoogleAnalyticsService> loggerService
        )
        {
            var googleAnalyticsSecretsFilePath = Path.Combine(
                path1: AppDomain.CurrentDomain.BaseDirectory ?? string.Empty,
                path2: GoogleAnalyticsSecretsFileName
            );

            _loggerService = loggerService;
            _client = new BetaAnalyticsDataClientBuilder
            {
                CredentialsPath = googleAnalyticsSecretsFilePath,
            }.Build();
        }

        /// <inheritdoc/>
        public async Task<int> GetNumberOfActiveUsersFromYesterday()
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
            };

            var response = await RunReport(request);
            var numberOfActiveUsersString = response.Rows[0].MetricValues[0].Value;
            var numberOfActiveUsers = int.Parse(numberOfActiveUsersString);

            return numberOfActiveUsers;
        }

        /// <inheritdoc/>
        public async Task<decimal> GetTotalRevenueFromYesterday()
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
            };

            var response = await RunReport(request);
            var revenueString = response.Rows[0].MetricValues[0].Value;
            var revenue = decimal.Parse(revenueString);

            return revenue;
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
                    logLevel: LogLevel.Warning
                );

                throw;
            }
        }
    }
}
