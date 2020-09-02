using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.NewsPortalEnums;
using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.IServices;
using Espresso.Domain.IValidators;

namespace Espresso.Domain.Services
{
    public class ArticleParserService : IArticleParserService
    {
        #region Fields
        private readonly IWebScrapingService _webScrapingService;
        private readonly IArticleValidator _articleValidator;
        #endregion

        #region Constructors
        public ArticleParserService(
            IWebScrapingService webScrapingService,
            IArticleValidator articleValidator
        )
        {
            _webScrapingService = webScrapingService;
            _articleValidator = articleValidator;
        }
        #endregion

        #region Public Methods
        public async Task<Article> CreateArticleAsync(
            RssFeed rssFeed,
            IEnumerable<Category> categories,
            string? itemId,
            IEnumerable<Uri?>? itemLinks,
            string? itemTitle,
            string? itemSummary,
            string? itemContent,
            DateTimeOffset itemPublishDateTime,
            CancellationToken cancellationToken
        )
        {
            var random = new Random();
            var id = Guid.NewGuid();
            var utcNow = DateTime.UtcNow;
            var initialNumberOfClicks = random.Next(0, 15);
            var initialTrendingScore = 0;

            var articleId = GetArticleId(
                itemLinks: itemLinks,
                rssFeed: rssFeed
            );

            var url = GetUrl(
                rssFeed: rssFeed,
                itemLinks: itemLinks,
                itemId: itemId
            );

            var summary = GetSummary(
                itemSummary: itemSummary,
                itemTitle: itemTitle
            );

            var title = itemTitle;

            var imageUrl = await GetImageUrl(
                itemLinks: itemLinks,
                itemSummary: itemSummary,
                itemContent: itemContent,
                rssFeed: rssFeed,
                cancellationToken: cancellationToken
            );

            var publishDateTime = GetPublishDateTime(
                itemPublishDateTime: itemPublishDateTime,
                utcNow: utcNow
            );

            var articlecategories = GetArticleCategories(
                categories: categories,
                itemTitle: itemTitle,
                itemSummary: itemSummary,
                articleId: id,
                itemUrl: itemLinks.FirstOrDefault(),
                rssFeed: rssFeed
            );

            var article = _articleValidator.Validate(
                id: id,
                articleId: articleId,
                url: url,
                summary: summary,
                title: title,
                imageUrl: imageUrl,
                createDateTime: utcNow,
                updateDateTime: utcNow,
                publishDateTime: publishDateTime,
                numberOfClicks: initialNumberOfClicks,
                trendingScore: initialTrendingScore,
                newsPortalId: rssFeed.NewsPortalId,
                rssFeedId: rssFeed.Id,
                articleCategories: articlecategories,
                newsPortal: rssFeed.NewsPortal,
                rssFeed: rssFeed
            );

            return article;
        }
        #endregion

        #region Private Methods

        #region ArticleId
        private string? GetArticleId(IEnumerable<Uri?>? itemLinks, RssFeed rssFeed)
        {
            var url = itemLinks?.FirstOrDefault()?.ToString();
            // TODO: Refactor
            if (
                rssFeed.Id.Equals((int)RssFeedId.IstarskaZupanija) &&
                !string.IsNullOrWhiteSpace(url)
            )
            {
                return rssFeed.NewsPortal!.BaseUrl + url;
            }
            return url;
        }
        #endregion

        #region Url
        private string? GetUrl(RssFeed rssFeed, IEnumerable<Uri?>? itemLinks, string? itemId)
        {
            if (rssFeed.AmpConfiguration?.HasAmpArticles == true)
            {
                return GetAmpUrl(rssFeed, itemLinks, itemId);
            }

            return GetNormalUrl(rssFeed, itemLinks);
        }

        private string? GetNormalUrl(RssFeed rssFeed, IEnumerable<Uri?>? itemLinks)
        {
            var url = itemLinks?.FirstOrDefault()?.ToString();

            var articleUrl = AddBaseUrlToUrlFragment(url, rssFeed.NewsPortal?.BaseUrl);

            return articleUrl;
        }

        private string? GetAmpUrl(RssFeed rssFeed, IEnumerable<Uri?>? itemLinks, string? itemId)
        {
            if (
                itemId is null ||
                itemLinks?.FirstOrDefault() is null ||
                string.IsNullOrEmpty(rssFeed.AmpConfiguration?.TemplateUrl)
            )
            {
                return null;
            }

#pragma warning disable IDE0057
            var articleId = $"{itemId.Substring(itemId.IndexOf("id=") + 3)}/";
#pragma warning restore IDE0057
            var urlSegments = itemLinks.FirstOrDefault()?.Segments ?? new string[0];

            var firstArticleSegment = urlSegments.Count() < 2 ? "" : urlSegments[1].ToLower();
            var secondArticleSegment = urlSegments.Count() < 3 ? "" : urlSegments[2].ToLower();
            var thirdArticleSegment = urlSegments.Count() < 4 ? "" : urlSegments[3].ToLower();
            var fourthArticleSegment = urlSegments.Count() < 5 ? "" : urlSegments[4].ToLower();

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
        #endregion

        #region Summary
        private string? GetSummary(string? itemSummary, string? itemTitle)
        {
            var summary = _webScrapingService.GetText(html: itemSummary);
            return string.IsNullOrEmpty(summary) ? itemTitle : summary;
        }
        #endregion

        #region ImageUrl
        private async Task<string?> GetImageUrl(
            IEnumerable<Uri?>? itemLinks,
            string? itemSummary,
            string? itemContent,
            RssFeed rssFeed,
            CancellationToken cancellationToken
        )
        {
            string? imageUrl;
            switch (rssFeed.ImageUrlParseConfiguration.ImageUrlParseStrategy)
            {
                case ImageUrlParseStrategy.SecondLinkOrFromSummary:
                case ImageUrlParseStrategy.Undefined:
                default:
                    {
                        imageUrl = itemLinks.Count() > 1 ? itemLinks.ElementAt(1)?.ToString() : null;
                        if (imageUrl is null)
                        {
                            imageUrl = _webScrapingService.GetSrcAttributeFromFirstImgElement(itemSummary);
                        }

                        break;
                    }
                case ImageUrlParseStrategy.FromContent:
                    {
                        imageUrl = _webScrapingService.GetSrcAttributeFromFirstImgElement(itemContent);
                        break;
                    }
            }

            if (rssFeed.Id == (int)RssFeedId.Scena)
            {

            }

            if (string.IsNullOrEmpty(imageUrl) || rssFeed.ImageUrlParseConfiguration.ShouldImageUrlBeWebScraped)
            {
                var articleUrl = itemLinks.FirstOrDefault()?.ToString();

                imageUrl = await _webScrapingService.GetSrcAttributeFromElementDefinedByXPath(
                    articleUrl: articleUrl,
                    xPath: rssFeed.ImageUrlParseConfiguration.ImgElementXPath,
                    imageUrlWebScrapeType: rssFeed.ImageUrlParseConfiguration.ImageUrlWebScrapeType,
                    propertyNames: rssFeed.ImageUrlParseConfiguration.GetPropertyNames(),
                    cancellationToken: cancellationToken
                );
            }

            imageUrl = AddBaseUrlToUrlFragment(imageUrl, rssFeed.NewsPortal?.BaseUrl);

            return imageUrl;
        }
        #endregion

        #region PublishDateTime
        private DateTime? GetPublishDateTime(DateTimeOffset itemPublishDateTime, DateTime utcNow)
        {
            var invalidPublishdateTimeMinimum = utcNow.AddDays(-7);
            var minimumPublishDateTime = utcNow.AddDays(-1);
            var maximumPublishDateTime = utcNow;
            var rssFeedPublishDateTime = itemPublishDateTime.UtcDateTime;

            return rssFeedPublishDateTime < invalidPublishdateTimeMinimum
                ? null
                : rssFeedPublishDateTime > maximumPublishDateTime || rssFeedPublishDateTime < minimumPublishDateTime
                ? (DateTime?)utcNow
                : (DateTime?)rssFeedPublishDateTime;
        }
        #endregion

        #region ArticleCategories
        private IEnumerable<ArticleCategory> GetArticleCategories(
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

        private IEnumerable<ArticleCategory> GetArticleCategoriesFromCategorysKeyWords(
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

        private IEnumerable<ArticleCategory> GetArticlecategoriesFromFromUrl(
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

        private IEnumerable<ArticleCategory> GetArticleCategoriesFromRssFeed(
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

        #endregion

        #region Common

        private string? AddBaseUrlToUrlFragment(string? urlFragmentOrFullUrl, string? baseUrl)
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

        #endregion
    }
}
