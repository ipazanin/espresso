using System;
using System.IO;
using System.Threading.Tasks;
using Espresso.Application.Services.Contracts;
using Espresso.Domain.IServices;
using Google.Analytics.Data.V1Alpha;
using Microsoft.Extensions.Logging;

namespace Espresso.Application.Services.Implementations
{
    /// <summary>
    /// GoogleAnalyticsService
    /// </summary>
    public class GoogleAnalyticsService : IGoogleAnalyticsService
    {
        #region Constants

        private const string PropertyId = "232499903";
        private const string GoogleAnalyticsSecretsFileName = "google-analytics-key.json";

        #endregion Constants

        #region Fields

        private readonly ILoggerService<GoogleAnalyticsService> _loggerService;
        private readonly AlphaAnalyticsDataClient _client;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// GoogleAnalyticsService Constructor
        /// </summary>
        public GoogleAnalyticsService(
            ILoggerService<GoogleAnalyticsService> loggerService
        )
        {
            var googleAnalyticsSecretsFilePath = Path.Combine(
                path1: AppDomain.CurrentDomain.BaseDirectory ?? "",
                path2: GoogleAnalyticsSecretsFileName
            );

            _loggerService = loggerService;
            _client = new AlphaAnalyticsDataClientBuilder
            {
                CredentialsPath = googleAnalyticsSecretsFilePath
            }.Build();
        }

        #endregion Constructors

        #region Methods

        public async Task<int> GetNumberOfActiveUsersFromYesterday()
        {
            var request = new RunReportRequest
            {
                Entity = new Entity { PropertyId = PropertyId },
                Metrics = { new Metric { Name = "activeUsers" }, },
                DateRanges =
                {
                    new DateRange
                    {
                        StartDate = "1daysAgo",
                        EndDate = "today"
                    },
                },
            };

            var response = await RunReport(request);
            var numberOfActiveUsersString = response.Rows[0].MetricValues[0].Value;
            var numberOfActiveUsers = int.Parse(numberOfActiveUsersString);

            return numberOfActiveUsers;
        }

        public async Task<decimal> GetTotalRevenueFromYesterday()
        {
            var request = new RunReportRequest
            {
                Entity = new Entity { PropertyId = PropertyId },
                Metrics = { new Metric { Name = "totalRevenue" }, },
                DateRanges =
                {
                    new DateRange
                    {
                        StartDate = "1daysAgo",
                        EndDate = "today"
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

        #endregion Methods
    }
}
