// CreateArticlesService.cs
//
// © 2021 Espresso News. All rights reserved.

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Espresso.Dashboard.Application.Services
{
    public class CreateArticleService : ICreateArticleService
    {
        private readonly IScrapeWebService _webScrapingService;
        private readonly IParseHtmlService _htmlParsingService;
        private readonly IValidator<ArticleData> _articleDataValidator;
        private readonly ILoggerService<CreateArticleService> _loggerService;
        private readonly ISettingProvider _settingProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateArticleService"/> class.
        /// </summary>
        /// <param name="webScrapingService"></param>
        /// <param name="htmlParsingService"></param>
        /// <param name="articleDataValidator"></param>
        /// <param name="loggerService"></param>
        /// <param name="settingProvider"></param>
        public CreateArticleService(
            IScrapeWebService webScrapingService,
            IParseHtmlService htmlParsingService,
            IValidator<ArticleData> articleDataValidator,
            ILoggerService<CreateArticleService> loggerService,
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
                            const string? eventName = "Create Article Unhandled Exception";
                            _loggerService.Log(eventName, exception, LogLevel.Error);
                        }
                    }, cancellationToken);
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
            writer.Complete();

            return parsedArticlesChannel;
        }

        private async Task<(Article? article, bool isValid)> CreateArticleAsync(
            RssFeedItem rssFeedItem,
            IEnumerable<Category> categories,
            CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid();
            var utcNow = DateTime.UtcNow;
            const int initialNumberOfClicks = 0;

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
                NumberOfClicks = initialNumberOfClicks,
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
                    subordinateArticles: null,
                    mainArticle: null);

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

        private static string? GetUrl(RssFeed rssFeed, IEnumerable<Uri?>? itemLinks, string? itemId)
        {
            if (rssFeed.AmpConfiguration?.HasAmpArticles == true)
            {
                return GetAmpUrl(rssFeed, itemLinks, itemId);
            }

            return GetNormalUrl(rssFeed, itemLinks);
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

            var articleId = $"{itemId[(itemId.IndexOf("id=") + 3) ..]}";
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
            var imageUrl = rssFeed.ImageUrlParseConfiguration.ImageUrlParseStrategy switch
            {
                ImageUrlParseStrategy.FromContent => _htmlParsingService.GetSrcAttributeFromFirstImgElement(itemContent),
                ImageUrlParseStrategy.FromElementExtension => elementExtensionIndex is null || elementExtensions is null ?
                    null :
                    elementExtensions.Count() <= elementExtensionIndex.Value ?
                        null :
                        isSavedInHtmlElementWithSrcAttribute == true ?
                            _htmlParsingService.GetSrcAttributeFromFirstImgElement(elementExtensions.ElementAt(elementExtensionIndex.Value)) :
                            elementExtensions.ElementAt(elementExtensionIndex.Value),
                ImageUrlParseStrategy.SecondLinkOrFromSummary or _ => itemLinks?.Count() > 1 ?
                    itemLinks.ElementAt(1)?.ToString() :
                    _htmlParsingService.GetSrcAttributeFromFirstImgElement(itemSummary)
            };

            var shouldImageBeWebScraped = rssFeed.ImageUrlParseConfiguration.ShouldImageUrlBeWebScraped is null ?
                string.IsNullOrEmpty(imageUrl) : rssFeed.ImageUrlParseConfiguration.ShouldImageUrlBeWebScraped.Value;

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
                var arguments = new (string, object)[]{
                    (nameof(rssFeedUrl), rssFeedUrl),
                    (nameof(newsPortalName), newsPortalName),
                    (nameof(imageUrl), imageUrl ?? string.Empty),
                };

                _loggerService.Log(eventName, LogLevel.Information, arguments);
            }

            imageUrl = AddBaseUrlToUrlFragment(imageUrl, rssFeed.NewsPortal?.BaseUrl);

            return imageUrl;
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
            else if (urlFragmentOrFullUrl.StartsWith("http"))
            {
                return urlFragmentOrFullUrl;
            }
            else
            {
                return $"{baseUrl}{urlFragmentOrFullUrl}";
            }
        }

        private DateTime? GetPublishDateTime(DateTimeOffset itemPublishDateTime, DateTime utcNow)
        {
            var invalidPublishdateTimeMinimum = utcNow - _settingProvider.LatestSetting.ArticleSetting.MaxAgeOfArticle;
            var minimumPublishDateTime = utcNow.AddDays(-1);
            var maximumPublishDateTime = utcNow;
            var rssFeedPublishDateTime = itemPublishDateTime.UtcDateTime;

            if (rssFeedPublishDateTime < invalidPublishdateTimeMinimum)
            {
                return null;
            }
            else if (rssFeedPublishDateTime > maximumPublishDateTime || rssFeedPublishDateTime < minimumPublishDateTime)
            {
                return (DateTime?)utcNow;
            }
            else
            {
                return (DateTime?)rssFeedPublishDateTime;
            }
        }
    }
}
