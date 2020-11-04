using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.IServices;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Common.Utilities;
using Espresso.Application.DataTransferObjects;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Espresso.Application.Services
{
    public class SlackService : ISlackService
    {
        #region Constants
        private const int EspressoDownloadsMileStone = 10000;
        private static readonly TimeSpan s_exceptionMessageCooldownInterval = TimeSpan.FromHours(4);

        private const string ErrorsBotIconEmoji = ":no_entry:";
        private const string ErrorBotUsername = "error-bot";
        private const string ErrorsChannel = "#errors-backend-bot";

#pragma warning disable IDE0051
        private const string WarningBotIconEmoji = ":warning:";
        private const string WarningBotUsername = "warning-bot";
        private const string WarningsChannel = "#warnings-backend-bot";
        private const string IvanPazaninUserName = "@ipazanin";

        private const string MissingCategoriesErrorsBotIconEmoji = ":warning:";
        private const string MissingCategoriesErrorsBotUsername = "warning-bot";
        private const string MissingCategoriesErrorsChannel = "#missing-categories-errors-bot";

#pragma warning restore IDE0051

        private const string AppDownloadsBotIconEmoji = ":tada:";
        private const string AppDownloadsBotUsername = "app-downloads-bot";
        private const string AppDownloadsChannel = "#marketing-bot";

        private const string NewNewsPortalRequestBotIconEmoji = ":email:";
        private const string NewNewsPortalRequestBotUsername = "new-source-bot";
        private const string NewNewsPortalRequestChannel = "#new-source-requests-bot";

        private const string BackendStatisticsBotIconEmoji = ":bar_chart:";
        private const string BackendStatisticsBotUsername = "backend-bot";
        private const string BackendStatisticsChannel = "#backend-statistics";
        #endregion

        #region Fields
        private readonly IMemoryCache _memoryCache;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoggerService<SlackService> _loggerService;
        private readonly string _webHookUrl;
        #endregion

        #region Contructor
        public SlackService(
            IMemoryCache memoryCache,
            IHttpClientFactory httpClientFactory,
            ILoggerService<SlackService> loggerService,
            string webHookUrl
        )
        {
            _memoryCache = memoryCache;
            _httpClientFactory = httpClientFactory;
            _loggerService = loggerService;
            _webHookUrl = webHookUrl;
        }
        #endregion

        #region Methods

        #region  Public Methods
        public Task LogError(
            string eventName,
            string version,
            string message,
            Exception exception,
            AppEnvironment appEnvironment,
            CancellationToken cancellationToken
        )
        {
            var exceptionMessage = exception.Message;
            var innerExceptionMessage = exception.InnerException?.Message ?? FormatConstants.EmptyValue;
            var text = $":blue_book: Event Name: {eventName}\n" +
                $":label: Version: {version}\n" +
                $":email: Message: {message}\n" +
                $":exclamation: Exception Message: {exceptionMessage}\n" +
                $":exclamation: Inner Exception Message: {innerExceptionMessage}\n";

            return Log(
                data: new SlackWebHookDto(
                    userName: ErrorBotUsername,
                    iconEmoji: ErrorsBotIconEmoji,
                    text: text,
                    channel: ErrorsChannel
                ),
                appEnvironment: appEnvironment,
                cancellationToken: cancellationToken
            );
        }

        public Task LogRequestError(
            string requestName,
            string apiVersion,
            string targetedApiVersion,
            string consumerVersion,
            DeviceType deviceType,
            string requestParameters,
            Exception exception,
            AppEnvironment appEnvironment,
            CancellationToken cancellationToken
        )
        {
            var exceptionMessage = exception.Message;
            var innerExceptionMessage = exception.InnerException?.Message ?? FormatConstants.EmptyValue;
            var text = $":blue_book: Request Name: {requestName}\n" +
                $":label: App Version: {apiVersion}\n" +
                $":label: Targeted WebApi Version: {targetedApiVersion}\n" +
                $":label: Consumer Version: {consumerVersion}\n" +
                $":iphone: Device Type: {deviceType}\n" +
                $":books: Request Parameters: {requestParameters}\n" +
                $":exclamation: Exception Message: {exceptionMessage}\n" +
                $":exclamation: Inner Exception Message: {innerExceptionMessage}\n";

            return Log(
                data: new SlackWebHookDto(
                    userName: ErrorBotUsername,
                    iconEmoji: ErrorsBotIconEmoji,
                    text: text,
                    channel: ErrorsChannel
                ),
                appEnvironment: appEnvironment,
                cancellationToken: cancellationToken
            );
        }

        public async Task LogAppDownloadStatistics(
            int yesterdayAndroidCount,
            int yesterdayIosCount,
            int totalAndroidCount,
            int totalIosCount,
            AppEnvironment appEnvironment,
            CancellationToken cancellationToken
        )
        {
            var text = $":calendar: Yesterday: Android +{yesterdayAndroidCount}, " +
                $"iOS +{yesterdayIosCount}, total: +{yesterdayAndroidCount + yesterdayIosCount}\n" +
                $":robot_face: Lifetime Android: {totalAndroidCount}\n" +
                $":apple: Lifetime iOS: {totalIosCount}\n" +
                $":chart_with_upwards_trend: Lifetime: {totalAndroidCount + totalIosCount}";

            await Log(
                data: new SlackWebHookDto(
                    userName: AppDownloadsBotUsername,
                    iconEmoji: AppDownloadsBotIconEmoji,
                    text: text,
                    channel: AppDownloadsChannel
                ),
                appEnvironment: appEnvironment,
                cancellationToken: cancellationToken
            );

            if (totalAndroidCount + totalIosCount == EspressoDownloadsMileStone)
            {
                await Log(
                    data: new SlackWebHookDto(
                        userName: "Alkos-Bot",
                        iconEmoji: ":fire:",
                        text: $"@channel {EspressoDownloadsMileStone} Downloadova, Woooooo espresso ide pit " +
                        ":champagne::beer::beers::tropical_drink::clinking_glasses::wine_glass::cocktail::tumbler_glass:",
                        channel: AppDownloadsChannel
                    ),
                    appEnvironment: appEnvironment,
                    cancellationToken: cancellationToken
                );
            }
        }

        public Task LogMissingCategoriesError(
            string version,
            string rssFeedUrl,
            string articleUrl,
            string urlCategories,
            AppEnvironment appEnvironment,
            CancellationToken cancellationToken
        )
        {
            var text = $":blue_book: Request Name: Missing Categories\n" +
                $":label: Version: {version}\n" +
                $":email: Rss Feed Url: {rssFeedUrl}\n" +
                $":email: Article Url: {articleUrl}\n" +
                $":email: Url-SegmentIndex:Category Map: {urlCategories}\n";

            return Log(
                data: new SlackWebHookDto(
                    userName: MissingCategoriesErrorsBotUsername,
                    iconEmoji: MissingCategoriesErrorsBotIconEmoji,
                    text: text,
                    channel: MissingCategoriesErrorsChannel
                ),
                appEnvironment: appEnvironment,
                cancellationToken: cancellationToken
            );
        }

        public Task LogNewNewsPortalRequest(
            string newsPortalName,
            string email,
            string? url,
            AppEnvironment appEnvironment,
            CancellationToken cancellationToken
        )
        {
            var text = $"There’s a request for new source\n" +
                $"Source Name: {newsPortalName}\n" +
                $"Email: {email}\n" +
                $"Url: {url}";

            return Log(
                data: new SlackWebHookDto(
                    userName: NewNewsPortalRequestBotUsername,
                    iconEmoji: NewNewsPortalRequestBotIconEmoji,
                    text: text,
                    channel: NewNewsPortalRequestChannel
                ),
                appEnvironment: appEnvironment,
                cancellationToken: cancellationToken
            );
        }

        public Task LogPerformance(
            string applicationName,
            IEnumerable<(string name, int count, TimeSpan duration)> data,
            AppEnvironment appEnvironment,
            CancellationToken cancellationToken
        )
        {
            var textBuilder = new StringBuilder($"{applicationName} Performance\n");

            foreach (var (name, count, duration) in data)
            {
                textBuilder.Append(
                    $"{name}\n" +
                    $"\t:chart_with_upwards_trend: Daily Count: {count}\n" +
                    $"\t:clock1: Duration: {duration}\n\n"
                );
            }

            return Log(
                data: new SlackWebHookDto(
                    userName: BackendStatisticsBotUsername,
                    iconEmoji: BackendStatisticsBotIconEmoji,
                    text: textBuilder.ToString(),
                    channel: BackendStatisticsChannel
                ),
                appEnvironment: appEnvironment,
                cancellationToken: cancellationToken
            );
        }

        public Task LogTopArticles(
            IEnumerable<(string title, int numberOfClicks, string newsPortalName, DateTime publishDateTime)> topArticles,
            AppEnvironment appEnvironment,
            CancellationToken cancellationToken
        )
        {
            var textBuilder = new StringBuilder($"Top Articles:\n");

            foreach (var (title, numberOfClicks, newsPortalName, publishDateTime) in topArticles)
            {
                textBuilder.Append(
                    $"Title: {title}\n" +
                    $"Number Of Clicks: {numberOfClicks}\n" +
                    $"Source Name: {newsPortalName}\n" +
                    $"Publish Date: {publishDateTime.ToShortDateString()}\n\n"
                );
            }

            return Log(
                data: new SlackWebHookDto(
                    userName: BackendStatisticsBotUsername,
                    iconEmoji: BackendStatisticsBotIconEmoji,
                    text: textBuilder.ToString(),
                    channel: BackendStatisticsChannel
                ),
                appEnvironment: appEnvironment,
                cancellationToken: cancellationToken
            );
        }
        #endregion

        #region Private Methods
        private async Task Log(
                SlackWebHookDto data,
                AppEnvironment appEnvironment,
                CancellationToken cancellationToken
            )
        {
            if (!appEnvironment.Equals(AppEnvironment.Prod))
            {
                return;
            }

            var httpClient = _httpClientFactory.CreateClient();

            _ = await _memoryCache.GetOrCreateAsync(data.Text, async entry =>
              {
                  entry.AbsoluteExpirationRelativeToNow = s_exceptionMessageCooldownInterval;
                  try
                  {
                      var jsonString = await JsonUtility.Serialize(data, cancellationToken);

                      var content = new StringContent(jsonString, Encoding.UTF8, MimeTypeConstants.Json);
                      var response = await httpClient.PostAsync(
                          requestUri: _webHookUrl,
                          content: content,
                          cancellationToken: cancellationToken
                      );

                      response.EnsureSuccessStatusCode();
                  }
                  catch (Exception exception)
                  {
                      var eventName = Event.SlackServiceException.ToString();
                      var exceptionMessage = exception.Message;
                      var innerExceptionMessage = exception.InnerException?.Message ?? "";

                      var arguments = new List<(string parameterName, object parameterValue)>
                      {
                          (nameof(exceptionMessage),exceptionMessage),
                          (nameof(innerExceptionMessage),innerExceptionMessage),
                      };

                      _loggerService.Log(eventName, exception, LogLevel.Error, arguments);
                  }

                  return "";
              });
        }
        #endregion

        #endregion
    }
}
