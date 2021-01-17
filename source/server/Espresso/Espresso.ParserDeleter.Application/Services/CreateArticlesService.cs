using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Domain.IServices;
using Espresso.Common.Enums;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Common.Extensions;
using Espresso.Domain.Records;
using Espresso.Domain.ValueObjects.ArticleValueObjects;
using Espresso.ParserDeleter.Application.IServices;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Espresso.ParserDeleter.Application.Services
{
    public class CreateArticleService : ICreateArticleService
    {
        #region Fields
        private readonly IScrapeWebService _webScrapingService;
        private readonly IParseHtmlService _htmlParsingService;
        private readonly IValidator<ArticleData> _articleDataValidator;
        private readonly ILoggerService<CreateArticleService> _loggerService;
        private readonly TimeSpan _maxAgeOfArticle;
        #endregion

        #region Constructors
        public CreateArticleService(
            IScrapeWebService webScrapingService,
            IParseHtmlService htmlParsingService,
            IValidator<ArticleData> articleDataValidator,
            ILoggerService<CreateArticleService> loggerService,
            TimeSpan maxAgeOfArticle
        )
        {
            _webScrapingService = webScrapingService;
            _htmlParsingService = htmlParsingService;
            _articleDataValidator = articleDataValidator;
            _loggerService = loggerService;
            _maxAgeOfArticle = maxAgeOfArticle;
        }
        #endregion

        #region Public Methods
        public async Task<(Article? article, bool isValid)> CreateArticleAsync(
            RssFeedItem rssFeedItem,
            IEnumerable<Category> categories,
            CancellationToken cancellationToken
        )
        {
            var id = Guid.NewGuid();
            var utcNow = DateTime.UtcNow;
            var initialNumberOfClicks = 0;

            var title = rssFeedItem.Title;

            var url = GetUrl(
                rssFeed: rssFeedItem.RssFeed,
                itemLinks: rssFeedItem.Links,
                itemId: rssFeedItem.Id
            );

            var webUrl = GetNormalUrl(
                rssFeed: rssFeedItem.RssFeed,
                itemLinks: rssFeedItem.Links
            );

            var summary = GetSummary(
                itemSummary: rssFeedItem.Summary,
                itemTitle: rssFeedItem.Title
            );

            var imageUrl = await GetImageUrl(
                itemLinks: rssFeedItem.Links,
                itemSummary: rssFeedItem.Summary,
                itemContent: rssFeedItem.Content,
                rssFeed: rssFeedItem.RssFeed,
                webUrl: webUrl,
                elementExtensions: rssFeedItem.ElementExtensions,
                cancellationToken: cancellationToken
            );

            var publishDateTime = GetPublishDateTime(
                itemPublishDateTime: rssFeedItem.PublishDateTime,
                utcNow: utcNow
            );

            var articlecategories = GetArticleCategories(
                categories: categories,
                itemTitle: rssFeedItem.Title,
                itemSummary: summary,
                articleId: id,
                itemUrl: rssFeedItem.Links?.FirstOrDefault(),
                rssFeed: rssFeedItem.RssFeed
            );

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
                ArticleCategories = articlecategories
            };

            return CreateArticle(
                articleData: articleData,
                rssFeed: rssFeedItem.RssFeed
            );
        }
        #endregion

        #region Private Methods
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
                    newsPortal: rssFeed.NewsPortal,
                    rssFeed: rssFeed,
                    subordinateArticles: null,
                    mainArticle: null
                );

                return (article, true);
            }
            else
            {
                var rssFeedUrl = rssFeed.Url;
                var exceptionMessage = validationResult.ToString();
                var eventName = Event.CreateArticle.GetDisplayName();
                var parameters = new (string, object)[]
                {
                    (nameof(rssFeedUrl), rssFeedUrl)
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
                string.IsNullOrEmpty(rssFeed.AmpConfiguration?.TemplateUrl)
            )
            {
                return null;
            }

            var articleId = $"{itemId[(itemId.IndexOf("id=") + 3)..]}/";
            var urlSegments = itemLinks.FirstOrDefault()?.Segments ?? Array.Empty<string>();

            var firstArticleSegment = urlSegments.Length < 2 ? "" : urlSegments[1].ToLower();
            var secondArticleSegment = urlSegments.Length < 3 ? "" : urlSegments[2].ToLower();
            var thirdArticleSegment = urlSegments.Length < 4 ? "" : urlSegments[3].ToLower();
            var fourthArticleSegment = urlSegments.Length < 5 ? "" : urlSegments[4].ToLower();

            var articleUrl = string.Format(
                rssFeed.AmpConfiguration.TemplateUrl,
                articleId,
                firstArticleSegment,
                secondArticleSegment,
                thirdArticleSegment,
                fourthArticleSegment
            );

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
            CancellationToken cancellationToken
        )
        {
            string? imageUrl;
            switch (rssFeed.ImageUrlParseConfiguration.ImageUrlParseStrategy)
            {
                case ImageUrlParseStrategy.SecondLinkOrFromSummary:
                default:
                    imageUrl = itemLinks?.Count() > 1 ? itemLinks.ElementAt(1)?.ToString() : null;
                    if (imageUrl is null)
                    {
                        imageUrl = _htmlParsingService.GetSrcAttributeFromFirstImgElement(itemSummary);
                    }

                    break;
                case ImageUrlParseStrategy.FromContent:
                    imageUrl = _htmlParsingService.GetSrcAttributeFromFirstImgElement(itemContent);
                    break;
                case ImageUrlParseStrategy.FromFirstElementExtension:
                    imageUrl = elementExtensions?.FirstOrDefault();
                    break;
                case ImageUrlParseStrategy.FromSecondElementExtension:
                    if (elementExtensions != null)
                    {
                        var htmlString = elementExtensions.Count() > 1 ? elementExtensions?.ElementAt(1) : null;
                        imageUrl = _htmlParsingService.GetSrcAttributeFromFirstImgElement(htmlString);
                    }
                    else
                    {
                        imageUrl = null;
                    }
                    break;
            }

            var shouldImageBeWebScraped = rssFeed.ImageUrlParseConfiguration.ShouldImageUrlBeWebScraped is null ?
                string.IsNullOrEmpty(imageUrl) : rssFeed.ImageUrlParseConfiguration.ShouldImageUrlBeWebScraped.Value;

            if (shouldImageBeWebScraped)
            {
                imageUrl = await _webScrapingService.GetSrcAttributeFromElementDefinedByXPath(
                    articleUrl: webUrl,
                    requestType: RequestType.Browser,
                    imageUrlParseConfiguration: rssFeed.ImageUrlParseConfiguration,
                    cancellationToken: cancellationToken
                );

                var eventName = Event.ImageUrlWebScrapingData.GetDisplayName();
                var rssFeedUrl = rssFeed.Url;
                var newsPortalName = rssFeed.NewsPortal?.Name ?? "";
                var arguments = new (string, object)[]{
                    (nameof(rssFeedUrl), rssFeedUrl),
                    (nameof(newsPortalName), newsPortalName),
                    (nameof(imageUrl), imageUrl ?? "")
                };

                _loggerService.Log(eventName, LogLevel.Information, arguments);
            }

            imageUrl = AddBaseUrlToUrlFragment(imageUrl, rssFeed.NewsPortal?.BaseUrl);

            return imageUrl;
        }

        private DateTime? GetPublishDateTime(DateTimeOffset itemPublishDateTime, DateTime utcNow)
        {
            var invalidPublishdateTimeMinimum = utcNow - _maxAgeOfArticle;
            var minimumPublishDateTime = utcNow.AddDays(-1);
            var maximumPublishDateTime = utcNow;
            var rssFeedPublishDateTime = itemPublishDateTime.UtcDateTime;

            return rssFeedPublishDateTime < invalidPublishdateTimeMinimum
                ? null
                : rssFeedPublishDateTime > maximumPublishDateTime || rssFeedPublishDateTime < minimumPublishDateTime
                ? (DateTime?)utcNow
                : (DateTime?)rssFeedPublishDateTime;
        }

        private static IEnumerable<ArticleCategory> GetArticleCategories(
            IEnumerable<Category> categories,
            string? itemTitle,
            string? itemSummary,
            Guid articleId,
            Uri? itemUrl,
            RssFeed rssFeed
        )
        {
            var articleCategories = new HashSet<ArticleCategory>();
            var articleCategoriesFromKeyWords = GetArticleCategoriesFromCategorysKeyWords(
                categories: categories,
                itemTitle: itemTitle,
                itemSummary: itemSummary,
                articleId: articleId
            );
            articleCategories.UnionWith(articleCategoriesFromKeyWords);

            switch (rssFeed.CategoryParseConfiguration.CategoryParseStrategy)
            {
                case CategoryParseStrategy.Undefined:
                default:
                case CategoryParseStrategy.FromRssFeed:
                    var articleCategoriesFromRssFeed = GetArticleCategoriesFromRssFeed(
                        articleId: articleId,
                        rssFeed: rssFeed
                    );
                    articleCategories.UnionWith(articleCategoriesFromRssFeed);
                    break;
                case CategoryParseStrategy.FromUrl:
                    var articleCategoriesFromUrl = GetArticlecategoriesFromFromUrl(
                        articleId: articleId,
                        itemUrl: itemUrl,
                        rssFeed: rssFeed
                    );
                    articleCategories.UnionWith(articleCategoriesFromUrl);
                    break;
            }

            return articleCategories;
        }

        private static IEnumerable<ArticleCategory> GetArticleCategoriesFromCategorysKeyWords(
            IEnumerable<Category> categories,
            string? itemTitle,
            string? itemSummary,
            Guid articleId
        )
        {
            var categoryIds = new HashSet<ArticleCategory>();
            foreach (var category in categories)
            {
                if (
                    !string.IsNullOrEmpty(category.KeyWordsRegexPattern) &&
                    Regex.IsMatch($"{itemTitle} {itemSummary}", category.KeyWordsRegexPattern, RegexOptions.IgnoreCase)
                )
                {
                    categoryIds.Add(new ArticleCategory(
                        id: Guid.NewGuid(),
                        articleId: articleId,
                        categoryId: category.Id,
                        article: null,
                        category: category
                    ));
                }
            }

            return categoryIds;
        }

        private static IEnumerable<ArticleCategory> GetArticlecategoriesFromFromUrl(
            Guid articleId,
            Uri? itemUrl,
            RssFeed rssFeed
        )
        {
            var articleCategories = new HashSet<ArticleCategory>();

            foreach (var rssFeedCategory in rssFeed.RssFeedCategories)
            {
                if (
                    itemUrl?.Segments != null &&
                    itemUrl.Segments.Length > rssFeedCategory.UrlSegmentIndex
                )
                {
                    var secondUrlSegment = itemUrl?
                        .Segments[rssFeedCategory.UrlSegmentIndex]
                        .Replace("/", "").ToLower();

                    if (
                        !string.IsNullOrEmpty(rssFeedCategory.UrlRegex) &&
                        !string.IsNullOrEmpty(secondUrlSegment) &&
                        Regex.IsMatch(secondUrlSegment, rssFeedCategory.UrlRegex, RegexOptions.IgnoreCase)
                    )
                    {
                        articleCategories.Add(new ArticleCategory(
                            id: Guid.NewGuid(),
                            articleId: articleId,
                            categoryId: rssFeedCategory.CategoryId,
                            article: null,
                            category: rssFeedCategory.Category
                        ));
                    }
                }
            }

            return articleCategories;
        }

        private static IEnumerable<ArticleCategory> GetArticleCategoriesFromRssFeed(
            Guid articleId,
            RssFeed rssFeed
        )
        {
            yield return new ArticleCategory(
                id: Guid.NewGuid(),
                articleId: articleId,
                categoryId: rssFeed.CategoryId,
                article: null,
                category: rssFeed.Category
            );
        }

        private static string? AddBaseUrlToUrlFragment(string? urlFragmentOrFullUrl, string? baseUrl)
        {
            if (
                string.IsNullOrEmpty(urlFragmentOrFullUrl) ||
                string.IsNullOrEmpty(baseUrl)
            )
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
        #endregion
    }
}
