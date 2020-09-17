using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Common.Utilities;
using Espresso.Domain.DataTransferObjects;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.Domain.IServices;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Espresso.Application.DomainServices
{
    public class SlackService : ISlackService
    {
        #region Constants
        private const int EspressoDownloadsMileStone = 5000;
        private static readonly TimeSpan s_exceptionMessageCooldownInterval = TimeSpan.FromHours(4);
        private const string WebHookurl = "https://hooks.slack.com/services/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";

        private const string ErrorsBotIconEmoji = ":no_entry:";
        private const string ErrorBotUsername = "error-bot";
        private const string ErrorsChannel = "#errors-backend-bot";

        private const string WarningBotIconEmoji = ":warning:";
        private const string WarningBotUsername = "warning-bot";
        private const string WarningsChannel = "#warnings-backend-bot";
#pragma warning disable IDE0051
        private const string IvanPazaninUserName = "@ipazanin";
#pragma warning disable IDE0051

        private const string AppDownloadsBotIconEmoji = ":tada:";
        private const string AppDownloadsBotUsername = "app-downloads-bot";
        private const string AppDownloadsChannel = "#marketing-bot";

        private const string MissingCategoriesErrorsBotIconEmoji = ":warning:";
        private const string MissingCategoriesErrorsBotUsername = "warning-bot";
        private const string MissingCategoriesErrorsChannel = "#missing-categories-errors-bot";


        private const string NewNewsPortalRequestBotIconEmoji = ":email:";
        private const string NewNewsPortalRequestBotUsername = "new-source-bot";
        private const string NewNewsPortalRequestChannel = "#new-source-requests-bot";
        #endregion

        #region Fields
        private readonly IMemoryCache _memoryCache;
        private readonly HttpClient _httpClient;
        private readonly ILogger<SlackService> _logger;
        #endregion

        #region Contructor
        public SlackService(
            IMemoryCache memoryCache,
            IHttpClientFactory httpClientFactory,
            ILoggerFactory loggerFactory
        )
        {
            _memoryCache = memoryCache;
            _httpClient = httpClientFactory.CreateClient();
            _logger = loggerFactory.CreateLogger<SlackService>();
        }
        #endregion

        #region Methods

        #region  Public Methods
        public async Task LogWarning(
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
                $":exclamation: Inner Exception Message: {innerExceptionMessage}";

            await Log(
                data: new SlackWebHookDto(
                    userName: WarningBotUsername,
                    iconEmoji: WarningBotIconEmoji,
                    text: text,
                    channel: WarningsChannel
                ),
                appEnvironment: appEnvironment,
                cancellationToken: cancellationToken
            );
        }

        public async Task LogError(
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

            await Log(
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

        public async Task LogRequestError(
            string requestName,
            string webVersion,
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
                $":label: App Version: {webVersion}\n" +
                $":label: Targeted WebApi Version: {targetedApiVersion}\n" +
                $":label: Consumer Version: {consumerVersion}\n" +
                $":iphone: Device Type: {deviceType}\n" +
                $":books: Request Parameters: {requestParameters}\n" +
                $":exclamation: Exception Message: {exceptionMessage}\n" +
                $":exclamation: Inner Exception Message: {innerExceptionMessage}\n";

            await Log(
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

        public async Task LogAppDownload(
            string mobileDeviceType,
            int todayAndroidCount,
            int todayIosCount,
            int totalAndroidCount,
            int totalIosCount,
            AppEnvironment appEnvironment,
            CancellationToken cancellationToken
        )
        {
            var text = $"There’s a new {mobileDeviceType} download :tada:\n" +
                $":calendar: Today: Android +{todayAndroidCount}, iOS +{todayIosCount}\n" +
                $":robot_face: Lifetime Android: {totalAndroidCount}\n" +
                $":apple: Lifetime iOS: {totalIosCount}";

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

            _ = await _memoryCache.GetOrCreateAsync(data.Text, async entry =>
              {
                  entry.AbsoluteExpirationRelativeToNow = s_exceptionMessageCooldownInterval;
                  try
                  {
                      var stream = new MemoryStream();
                      await JsonSerializer.SerializeAsync(
                          stream,
                          data,
                          cancellationToken: cancellationToken);

                      stream.Position = 0;
                      using var reader = new StreamReader(stream);
                      var jsonString = await reader.ReadToEndAsync();

                      var content = new StringContent(jsonString, Encoding.UTF8, MimeTypeConstants.Json);
                      var response = await _httpClient.PostAsync(
                          requestUri: WebHookurl,
                          content: content,
                          cancellationToken: cancellationToken
                      );
                  }
                  catch (Exception exception)
                  {
                      var eventName = Event.SlackServiceException.ToString();
                      var exceptionMessage = exception.Message;
                      var innerExceptionMessage = exception.InnerException?.Message ?? "";

                      var formattedMessage =
                        $"{AnsiUtility.EncodeEventName("{0}")}\n\t" +
                        $"{AnsiUtility.EncodeParameterName(nameof(exceptionMessage))}: " +
                        $"{AnsiUtility.EncodeErrorMessage("{2}")}\n\t" +
                        $"{AnsiUtility.EncodeParameterName(nameof(innerExceptionMessage))}: " +
                        $"{AnsiUtility.EncodeErrorMessage("{3}")}";


                      var args = new object[]
                      {
                          eventName,
                          exceptionMessage,
                          innerExceptionMessage,
                      };

                      _logger.LogError(
                          eventId: new EventId(
                              id: (int)Event.SlackServiceException,
                              name: eventName
                          ),
                          exception: exception,
                          message: formattedMessage,
                          args: args
                      );
                  }

                  return "";
              });
        }
        #endregion

        #endregion
    }
}
