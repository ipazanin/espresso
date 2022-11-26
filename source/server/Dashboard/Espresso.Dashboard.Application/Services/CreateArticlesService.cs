// CreateArticlesService.cs
//
// © 2022 Espresso News. All rights reserved.

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
    private readonly IScrapeWebService _webScrapingService;
    private readonly IParseHtmlService _htmlParsingService;
    private readonly IValidator<ArticleData> _articleDataValidator;
    private readonly ILoggerService<CreateArticlesService> _loggerService;
    private readonly ISettingProvider _settingProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateArticlesService"/> class.
    /// </summary>
    /// <param name="webScrapingService"></param>
    /// <param name="htmlParsingService"></param>
    /// <param name="articleDataValidator"></param>
    /// <param name="loggerService"></param>
    /// <param name="settingProvider"></param>
    public CreateArticlesService(
        IScrapeWebService webScrapingService,
        IParseHtmlService htmlParsingService,
        IValidator<ArticleData> articleDataValidator,
        ILoggerService<CreateArticlesService> loggerService,
        ISettingProvider settingProvider)
    {
        _webScrapingService = webScrapingService;
        _htmlParsingService = htmlParsingService;
        _articleDataValidator = articleDataValidator;
        _loggerService = loggerService;
        _settingProvider = settingProvider;
    }

    public async Task<Channel<Article>> CreateArticlesFromRssFeedItems(
        Channel<RssFeedItem> rssFeedItemChannel,
        IEnumerable<Category> categories,
        CancellationToken cancellationToken)
    {
        var reader = rssFeedItemChannel.Reader;
        var parsedArticlesChannel = Channel.CreateUnbounded<Article>();
        var writer = parsedArticlesChannel.Writer;

        var tasks = new List<Task>();

        var rssFeedItems = reader.ReadAllAsync(cancellationToken);

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
                            _ = writer.TryWrite(article);
                        }
                    }
                    catch (Exception exception)
                    {
                        const string? EventName = "Create Article Unhandled Exception";
                        _loggerService.Log(EventName, exception, LogLevel.Error);
                    }
                }, cancellationToken);
            tasks.Add(task);
        }

        await Task.WhenAll(tasks);
        writer.Complete();

        return parsedArticlesChannel;
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
        if (
            itemId is null ||
            itemLinks?.FirstOrDefault() is null ||
            string.IsNullOrEmpty(rssFeed.AmpConfiguration?.TemplateUrl))
        {
            return null;
        }

        var articleId = $"{itemId[(itemId.IndexOf("id=") + 3)..]}";
        var urlSegments = itemLinks.FirstOrDefault()?.Segments ?? Array.Empty<string>();

        var firstArticleSegment = urlSegments.Length < 2 ? string.Empty : urlSegments[1].ToLower();
        var secondArticleSegment = urlSegments.Length < 3 ? string.Empty : urlSegments[2].ToLower();
        var thirdArticleSegment = urlSegments.Length < 4 ? string.Empty : urlSegments[3].ToLower();
        var fourthArticleSegment = urlSegments.Length < 5 ? string.Empty : urlSegments[4].ToLower();

        var articleUrl = string.Format(
            rssFeed.AmpConfiguration.TemplateUrl,
            articleId,
            firstArticleSegment,
            secondArticleSegment,
            thirdArticleSegment,
            fourthArticleSegment);

        return articleUrl;
    }

    private static IEnumerable<ArticleCategory> GetArticleCategories(
        IEnumerable<Category> categories,
        string? itemTitle,
        string? itemSummary,
        Guid articleId,
        Uri? itemUrl,
        RssFeed rssFeed)
    {
        var articleCategories = new HashSet<ArticleCategory>();
        var articleCategoriesFromKeyWords = GetArticleCategoriesFromCategorysKeyWords(
            categories: categories,
            itemTitle: itemTitle,
            itemSummary: itemSummary,
            articleId: articleId);
        articleCategories.UnionWith(articleCategoriesFromKeyWords);

        switch (rssFeed.CategoryParseConfiguration.CategoryParseStrategy)
        {
            default:
                var articleCategoriesFromRssFeed = GetArticleCategoriesFromRssFeed(
                    articleId: articleId,
                    rssFeed: rssFeed);
                articleCategories.UnionWith(articleCategoriesFromRssFeed);
                break;
            case CategoryParseStrategy.FromUrl:
                var articleCategoriesFromUrl = GetArticlecategoriesFromFromUrl(
                    articleId: articleId,
                    itemUrl: itemUrl,
                    rssFeed: rssFeed);
                articleCategories.UnionWith(articleCategoriesFromUrl);
                break;
        }

        return articleCategories;
    }

    private static IEnumerable<ArticleCategory> GetArticleCategoriesFromCategorysKeyWords(
        IEnumerable<Category> categories,
        string? itemTitle,
        string? itemSummary,
        Guid articleId)
    {
        var categoryIds = new HashSet<ArticleCategory>();
        foreach (var category in categories)
        {
            if (
                !string.IsNullOrEmpty(category.KeyWordsRegexPattern) &&
                Regex.IsMatch($"{itemTitle} {itemSummary}", category.KeyWordsRegexPattern, RegexOptions.IgnoreCase))
            {
                categoryIds.Add(new ArticleCategory(
                    id: Guid.NewGuid(),
                    articleId: articleId,
                    categoryId: category.Id,
                    article: null,
                    category: category));
            }
        }

        return categoryIds;
    }

    private static IEnumerable<ArticleCategory> GetArticlecategoriesFromFromUrl(
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
                var secondUrlSegment = itemUrl?
                    .Segments[rssFeedCategory.UrlSegmentIndex]
                    .Replace("/", string.Empty).ToLower();

                if (
                    !string.IsNullOrEmpty(rssFeedCategory.UrlRegex) &&
                    !string.IsNullOrEmpty(secondUrlSegment) &&
                    Regex.IsMatch(secondUrlSegment, rssFeedCategory.UrlRegex, RegexOptions.IgnoreCase))
                {
                    articleCategories.Add(new ArticleCategory(
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

        if (urlFragmentOrFullUrl.StartsWith("http"))
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
        IEnumerable<Category> categories,
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

        var articlecategories = GetArticleCategories(
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
            ArticleCategories = articlecategories,
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
            var eventName = Event.CreateArticle.GetDisplayName();
            var parameters = new (string, object)[]
            {
                    (nameof(rssFeedUrl), rssFeedUrl),
            };

            _loggerService.Log(eventName, exceptionMessage, LogLevel.Error, parameters);

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
        IEnumerable<string?>? elementExtensions,
        CancellationToken cancellationToken)
    {
        var elementExtensionIndex = rssFeed.ImageUrlParseConfiguration.ElementExtensionIndex;
        var isSavedInHtmlElementWithSrcAttribute = rssFeed.ImageUrlParseConfiguration.IsSavedInHtmlElementWithSrcAttribute;

        string? imageUrl;
        switch (rssFeed.ImageUrlParseConfiguration.ImageUrlParseStrategy)
        {
            case ImageUrlParseStrategy.FromContent:
                imageUrl = _htmlParsingService.GetSrcAttributeFromFirstImgElement(itemContent);
                break;
            case ImageUrlParseStrategy.FromElementExtension:
                if (elementExtensionIndex is null || elementExtensions is null)
                {
                    imageUrl = null;
                    break;
                }

                if (elementExtensions.Count() <= elementExtensionIndex.Value)
                {
                    imageUrl = null;
                    break;
                }

                if (isSavedInHtmlElementWithSrcAttribute == true)
                {
                    imageUrl = _htmlParsingService.GetSrcAttributeFromFirstImgElement(elementExtensions.ElementAt(elementExtensionIndex.Value));
                }
                else
                {
                    imageUrl = elementExtensions.ElementAt(elementExtensionIndex.Value);
                }

                break;
            default:
                if (itemLinks?.Count() > 1)
                {
                    imageUrl = itemLinks.ElementAt(1)?.ToString();
                }
                else
                {
                    imageUrl = _htmlParsingService.GetSrcAttributeFromFirstImgElement(itemSummary);
                }

                break;
        }

        var shouldImageBeWebScraped = rssFeed.ImageUrlParseConfiguration.ShouldImageUrlBeWebScraped
            ?? string.IsNullOrEmpty(imageUrl);

        if (shouldImageBeWebScraped)
        {
            imageUrl = await _webScrapingService.GetSrcAttributeFromElementDefinedByXPath(
                articleUrl: webUrl,
                requestType: RequestType.Browser,
                imageUrlParseConfiguration: rssFeed.ImageUrlParseConfiguration,
                cancellationToken: cancellationToken);

            var eventName = Event.ImageUrlWebScrapingData.GetDisplayName();
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

        return AddBaseUrlToUrlFragment(imageUrl, rssFeed.NewsPortal?.BaseUrl);
    }
}
