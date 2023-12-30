// SlackService.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Globalization;
using Espresso.Application.DataTransferObjects.SlackDataTransferObjects;
using Espresso.Application.Models;
using Espresso.Application.Services.Contracts;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Common.Services.Contracts;
using Espresso.Dashboard.Application.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.IServices;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Espresso.Application.Services.Implementations;

/// <inheritdoc/>
public class SlackService : ISlackService
{
    private const string ErrorsBotIconEmoji = ":no_entry:";
    private const string ErrorBotUsername = "error-bot";
    private const string ErrorsChannel = "#errors-backend-bot";
    private const string MissingCategoriesErrorsBotIconEmoji = ":warning:";
    private const string MissingCategoriesErrorsBotUsername = "warning-bot";
    private const string MissingCategoriesErrorsChannel = "#missing-categories-errors-bot";

    private const string MarketingBotIconEmoji = ":bar_chart:";
    private const string MarketingBotUsername = "marketing-bot";
    private const string MarketingBitChannel = "#marketing-bot";

    private const string NewNewsPortalRequestBotIconEmoji = ":email:";
    private const string NewNewsPortalRequestBotUsername = "new-source-bot";
    private const string NewNewsPortalRequestChannel = "#new-source-requests-bot";

    private const string BackendStatisticsBotIconEmoji = ":bar_chart:";
    private const string BackendStatisticsBotUsername = "backend-bot";
    private const string BackendStatisticsChannel = "#backend-statistics";

    private const string PushNotificationBotIconEmoji = ":bell:";
    private const string PushNotificationBotUsername = "push-bot";
    private const string PushNotificationChannel = "#general";
    private static readonly TimeSpan s_exceptionMessageCoolDownInterval = TimeSpan.FromHours(4);

    private readonly IMemoryCache _memoryCache;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILoggerService<SlackService> _loggerService;
    private readonly IJsonService _jsonService;
    private readonly string _analyticsWebHookUrl;
    private readonly string _crashReportWebHookUrl;
    private readonly string _newSourceRequestWebHookUrl;
    private readonly ApplicationInformation _applicationInformation;

    /// <summary>
    /// Initializes a new instance of the <see cref="SlackService"/> class.
    /// </summary>
    /// <param name="memoryCache">Memory cache.</param>
    /// <param name="httpClientFactory">HTTP client factory.</param>
    /// <param name="loggerService">Logger service.</param>
    /// <param name="jsonService">JSON service.</param>
    /// <param name="analyticsWebHookUrl">Slack web hook url.</param>
    /// <param name="crashReportWebHookUrl"></param>
    /// <param name="newSourceRequestWebHookUrl"></param>
    /// <param name="applicationInformation">Application information.</param>
    public SlackService(
        IMemoryCache memoryCache,
        IHttpClientFactory httpClientFactory,
        ILoggerService<SlackService> loggerService,
        IJsonService jsonService,
        string analyticsWebHookUrl,
        string crashReportWebHookUrl,
        string newSourceRequestWebHookUrl,
        ApplicationInformation applicationInformation)
    {
        _memoryCache = memoryCache;
        _httpClientFactory = httpClientFactory;
        _loggerService = loggerService;
        _jsonService = jsonService;
        _analyticsWebHookUrl = analyticsWebHookUrl;
        _crashReportWebHookUrl = crashReportWebHookUrl;
        _newSourceRequestWebHookUrl = newSourceRequestWebHookUrl;
        _applicationInformation = applicationInformation;
    }

    /// <inheritdoc/>
    public Task LogError(
        string eventName,
        string message,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var exceptionMessage = exception.Message;
        var innerExceptionMessage = exception.InnerException?.Message ?? FormatConstants.EmptyValue;
        var text = $":blue_book: Event Name: {eventName}\n" +
            $":label: Version: {_applicationInformation.Version}\n" +
            $":email: Message: {message}\n" +
            $":exclamation: Exception Message: {exceptionMessage}\n" +
            $":exclamation: Inner Exception Message: {innerExceptionMessage}\n";

        return SendToSlack(
            webHookUrl: _crashReportWebHookUrl,
            data: new SlackWebHookRequestBodyDto(
                userName: ErrorBotUsername,
                iconEmoji: ErrorsBotIconEmoji,
                text: text,
                channel: ErrorsChannel,
                blocks: Array.Empty<SlackBlock>()),
            cancellationToken: cancellationToken);
    }

    /// <inheritdoc/>
    public Task LogApplicationStatistics(
        ApplicationStatistics applicationStatistics,
        CancellationToken cancellationToken)
    {
        var blocks = new List<SlackBlock>
            {
                new SlackTextFieldsImageSectionBlock(
                    text: new SlackMarkdownTextBlock("Downloads:"),
                    fields: new List<SlackMarkdownTextBlock>()
                    {
                        new SlackMarkdownTextBlock("Android"),
                        new SlackMarkdownTextBlock(applicationStatistics.YesterdayAndroidCount.ToString(CultureInfo.InvariantCulture)),
                        new SlackMarkdownTextBlock("iOS"),
                        new SlackMarkdownTextBlock(applicationStatistics.YesterdayIosCount.ToString(CultureInfo.InvariantCulture)),
                        new SlackMarkdownTextBlock("Total Android"),
                        new SlackMarkdownTextBlock(applicationStatistics.TotalAndroidCount.ToString(CultureInfo.InvariantCulture)),
                        new SlackMarkdownTextBlock("Total iOS"),
                        new SlackMarkdownTextBlock(applicationStatistics.TotalIosCount.ToString(CultureInfo.InvariantCulture)),
                        new SlackMarkdownTextBlock("Total"),
                        new SlackMarkdownTextBlock((applicationStatistics.TotalIosCount + applicationStatistics.TotalAndroidCount).ToString(CultureInfo.InvariantCulture)),
                    },
                    accessory: new SlackImageBlock(
                        imageUrl: "https://aux.iconspalace.com/uploads/download-icon-256-361231194.png",
                        altText: "Downloads Icon")),
                new SlackDividerBlock(),
                new SlackTextFieldsImageSectionBlock(
                    text: new SlackMarkdownTextBlock("Active Users:"),
                    fields: new List<SlackMarkdownTextBlock>()
                    {
                        new SlackMarkdownTextBlock("Android"),
                        new SlackMarkdownTextBlock(applicationStatistics.ActiveUsersOnAndroid.ToString(CultureInfo.InvariantCulture)),
                        new SlackMarkdownTextBlock("iOS"),
                        new SlackMarkdownTextBlock(applicationStatistics.ActiveUsersOnIos.ToString(CultureInfo.InvariantCulture)),
                        new SlackMarkdownTextBlock("Total"),
                        new SlackMarkdownTextBlock($"{applicationStatistics.ActiveUsersOnAndroid + applicationStatistics.ActiveUsersOnIos}"),
                    },
                    accessory: new SlackImageBlock(
                        imageUrl: "https://cdn1.iconfinder.com/data/icons/ui-colored-3-of-3/100/UI_3__23-512.png",
                        altText: "Active Users Icon")),
                new SlackDividerBlock(),
                new SlackTextFieldsImageSectionBlock(
                    text: new SlackMarkdownTextBlock("Revenue:"),
                    fields: new List<SlackMarkdownTextBlock>()
                    {
                        new SlackMarkdownTextBlock("Today Android"),
                        new SlackMarkdownTextBlock($"{applicationStatistics.AndroidRevenueToday:0.##}$"),
                        new SlackMarkdownTextBlock("Today iOS"),
                        new SlackMarkdownTextBlock($"{applicationStatistics.IosRevenueToday:0.##}$"),
                        new SlackMarkdownTextBlock("Today"),
                        new SlackMarkdownTextBlock($"{applicationStatistics.AndroidRevenueToday + applicationStatistics.IosRevenueToday:0.##}$"),
                        new SlackMarkdownTextBlock("Current Month"),
                        new SlackMarkdownTextBlock($"{applicationStatistics.RevenueCurrentMonth:0.##}$"),
                        new SlackMarkdownTextBlock("Previous Month"),
                        new SlackMarkdownTextBlock($"{applicationStatistics.RevenuePreviousMonth:0.##}$"),
                    },
                    accessory: new SlackImageBlock(
                        imageUrl: "https://icons.iconarchive.com/icons/cjdowner/cryptocurrency/256/Dollar-USD-icon.png",
                        altText: "Revenue Icon")),
            };

        return SendToSlack(
            webHookUrl: _analyticsWebHookUrl,
            data: new SlackWebHookRequestBodyDto(
                userName: MarketingBotUsername,
                iconEmoji: MarketingBotIconEmoji,
                text: "Analytics Data",
                channel: MarketingBitChannel,
                blocks: blocks),
            cancellationToken: cancellationToken);
    }

    /// <inheritdoc/>
    public Task LogMissingCategoriesError(
        string rssFeedUrl,
        string articleUrl,
        string urlCategories,
        CancellationToken cancellationToken)
    {
        var text = ":blue_book: Request Name: Missing Categories\n" +
            $":email: Rss Feed Url: {rssFeedUrl}\n" +
            $":email: Article Url: {articleUrl}\n" +
            $":email: Url-SegmentIndex:Category Map: {urlCategories}\n";

        return SendToSlack(
            webHookUrl: _crashReportWebHookUrl,
            data: new SlackWebHookRequestBodyDto(
                userName: MissingCategoriesErrorsBotUsername,
                iconEmoji: MissingCategoriesErrorsBotIconEmoji,
                text: text,
                channel: MissingCategoriesErrorsChannel,
                blocks: Array.Empty<SlackBlock>()),
            cancellationToken: cancellationToken);
    }

    /// <inheritdoc/>
    public Task LogNewNewsPortalRequest(
        string newsPortalName,
        string email,
        string? url,
        CancellationToken cancellationToken)
    {
        var text = "There's a request for new source\n" +
            $"Source Name: {newsPortalName}\n" +
            $"Email: {email}\n" +
            $"Url: {url}";

        return SendToSlack(
            webHookUrl: _newSourceRequestWebHookUrl,
            data: new SlackWebHookRequestBodyDto(
                userName: NewNewsPortalRequestBotUsername,
                iconEmoji: NewNewsPortalRequestBotIconEmoji,
                text: text,
                channel: NewNewsPortalRequestChannel,
                blocks: Array.Empty<SlackBlock>()),
            cancellationToken: cancellationToken);
    }

    /// <inheritdoc/>
    public Task LogYesterdaysStatistics(
        IEnumerable<Article> topArticles,
        int totalNumberOfClicks,
        IEnumerable<(NewsPortal newsPortal, int numberOfClicks, IEnumerable<Article> articles)> topNewsPortals,
        IEnumerable<(Category category, int numberOfClicks, IEnumerable<Article> articles)> categoriesWithNumberOfClicks,
        CancellationToken cancellationToken)
    {
        var blocks = new List<SlackBlock>
            {
                new SlackHeaderBlock(new SlackPlainTextBlock("Total clicks yesterday:")),
                new SlackTextSectionBlock(new SlackMarkdownTextBlock(totalNumberOfClicks.ToString(CultureInfo.InvariantCulture))),
                new SlackHeaderBlock(new SlackPlainTextBlock("Top Articles")),
            };

        foreach (var article in topArticles)
        {
            var articleBlock = new SlackTextFieldsImageSectionBlock(
                text: new SlackMarkdownTextBlock($"<{article.Url}|{article.Title}>"),
                fields: new List<SlackMarkdownTextBlock>()
                {
                        new SlackMarkdownTextBlock("*Source*"),
                        new SlackMarkdownTextBlock($"<{article.NewsPortal!.BaseUrl}|{article.NewsPortal!.Name}>"),
                        new SlackMarkdownTextBlock("*Category*"),
                        new SlackMarkdownTextBlock(article.ArticleCategories.First().Category!.Name),
                        new SlackMarkdownTextBlock("*Publish Time*"),
                        new SlackMarkdownTextBlock(article.PublishDateTime.DateTime.ToShortTimeString()),
                        new SlackMarkdownTextBlock("*Number Of Clicks*"),
                        new SlackMarkdownTextBlock(article.NumberOfClicks.ToString(CultureInfo.InvariantCulture)),
                        new SlackMarkdownTextBlock("*Clicks ‰*"),
                        new SlackMarkdownTextBlock($"{Math.Round(totalNumberOfClicks == 0 ? 0 : 100 * article.NumberOfClicks / (double)totalNumberOfClicks, 2)}‰"),
                },
                accessory: new SlackImageBlock(
                    imageUrl: article.ImageUrl ?? article.NewsPortal?.IconUrl ?? "https://via.placeholder.com/350x150.jpg",
                    altText: "article image"));

            blocks.Add(articleBlock);
            blocks.Add(new SlackDividerBlock());
        }

        return SendToSlack(
            webHookUrl: _analyticsWebHookUrl,
            data: new SlackWebHookRequestBodyDto(
                userName: BackendStatisticsBotUsername,
                iconEmoji: BackendStatisticsBotIconEmoji,
                text: "Articles",
                channel: BackendStatisticsChannel,
                blocks: blocks),
            cancellationToken: cancellationToken);
    }

    /// <inheritdoc/>
    public Task LogPushNotification(
        PushNotification pushNotification,
        Article article,
        CancellationToken cancellationToken)
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
                        new SlackMarkdownTextBlock(article.NewsPortal?.Name ?? string.Empty),
                    },
                    accessory: new SlackImageBlock(
                        imageUrl: article.ImageUrl ?? "https://via.placeholder.com/350x150.jpg",
                        altText: "article image")),
            };

        return SendToSlack(
            webHookUrl: _analyticsWebHookUrl,
            data: new SlackWebHookRequestBodyDto(
                userName: PushNotificationBotUsername,
                iconEmoji: PushNotificationBotIconEmoji,
                text: "PushNotification",
                channel: PushNotificationChannel,
                blocks: blocks),
            cancellationToken: cancellationToken);
    }

    private async Task SendToSlack(
        string webHookUrl,
        SlackWebHookRequestBodyDto data,
        CancellationToken cancellationToken)
    {
        if (!_applicationInformation.AppEnvironment.Equals(AppEnvironment.Prod))
        {
            return;
        }

        _ = await _memoryCache.GetOrCreateAsync(data.Text, async entry =>
        {
            var httpClient = _httpClientFactory.CreateClient(HttpClientConstants.SlackHttpClientName);

            entry.AbsoluteExpirationRelativeToNow = s_exceptionMessageCoolDownInterval;
            try
            {
                var jsonString = await _jsonService.Serialize(data, cancellationToken);

                var content = new StringContent(jsonString, Encoding.UTF8, MimeTypeConstants.Json);
                using var response = await httpClient.PostAsync(
                    requestUri: webHookUrl,
                    content: content,
                    cancellationToken: cancellationToken);

                _ = response.EnsureSuccessStatusCode();
            }
            catch (Exception exception)
            {
                const string EventName = nameof(LoggingEvent.SlackServiceException);
                var exceptionMessage = exception.Message;
                var innerExceptionMessage = exception.InnerException?.Message ?? string.Empty;

                var arguments = new List<(string parameterName, object parameterValue)>
                  {
                          (nameof(exceptionMessage), exceptionMessage),
                          (nameof(innerExceptionMessage), innerExceptionMessage),
                  };

                _loggerService.Log(EventName, exception, LogLevel.Error, arguments);
            }

            return string.Empty;
        });
    }
}
