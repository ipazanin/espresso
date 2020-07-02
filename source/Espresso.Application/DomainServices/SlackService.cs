using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.Domain.IServices;
using Espresso.Domain.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.Application.DomainServices
{
    public class SlackService : ISlackService
    {
        #region Constants
        public const int EspressoDownloadsMileStone = 5000;
        public static TimeSpan ExceptionMessageCooldownInterval = TimeSpan.FromHours(4);
        public const string WebHookurl = "https://hooks.slack.com/services/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";

        public const string ErrorsBotIconEmoji = ":no_entry:";
        public const string ErrorBotUsername = "error-bot";
        public const string ErrorsChannel = "#errors-backend-bot";

        public const string WarningBotIconEmoji = ":warning:";
        public const string WarningBotUsername = "warning-bot";
        public const string WarningsChannel = "#warnings-backend-bot";
        public const string IvanPazaninUserName = "@ipazanin";

        public const string AppDownloadsBotIconEmoji = ":tada:";
        public const string AppDownloadsBotUsername = "app-downloads-bot";
        public const string AppDownloadsChannel = "#marketing-bot";

        public const string MissingCategoriesErrorsBotIconEmoji = ":warning:";
        public const string MissingCategoriesErrorsBotUsername = "warning-bot";
        public const string MissingCategoriesErrorsChannel = "missing-categories-errors-bot";
        #endregion

        #region Fields
        private readonly IMemoryCache _memoryCache;
        private readonly HttpClient _httpClient;
        #endregion

        #region Contructor
        public SlackService(
            IMemoryCache memoryCache,
            IHttpClientFactory httpClientFactory
        )
        {
            _memoryCache = memoryCache;
            _httpClient = httpClientFactory.CreateClient();
        }
        #endregion

        #region Methods

        #region  Public Methods
        public async Task LogWarning(
            string eventName,
            string version,
            string message,
            Exception exception,
            CancellationToken cancellationToken
        )
        {
            var exceptionMessage = exception.Message;
            var innerExceptionMessage = exception.InnerException?.Message ?? FormatConstants.EmptyValue;

            var text = $":blue_book: Request Name: {eventName}\n" +
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
                cancellationToken: cancellationToken
            ).ConfigureAwait(false);
        }

        public async Task LogError(
            string eventName,
            string version,
            string message,
            Exception exception,
            CancellationToken cancellationToken
        )
        {
            var exceptionMessage = exception.Message;
            var innerExceptionMessage = exception.InnerException?.Message ?? FormatConstants.EmptyValue;
            var exceptionStackTrace = exception.StackTrace ?? "";
            var innerExceptionStackTrace = exception.InnerException?.StackTrace ?? FormatConstants.EmptyValue;
            var text = $":blue_book: Event Name: {eventName}\n" +
                $":label: Version: {version}\n" +
                $":email: Message: {message}\n" +
                $":exclamation: Exception Message: {exceptionMessage}\n" +
                $":chart: Stack Trace: {exceptionStackTrace}\n" +
                $":exclamation: Inner Exception Message: {innerExceptionMessage}\n" +
                $":chart: Inner Stack Trace: {innerExceptionStackTrace}\n";

            await Log(
                data: new SlackWebHookDto(
                    userName: ErrorBotUsername,
                    iconEmoji: ErrorsBotIconEmoji,
                    text: text,
                    channel: ErrorsChannel
                ),
                cancellationToken: cancellationToken
            ).ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task LogRequestError(
            string requestName,
            string webApiVersion,
            string targetedWebApiVersion,
            string consumerVersion,
            DeviceType deviceType,
            string requestParameters,
            Exception exception,
            CancellationToken cancellationToken
        )
        {
            var exceptionMessage = exception.Message;
            var innerExceptionMessage = exception.InnerException?.Message ?? FormatConstants.EmptyValue;
            var exceptionStackTrace = exception.StackTrace;
            var innerExceptionStackTrace = exception.InnerException?.StackTrace ?? FormatConstants.EmptyValue;
            var text = $":blue_book: Request Name: {requestName}\n" +
                $":label: WebApi Version: {webApiVersion}\n" +
                $":label: Targeted WebApi Version: {targetedWebApiVersion}\n" +
                $":label: Consumer Version: {consumerVersion}\n" +
                $":iphone: Device Type: {deviceType}\n" +
                $":books: Request Parameters: {requestParameters}\n" +
                $":exclamation: Exception Message: {exceptionMessage}\n" +
                $":chart: Stack Trace: {exceptionStackTrace}\n" +
                $":exclamation: Inner Exception Message: {innerExceptionMessage}\n" +
                $":chart: Stack Trace: {innerExceptionStackTrace}\n";

            await Log(
                data: new SlackWebHookDto(
                    userName: ErrorBotUsername,
                    iconEmoji: ErrorsBotIconEmoji,
                    text: text,
                    channel: ErrorsChannel
                ),
                cancellationToken: cancellationToken
            ).ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task LogAppDownload(
            string mobileDeviceType,
            int todayAndroidCount,
            int todayIosCount,
            int totalAndroidCount,
            int totalIosCount,
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
                cancellationToken: cancellationToken
            ).ConfigureAwait(false);

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
                    cancellationToken: cancellationToken
                ).ConfigureAwait(false);
            }
        }

        public Task LogMissingCategoriesError(
            string version,
            string rssFeedUrl,
            string articleUrl,
            string urlCategories,
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
                cancellationToken: cancellationToken
            );
        }
        #endregion

        #region Private Methods
        private async Task Log(
            SlackWebHookDto data,
            CancellationToken cancellationToken
        )
        {
            await _memoryCache.GetOrCreateAsync(data.Text, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = ExceptionMessageCooldownInterval;
                var stream = new MemoryStream();
                await JsonSerializer.SerializeAsync(
                    stream,
                    data,
                    cancellationToken: cancellationToken).ConfigureAwait(false);

                stream.Position = 0;
                using var reader = new StreamReader(stream);
                var jsonString = await reader.ReadToEndAsync().ConfigureAwait(false);

                var content = new StringContent(jsonString, Encoding.UTF8, MimeTypeConstants.Json);
                var response = await _httpClient.PostAsync(
                    requestUri: WebHookurl,
                    content: content,
                    cancellationToken: cancellationToken
                ).ConfigureAwait(false);

                return "";
            }).ConfigureAwait(false);
        }
        #endregion

        #endregion
    }
}
