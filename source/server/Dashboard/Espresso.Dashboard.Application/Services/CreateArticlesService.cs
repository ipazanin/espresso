﻿// CreateArticlesService.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Diagnostics;
using System.Globalization;
using Espresso.Application.DataTransferObjects;
using Espresso.Common.Enums;
using Espresso.Common.Extensions;
using Espresso.Dashboard.Application.IServices;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.Infrastructure;
using Espresso.Domain.IServices;
using Espresso.Domain.Records;
using Espresso.Domain.ValueObjects.ArticleValueObjects;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Espresso.Dashboard.Application.Services;

public class CreateArticlesService : ICreateArticlesService
{
    private const int ArticlesChannelCapacity = 200;

    private static readonly BoundedChannelOptions s_boundedChannelOptions = new(ArticlesChannelCapacity)
    {
        FullMode = BoundedChannelFullMode.Wait,
    };

    private readonly IScrapeWebService _webScrapingService;
    private readonly IParseHtmlService _htmlParsingService;
    private readonly IValidator<ArticleData> _articleDataValidator;
    private readonly ILoggerService<CreateArticlesService> _loggerService;
    private readonly ISettingProvider _settingProvider;
    private readonly IParsingMessagesService _parsingMessagesService;
    private readonly Channel<Article> _articlesChannel = Channel.CreateBounded<Article>(s_boundedChannelOptions);

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateArticlesService"/> class.
    /// </summary>
    /// <param name="webScrapingService"></param>
    /// <param name="htmlParsingService"></param>
    /// <param name="articleDataValidator"></param>
    /// <param name="loggerService"></param>
    /// <param name="settingProvider"></param>
    /// <param name="parsingMessagesService"></param>
    public CreateArticlesService(
        IScrapeWebService webScrapingService,
        IParseHtmlService htmlParsingService,
        IValidator<ArticleData> articleDataValidator,
        ILoggerService<CreateArticlesService> loggerService,
        ISettingProvider settingProvider,
        IParsingMessagesService parsingMessagesService)
    {
        _webScrapingService = webScrapingService;
        _htmlParsingService = htmlParsingService;
        _articleDataValidator = articleDataValidator;
        _loggerService = loggerService;
        _settingProvider = settingProvider;
        _parsingMessagesService = parsingMessagesService;
    }

    public ChannelReader<Article> ArticlesChannelReader => _articlesChannel.Reader;

    public async Task CreateArticlesFromRssFeedItems(
        ChannelReader<RssFeedItem> rssFeedItemChannelReader,
        IReadOnlyList<Category> categories,
        CancellationToken cancellationToken)
    {
        var tasks = new List<Task>();
        var articleChannelWriter = _articlesChannel.Writer;

        var rssFeedItems = rssFeedItemChannelReader.ReadAllAsync(cancellationToken);

        await foreach (var rssFeedItem in rssFeedItems)
        {
            var task = Task.Run(
                async () =>
                {
                    try
                    {
                        var (article, isValid) = await CreateArticleAsync(
                            rssFeedItem: rssFeedItem,
                            categories: categories,
                            cancellationToken: cancellationToken);

                        if (isValid && article != null)
                        {
                            await articleChannelWriter.WriteAsync(article);
                        }
                    }
                    catch (Exception exception)
                    {
                        const string? EventName = "Create Article Unhandled Exception";
                        _loggerService.Log(EventName, exception, LogLevel.Error);

                        var parsingErrorMessage = new ParsingErrorMessageDto(
                            logLevel: LogLevel.Error,
                            message: $"Create Article Unhandled Exception: {exception.Message}",
                            rssFeedId: rssFeedItem.RssFeed.Id);
                        _parsingMessagesService.PushMessage(parsingErrorMessage);
                    }
                }, cancellationToken);
            tasks.Add(task);
        }

        await Task.WhenAll(tasks);
        articleChannelWriter.Complete();
    }

    private static string? GetUrl(RssFeed rssFeed, IEnumerable<Uri?>? itemLinks, string? itemId)
    {
        return rssFeed.AmpConfiguration?.HasAmpArticles == true ?
            GetAmpUrl(rssFeed, itemLinks, itemId) :
            GetNormalUrl(rssFeed, itemLinks);
    }

    private static string? GetNormalUrl(RssFeed rssFeed, IEnumerable<Uri?>? itemLinks)
    {
        var url = itemLinks?.FirstOrDefault()?.ToString();

        var articleUrl = AddBaseUrlToUrlFragment(url, rssFeed.NewsPortal?.BaseUrl);

        return articleUrl;
    }

    private static string? GetAmpUrl(RssFeed rssFeed, IEnumerable<Uri?>? itemLinks, string? itemId)
    {
        if (itemId is null ||
            itemLinks?.FirstOrDefault() is null ||
            string.IsNullOrEmpty(rssFeed.AmpConfiguration?.TemplateUrl))
        {
            return null;
        }

        var articleId = $"{itemId[(itemId.IndexOf("id=", StringComparison.Ordinal) + 3)..]}";
        var urlSegments = itemLinks.FirstOrDefault()?.Segments ?? [];

#pragma warning disable CA1308 // Normalize strings to uppercase
        var firstArticleSegment = urlSegments.Length < 2 ? string.Empty : urlSegments[1].ToLowerInvariant();
        var secondArticleSegment = urlSegments.Length < 3 ? string.Empty : urlSegments[2].ToLowerInvariant();
        var thirdArticleSegment = urlSegments.Length < 4 ? string.Empty : urlSegments[3].ToLowerInvariant();
        var fourthArticleSegment = urlSegments.Length < 5 ? string.Empty : urlSegments[4].ToLowerInvariant();
#pragma warning restore CA1308 // Normalize strings to uppercase

        var articleUrl = string.Format(
            CultureInfo.InvariantCulture,
            rssFeed.AmpConfiguration.TemplateUrl,
            articleId,
            firstArticleSegment,
            secondArticleSegment,
            thirdArticleSegment,
            fourthArticleSegment);

        return articleUrl;
    }

    private static HashSet<ArticleCategory> GetArticleCategories(
        IReadOnlyList<Category> categories,
        string? itemTitle,
        string? itemSummary,
        Guid articleId,
        Uri? itemUrl,
        RssFeed rssFeed)
    {
        var articleCategories = GetArticleCategoriesFromCategoriesKeyWords(
            categories: categories,
            itemTitle: itemTitle,
            itemSummary: itemSummary,
            articleId: articleId);

        switch (rssFeed.CategoryParseConfiguration.CategoryParseStrategy)
        {
            case CategoryParseStrategy.None:
            case CategoryParseStrategy.FromRssFeed:
                var articleCategoriesFromRssFeed = GetArticleCategoriesFromRssFeed(
                    articleId: articleId,
                    rssFeed: rssFeed);
                articleCategories.UnionWith(articleCategoriesFromRssFeed);
                break;
            case CategoryParseStrategy.FromUrl:
                var articleCategoriesFromUrl = GetArticleCategoriesFromFromUrl(
                    articleId: articleId,
                    itemUrl: itemUrl,
                    rssFeed: rssFeed);
                articleCategories.UnionWith(articleCategoriesFromUrl);
                break;
            default:
                throw new UnreachableException($"Unknown CategoryParseStrategy: {rssFeed.CategoryParseConfiguration.CategoryParseStrategy}");
        }

        return articleCategories;
    }

    private static HashSet<ArticleCategory> GetArticleCategoriesFromCategoriesKeyWords(
        IReadOnlyList<Category> categories,
        string? itemTitle,
        string? itemSummary,
        Guid articleId)
    {
        var articleCategories = new HashSet<ArticleCategory>();
        foreach (var category in categories)
        {
            if (
                !string.IsNullOrEmpty(category.KeyWordsRegexPattern) &&
                Regex.IsMatch($"{itemTitle} {itemSummary}", category.KeyWordsRegexPattern, RegexOptions.IgnoreCase))
            {
                _ = articleCategories.Add(new ArticleCategory(
                    id: Guid.NewGuid(),
                    articleId: articleId,
                    categoryId: category.Id,
                    article: null,
                    category: null));
            }
        }

        return articleCategories;
    }

    private static HashSet<ArticleCategory> GetArticleCategoriesFromFromUrl(
        Guid articleId,
        Uri? itemUrl,
        RssFeed rssFeed)
    {
        var articleCategories = new HashSet<ArticleCategory>();

        foreach (var rssFeedCategory in rssFeed.RssFeedCategories)
        {
            if (
                itemUrl?.Segments != null &&
                itemUrl.Segments.Length > rssFeedCategory.UrlSegmentIndex)
            {
                var secondUrlSegment = itemUrl
                    .Segments[rssFeedCategory.UrlSegmentIndex]
                    .Replace("/", string.Empty, StringComparison.Ordinal);

                if (
                    !string.IsNullOrEmpty(rssFeedCategory.UrlRegex) &&
                    !string.IsNullOrEmpty(secondUrlSegment) &&
                    Regex.IsMatch(secondUrlSegment, rssFeedCategory.UrlRegex, RegexOptions.IgnoreCase))
                {
                    _ = articleCategories.Add(new ArticleCategory(
                        id: Guid.NewGuid(),
                        articleId: articleId,
                        categoryId: rssFeedCategory.CategoryId,
                        article: null,
                        category: null));
                }
            }
        }

        return articleCategories;
    }

    private static IEnumerable<ArticleCategory> GetArticleCategoriesFromRssFeed(
        Guid articleId,
        RssFeed rssFeed)
    {
        yield return new ArticleCategory(
            id: Guid.NewGuid(),
            articleId: articleId,
            categoryId: rssFeed.CategoryId,
            article: null,
            category: null);
    }

    private static string? AddBaseUrlToUrlFragment(string? urlFragmentOrFullUrl, string? baseUrl)
    {
        if (
            string.IsNullOrEmpty(urlFragmentOrFullUrl) ||
            string.IsNullOrEmpty(baseUrl))
        {
            return null;
        }

        if (urlFragmentOrFullUrl.StartsWith("http", StringComparison.Ordinal))
        {
            return urlFragmentOrFullUrl;
        }

        return $"{baseUrl}{urlFragmentOrFullUrl}";
    }

    private DateTimeOffset? GetPublishDateTime(DateTimeOffset itemPublishDateTime, DateTimeOffset utcNow)
    {
        var invalidPublishdateTimeMinimum = utcNow - _settingProvider.LatestSetting.ArticleSetting.MaxAgeOfArticle;
        var minimumPublishDateTime = utcNow.AddDays(-1);
        var maximumPublishDateTime = utcNow;
        var rssFeedPublishDateTime = itemPublishDateTime.UtcDateTime;

        if (rssFeedPublishDateTime < invalidPublishdateTimeMinimum)
        {
            return null;
        }

        if (rssFeedPublishDateTime > maximumPublishDateTime || rssFeedPublishDateTime < minimumPublishDateTime)
        {
            return utcNow;
        }

        return rssFeedPublishDateTime;
    }

    private async Task<(Article? article, bool isValid)> CreateArticleAsync(
        RssFeedItem rssFeedItem,
        IReadOnlyList<Category> categories,
        CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        var utcNow = DateTimeOffset.UtcNow;
        const int InitialNumberOfClicks = 0;

        var title = rssFeedItem.Title;

        var url = GetUrl(
            rssFeed: rssFeedItem.RssFeed,
            itemLinks: rssFeedItem.Links,
            itemId: rssFeedItem.Id);

        var webUrl = GetNormalUrl(
            rssFeed: rssFeedItem.RssFeed,
            itemLinks: rssFeedItem.Links);

        var summary = GetSummary(
            itemSummary: rssFeedItem.Summary,
            itemTitle: rssFeedItem.Title);

        var imageUrl = await GetImageUrl(
            itemLinks: rssFeedItem.Links,
            itemSummary: rssFeedItem.Summary,
            itemContent: rssFeedItem.Content,
            rssFeed: rssFeedItem.RssFeed,
            webUrl: webUrl,
            elementExtensions: rssFeedItem.ElementExtensions,
            cancellationToken: cancellationToken);

        var publishDateTime = GetPublishDateTime(
            itemPublishDateTime: rssFeedItem.PublishDateTime,
            utcNow: utcNow);

        var articleCategories = GetArticleCategories(
            categories: categories,
            itemTitle: rssFeedItem.Title,
            itemSummary: summary,
            articleId: id,
            itemUrl: rssFeedItem.Links?.FirstOrDefault(),
            rssFeed: rssFeedItem.RssFeed);

        var articleData = new ArticleData
        {
            Id = id,
            PublishDateTime = publishDateTime,
            Summary = summary,
            Title = title,
            CreateDateTime = utcNow,
            ImageUrl = imageUrl,
            NumberOfClicks = InitialNumberOfClicks,
            TrendingScore = Article.TrendingScoreDefaultValue,
            UpdateDateTime = utcNow,
            Url = url,
            WebUrl = webUrl,
            ArticleCategories = articleCategories,
        };

        return CreateArticle(
            articleData: articleData,
            rssFeed: rssFeedItem.RssFeed);
    }

    private (Article? article, bool isValid) CreateArticle(ArticleData articleData, RssFeed rssFeed)
    {
        var validationResult = _articleDataValidator.Validate(articleData);

        if (validationResult.IsValid)
        {
            var article = new Article(
                id: articleData.Id,
                url: articleData.Url!,
                webUrl: articleData.WebUrl!,
                summary: articleData.Summary!,
                title: articleData.Title!,
                imageUrl: articleData.ImageUrl,
                createDateTime: articleData.CreateDateTime,
                updateDateTime: articleData.UpdateDateTime,
                publishDateTime: articleData.PublishDateTime!.Value,
                numberOfClicks: articleData.NumberOfClicks,
                trendingScore: articleData.TrendingScore,
                editorConfiguration: new EditorConfiguration(),
                newsPortalId: rssFeed.NewsPortalId,
                rssFeedId: rssFeed.Id,
                articleCategories: articleData.ArticleCategories,
                newsPortal: null,
                rssFeed: null,
                firstSimilarArticles: null,
                secondSimilarArticles: null);

            return (article, true);
        }
        else
        {
            var rssFeedUrl = rssFeed.Url;
            var exceptionMessage = validationResult.ToString();
            var eventName = LoggingEvent.CreateArticle.GetDisplayName();
            var parameters = new (string, object)[]
            {
                    (nameof(rssFeedUrl), rssFeedUrl),
            };

            _loggerService.Log(eventName, exceptionMessage, LogLevel.Error, parameters);

            var parsingErrorMessage = new ParsingErrorMessageDto(
                logLevel: LogLevel.Information,
                message: $"Article: {articleData.WebUrl} is invalid: {exceptionMessage}",
                rssFeedId: rssFeed.Id);
            _parsingMessagesService.PushMessage(parsingErrorMessage);

            return (null, false);
        }
    }

    private string? GetSummary(string? itemSummary, string? itemTitle)
    {
        var summary = _htmlParsingService.GetText(html: itemSummary);
        return string.IsNullOrEmpty(summary) ? itemTitle : summary;
    }

    private async Task<string?> GetImageUrl(
        IEnumerable<Uri?>? itemLinks,
        string? itemSummary,
        string? itemContent,
        RssFeed rssFeed,
        string? webUrl,
        IEnumerable<XElement> elementExtensions,
        CancellationToken cancellationToken)
    {
        string? imageUrl;
        switch (rssFeed.ImageUrlParseConfiguration.ImageUrlParseStrategy)
        {
            case ImageUrlParseStrategy.FromContent:
                imageUrl = _htmlParsingService.GetSrcAttributeFromFirstImgElement(itemContent);
                break;
            case ImageUrlParseStrategy.FromElementExtension:
                var elementExtension = elementExtensions
                    .FirstOrDefault(e => e.Name.LocalName == rssFeed.ImageUrlParseConfiguration.ElementExtensionName);

                var value = rssFeed.ImageUrlParseConfiguration.ElementExtensionValueType switch
                {
                    XmlValueType.Attribute => elementExtension?.Attribute(rssFeed.ImageUrlParseConfiguration.ElementExtensionAttributeName)?.Value,
                    XmlValueType.Value or _ => elementExtension?.Value,
                };

                imageUrl = rssFeed.ImageUrlParseConfiguration.ElementExtensionValueParseType switch
                {
                    ValueParseType.Html => _htmlParsingService.GetSrcAttributeFromFirstImgElement(value),
                    ValueParseType.FullValue or _ => value,
                };
                break;
            case ImageUrlParseStrategy.None:
            case ImageUrlParseStrategy.SecondLinkOrFromSummary:
                if (itemLinks?.Count() > 1)
                {
                    imageUrl = itemLinks.ElementAt(1)?.ToString();
                }
                else
                {
                    imageUrl = _htmlParsingService.GetSrcAttributeFromFirstImgElement(itemSummary);
                }

                break;
            default:
                throw new UnreachableException($"Unknown image url parse strategy: {rssFeed.ImageUrlParseConfiguration.ImageUrlParseStrategy}");
        }

        var shouldImageBeWebScraped = rssFeed.ImageUrlParseConfiguration.ShouldImageUrlBeWebScraped;

        if (shouldImageBeWebScraped)
        {
            imageUrl = await _webScrapingService.GetSrcAttributeFromElementDefinedByXPath(
                articleUrl: webUrl,
                rssFeed: rssFeed,
                cancellationToken: cancellationToken);

            var eventName = LoggingEvent.ImageUrlWebScrapingData.GetDisplayName();
            var rssFeedUrl = rssFeed.Url;
            var newsPortalName = rssFeed.NewsPortal?.Name ?? string.Empty;
            var arguments = new (string, object)[]
            {
                    (nameof(rssFeedUrl), rssFeedUrl),
                    (nameof(newsPortalName), newsPortalName),
                    (nameof(imageUrl), imageUrl ?? string.Empty),
            };

            _loggerService.Log(eventName, LogLevel.Information, arguments);
        }

        if (string.IsNullOrEmpty(imageUrl))
        {
            var parsingErrorMessage = new ParsingErrorMessageDto(
                logLevel: LogLevel.Warning,
                message: $"Article {webUrl} does not have image!",
                rssFeedId: rssFeed.Id);
            _parsingMessagesService.PushMessage(parsingErrorMessage);
        }

        return AddBaseUrlToUrlFragment(imageUrl, rssFeed.NewsPortal!.BaseUrl);
    }
}
