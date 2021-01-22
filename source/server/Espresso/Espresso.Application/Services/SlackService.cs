using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.IServices;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Espresso.Domain.IServices;
using Espresso.Domain.Entities;
using System.Linq;
using Espresso.Application.DataTransferObjects.SlackDataTransferObjects;
using Espresso.Application.Models;
using Espresso.Common.IServices;
using Espresso.Dashboard.Application.Constants;

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

        private const string PushNotificationBotIconEmoji = ":bell:";
        private const string PushNotificationBotUsername = "push-bot";
        private const string PushNotificationChannel = "#general";
        #endregion

        #region Fields
        private readonly IMemoryCache _memoryCache;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoggerService<SlackService> _loggerService;
        private readonly IJsonService _jsonService;
        private readonly string _webHookUrl;
        private readonly ApplicationInformation _applicationInformation;
        #endregion

        #region Constructor
        public SlackService(
            IMemoryCache memoryCache,
            IHttpClientFactory httpClientFactory,
            ILoggerService<SlackService> loggerService,
            IJsonService jsonService,
            string webHookUrl,
            ApplicationInformation applicationInformation
        )
        {
            _memoryCache = memoryCache;
            _httpClientFactory = httpClientFactory;
            _loggerService = loggerService;
            _jsonService = jsonService;
            _webHookUrl = webHookUrl;
            _applicationInformation = applicationInformation;
        }
        #endregion

        #region Methods

        #region  Public Methods
        public Task LogError(
            string eventName,
            string message,
            Exception exception,
            CancellationToken cancellationToken
        )
        {
            var exceptionMessage = exception.Message;
            var innerExceptionMessage = exception.InnerException?.Message ?? FormatConstants.EmptyValue;
            var text = $":blue_book: Event Name: {eventName}\n" +
                $":label: Version: {_applicationInformation.Version}\n" +
                $":email: Message: {message}\n" +
                $":exclamation: Exception Message: {exceptionMessage}\n" +
                $":exclamation: Inner Exception Message: {innerExceptionMessage}\n";

            return Log(
                data: new SlackWebHookRequestBodyDto(
                    userName: ErrorBotUsername,
                    iconEmoji: ErrorsBotIconEmoji,
                    text: text,
                    channel: ErrorsChannel,
                    blocks: Array.Empty<SlackBlock>()
                ),

                cancellationToken: cancellationToken
            );
        }

        public async Task LogAppDownloadStatistics(
            int yesterdayAndroidCount,
            int yesterdayIosCount,
            int totalAndroidCount,
            int totalIosCount,
            CancellationToken cancellationToken
        )
        {
            var text = $":calendar: Yesterday: Android +{yesterdayAndroidCount}, " +
                $"iOS +{yesterdayIosCount}, total: +{yesterdayAndroidCount + yesterdayIosCount}\n" +
                $":robot_face: Lifetime Android: {totalAndroidCount}\n" +
                $":apple: Lifetime iOS: {totalIosCount}\n" +
                $":chart_with_upwards_trend: Lifetime: {totalAndroidCount + totalIosCount}";

            await Log(
                data: new SlackWebHookRequestBodyDto(
                    userName: AppDownloadsBotUsername,
                    iconEmoji: AppDownloadsBotIconEmoji,
                    text: text,
                    channel: AppDownloadsChannel,
                    blocks: Array.Empty<SlackBlock>()
                ),

                cancellationToken: cancellationToken
            );

            if (totalAndroidCount + totalIosCount == EspressoDownloadsMileStone)
            {
                await Log(
                    data: new SlackWebHookRequestBodyDto(
                        userName: "Alkos-Bot",
                        iconEmoji: ":fire:",
                        text: $"@channel {EspressoDownloadsMileStone} Downloadova, Woooooo espresso ide pit " +
                        ":champagne::beer::beers::tropical_drink::clinking_glasses::wine_glass::cocktail::tumbler_glass:",
                        channel: AppDownloadsChannel,
                    blocks: Array.Empty<SlackBlock>()
                    ),
                    cancellationToken: cancellationToken
                );
            }
        }

        public Task LogMissingCategoriesError(
            string rssFeedUrl,
            string articleUrl,
            string urlCategories,
            CancellationToken cancellationToken
        )
        {
            var text = $":blue_book: Request Name: Missing Categories\n" +
                $":email: Rss Feed Url: {rssFeedUrl}\n" +
                $":email: Article Url: {articleUrl}\n" +
                $":email: Url-SegmentIndex:Category Map: {urlCategories}\n";

            return Log(
                data: new SlackWebHookRequestBodyDto(
                    userName: MissingCategoriesErrorsBotUsername,
                    iconEmoji: MissingCategoriesErrorsBotIconEmoji,
                    text: text,
                    channel: MissingCategoriesErrorsChannel,
                    blocks: Array.Empty<SlackBlock>()
                ),
                cancellationToken: cancellationToken
            );
        }

        public Task LogNewNewsPortalRequest(
            string newsPortalName,
            string email,
            string? url,
            CancellationToken cancellationToken
        )
        {
            var text = $"There’s a request for new source\n" +
                $"Source Name: {newsPortalName}\n" +
                $"Email: {email}\n" +
                $"Url: {url}";

            return Log(
                data: new SlackWebHookRequestBodyDto(
                    userName: NewNewsPortalRequestBotUsername,
                    iconEmoji: NewNewsPortalRequestBotIconEmoji,
                    text: text,
                    channel: NewNewsPortalRequestChannel,
                    blocks: Array.Empty<SlackBlock>()
                ),
                cancellationToken: cancellationToken
            );
        }

        public Task LogPerformance(
            string applicationName,
            IEnumerable<(string name, int count, TimeSpan duration)> data,
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
                data: new SlackWebHookRequestBodyDto(
                    userName: BackendStatisticsBotUsername,
                    iconEmoji: BackendStatisticsBotIconEmoji,
                    text: textBuilder.ToString(),
                    channel: BackendStatisticsChannel,
                    blocks: Array.Empty<SlackBlock>()
                ),
                cancellationToken: cancellationToken
            );
        }

        public async Task LogYesterdaysStatistics(
            IEnumerable<Article> topArticles,
            int totalNumberOfClicks,
            IEnumerable<(NewsPortal newsPortal, int numberOfClicks, IEnumerable<Article> articles)> topNewsPortals,
            IEnumerable<(Category category, int numberOfClicks, IEnumerable<Article> articles)> categoriesWithNumberOfClicks,
            CancellationToken cancellationToken
        )
        {
            var blocks = new List<SlackBlock>
            {
                new SlackHeaderBlock(new SlackPlainTextBlock("Total clicks yesterday:")),
                new SlackTextSectionBlock(new SlackMarkdownTextBlock(totalNumberOfClicks.ToString())),
                new SlackHeaderBlock(new SlackPlainTextBlock("Top Articles")),
            };

            foreach (var article in topArticles)
            {
                var articleBlock = new SlackTextFieldsImageSectionBlock(
                    text: new SlackMarkdownTextBlock($"<{article.Url}|{article.Title}>"),
                    fields: new List<SlackMarkdownTextBlock>()
                    {
                        new SlackMarkdownTextBlock($"*Source*"),
                        new SlackMarkdownTextBlock($"<{article.NewsPortal!.BaseUrl}|{article.NewsPortal!.Name}>"),
                        new SlackMarkdownTextBlock($"*Category*"),
                        new SlackMarkdownTextBlock(article.ArticleCategories.First().Category!.Name),
                        new SlackMarkdownTextBlock($"*Publish Time*"),
                        new SlackMarkdownTextBlock(article.PublishDateTime.ToShortTimeString()),
                        new SlackMarkdownTextBlock($"*Number Of Clicks*"),
                        new SlackMarkdownTextBlock(article.NumberOfClicks.ToString()),
                        new SlackMarkdownTextBlock($"*Clicks ‰*"),
                        new SlackMarkdownTextBlock($"{Math.Round(totalNumberOfClicks == 0 ? 0 : 100 * article.NumberOfClicks / (double)totalNumberOfClicks, 2)}‰"),

                    },
                    accessory: new SlackImageBlock(
                        imageUrl: article.ImageUrl ?? article.NewsPortal?.IconUrl ?? "https://via.placeholder.com/350x150.jpg",
                        altText: "article image"
                    )
                );

                blocks.Add(articleBlock);
                blocks.Add(new SlackDividerBlock());
            }

            await Log(
                data: new SlackWebHookRequestBodyDto(
                    userName: BackendStatisticsBotUsername,
                    iconEmoji: BackendStatisticsBotIconEmoji,
                    text: "Articles",
                    channel: BackendStatisticsChannel,
                    blocks: blocks
                ),
                cancellationToken: cancellationToken
            );

            #region TODO: Move to another method and add cron job to execute once a week
            // blocks.Clear();
            // blocks.Add(new SlackHeaderBlock(new SlackPlainTextBlock("Top News Portals")));

            // foreach (var (newsPortal, numberOfClicks, articles) in topNewsPortals)
            // {
            //     var numberOfArticles = articles.Count();
            //     var newsPortalsBlock = new SlackTextFieldsImageSectionBlock(
            //         text: new SlackMarkdownTextBlock($"<{newsPortal.BaseUrl}|{newsPortal.Name}>"),
            //         fields: new List<SlackMarkdownTextBlock>
            //         {
            //             new SlackMarkdownTextBlock("*Category*"),
            //             new SlackMarkdownTextBlock(newsPortal.Category!.Name),
            //             new SlackMarkdownTextBlock("*Number Of Clicks*"),
            //             new SlackMarkdownTextBlock(numberOfClicks.ToString()),
            //             new SlackMarkdownTextBlock("*Clicks %*"),
            //             new SlackMarkdownTextBlock($"{Math.Round(totalNumberOfClicks == 0 ? 0 : 100 * numberOfClicks / (double)totalNumberOfClicks, 2)}%"),
            //             new SlackMarkdownTextBlock("*Clicks Per Article*"),
            //             new SlackMarkdownTextBlock($"{Math.Round(numberOfArticles == 0 ? 0 : numberOfClicks / (double)numberOfArticles, 2)}"),
            //         },
            //         accessory: new SlackImageBlock(
            //             imageUrl: $"https://espressonews.co/{newsPortal.IconUrl}" ?? "https://via.placeholder.com/350x150.jpg",
            //             altText: "news portal image"
            //         )
            //     );

            //     blocks.Add(newsPortalsBlock);
            //     blocks.Add(new SlackDividerBlock());
            // }

            // await Log(
            //     data: new SlackWebHookRequestBodyDto(
            //         userName: BackendStatisticsBotUsername,
            //         iconEmoji: BackendStatisticsBotIconEmoji,
            //         text: "News Portals",
            //         channel: BackendStatisticsChannel,
            //         blocks: blocks
            //     ),
            //     cancellationToken: cancellationToken
            // );
            // blocks.Clear();

            // blocks.Add(new SlackHeaderBlock(new SlackPlainTextBlock("Categories")));


            // foreach (var (category, numberOfClicks, articles) in categoriesWithNumberOfClicks)
            // {
            //     var numberOfArticles = articles.Count();
            //     var categoriesBlock = new SlackTextFieldsSectionBlock(
            //         text: new SlackMarkdownTextBlock($"*{category.Name}*"),
            //         fields: new List<SlackMarkdownTextBlock>
            //         {
            //             new SlackMarkdownTextBlock("*Number Of Clicks*"),
            //             new SlackMarkdownTextBlock(numberOfClicks.ToString()),
            //             new SlackMarkdownTextBlock("*Clicks %*"),
            //             new SlackMarkdownTextBlock($"{Math.Round(totalNumberOfClicks == 0 ? 0 : 100 * numberOfClicks / (double)totalNumberOfClicks, 2)}%"),
            //             new SlackMarkdownTextBlock("*Clicks Per Article*"),
            //             new SlackMarkdownTextBlock($"{Math.Round(numberOfArticles == 0 ? 0 : numberOfClicks / (double)numberOfArticles, 2)}"),
            //         }
            //     );
            //     blocks.Add(categoriesBlock);
            //     blocks.Add(new SlackDividerBlock());
            // }

            // await Log(
            //     data: new SlackWebHookRequestBodyDto(
            //         userName: BackendStatisticsBotUsername,
            //         iconEmoji: BackendStatisticsBotIconEmoji,
            //         text: "Categories",
            //         channel: BackendStatisticsChannel,
            //         blocks: blocks
            //     ),
            //     cancellationToken: cancellationToken
            // );
            #endregion
        }

        public Task LogPushNotification(
            PushNotification pushNotification,
            Article article,
            CancellationToken cancellationToken
        )
        {
            var blocks = new List<SlackBlock>
            {
                new SlackHeaderBlock(new SlackPlainTextBlock(pushNotification.InternalName)),
                new SlackTextFieldsImageSectionBlock(
                    text: new SlackMarkdownTextBlock($"<{pushNotification.ArticleUrl}|{article.Title}>"),
                    fields: new List<SlackMarkdownTextBlock>
                    {
                        new SlackMarkdownTextBlock("*Topic*"),
                        new SlackMarkdownTextBlock(pushNotification.Topic),
                        new SlackMarkdownTextBlock("*Message*"),
                        new SlackMarkdownTextBlock(pushNotification.Message),
                        new SlackMarkdownTextBlock("*Title*"),
                        new SlackMarkdownTextBlock(pushNotification.Title),
                        new SlackMarkdownTextBlock("*Source*"),
                        new SlackMarkdownTextBlock(article.NewsPortal?.Name ?? ""),
                    },
                    accessory: new SlackImageBlock(
                        imageUrl: article.ImageUrl ?? "https://via.placeholder.com/350x150.jpg",
                        altText: "article image"
                    )
                )
            };

            return Log(
                data: new SlackWebHookRequestBodyDto(
                    userName: PushNotificationBotUsername,
                    iconEmoji: PushNotificationBotIconEmoji,
                    text: "PushNotification",
                    channel: PushNotificationChannel,
                    blocks: blocks
                ),
                cancellationToken: cancellationToken
            );
        }
        #endregion

        #region Private Methods
        private async Task Log(
                SlackWebHookRequestBodyDto data,
                CancellationToken cancellationToken
        )
        {
            if (!_applicationInformation.AppEnvironment.Equals(AppEnvironment.Prod))
            {
                return;
            }

            await _memoryCache.GetOrCreateAsync(data.Text, async entry =>
              {
                  var httpClient = _httpClientFactory.CreateClient(HttpClientConstants.SlackHttpClientName);

                  entry.AbsoluteExpirationRelativeToNow = s_exceptionMessageCooldownInterval;
                  try
                  {
                      var jsonString = await _jsonService.Serialize(data, cancellationToken);

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
