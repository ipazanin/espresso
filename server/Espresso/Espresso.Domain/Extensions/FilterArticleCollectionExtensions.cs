using System;
using System.Collections.Generic;
using System.Linq;
using Espresso.Domain.Entities;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.CategoryEnums;
using Espresso.Domain.Utilities;

namespace Espresso.Domain.Extensions
{
    public static class FilterArticleCollectionExtensions
    {
        #region Constants
        public static IEnumerable<string> BannedKeywords => new List<string>
            {
                "virus",
                "pandemij",
                "koron",
                "coron",
                "capak",
                "božinović",
                "bozinovic",
                "markotić",
                "markotic",
                "mask",
                "karanten",
                "umrlih",
                "zaraz",
                "zaraž",
                "staracki dom",
                "starački dom",
                "bolnica",
                "prva pomoc",
                "prva pomoć",
                "hitna",
                "zabran",
                "pozitiv",
                "testir",
                "slučaj",
                "slucaj",
                "covid",
                "stozer",
                "stožer",
                "respirator",
                "bolest",
                "ambulant"
            };
        #endregion

        #region Methods
        public static IEnumerable<Article> FilterArticlesWithCoronaVirusContentForIosRelease(
            this IEnumerable<Article> articles,
            DeviceType deviceType,
            string targetedApiVersion
        )
        {
            if (!(deviceType == DeviceType.Ios && targetedApiVersion == "2.0"))
            {
                return articles;
            }

            var filteredArticles = articles.Where(
                article => !BannedKeywords.Any(
                    bannedKeyword => article.Title.Contains(bannedKeyword, StringComparison.InvariantCultureIgnoreCase) ||
                        article.Summary.Contains(bannedKeyword, StringComparison.InvariantCultureIgnoreCase)
                )
            );

            return filteredArticles;
        }

        public static IEnumerable<Article> FilterArticles(
            this IEnumerable<Article> articles,
            IEnumerable<int>? categoryIds,
            IEnumerable<int>? newsPortalIds,
            string? titleSearchTerm,
            DateTime? articleCreateDateTime
        )
        {
            var articleMinimumAge = articleCreateDateTime ?? DateTime.UtcNow;
            var searchTerms = LanguageUtility.GetSearchTerms(titleSearchTerm);

            var filteredArticles = articles
                .Where(
                    article =>
                        !article.EditorConfiguration.IsHidden &&
                        article.CreateDateTime <= articleMinimumAge &&
                        article.MainArticle == null &&
                        (categoryIds == null || article
                            .ArticleCategories
                            .Any(articleCategory => categoryIds.Contains(articleCategory.CategoryId))) &&
                        (newsPortalIds == null || newsPortalIds.Contains(article.NewsPortalId)) &&
                        (
                            searchTerms == null ||
                            searchTerms
                                .All(searchTerm => article
                                    .Title
                                    .ReplaceCroatianCharacters()
                                    .Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase)
                                )
                        )
                );

            return filteredArticles;
        }

        public static IEnumerable<Article> FilterArticles(
            this IEnumerable<Article> articles,
            int categoryId,
            IEnumerable<int>? newsPortalIds,
            string? titleSearchTerm,
            DateTime? articleCreateDateTime
        )
        {
            var categoryIds = new[] { categoryId };

            var filteredArticles = articles.FilterArticles(
                categoryIds: categoryIds,
                newsPortalIds: newsPortalIds,
                titleSearchTerm: titleSearchTerm,
                articleCreateDateTime: articleCreateDateTime
            );

            return filteredArticles;
        }

        public static IEnumerable<Article> FilterArticles_2_0(
                    this IEnumerable<Article> articles,
                    IEnumerable<int>? categoryIds,
                    IEnumerable<int>? newsPortalIds,
                    string? titleSearchTerm,
                    DateTime? articleCreateDateTime
                )
        {
            var articleMinimumAge = articleCreateDateTime ?? DateTime.UtcNow;
            var searchTerms = LanguageUtility.GetSearchTerms(titleSearchTerm);

            var filteredArticles = articles
                .Where(
                    article =>
                        !article.EditorConfiguration.IsHidden &&
                        article.CreateDateTime <= articleMinimumAge &&
                        (categoryIds == null || article
                            .ArticleCategories
                            .Any(articleCategory => categoryIds.Contains(articleCategory.CategoryId))) &&
                        (newsPortalIds == null || newsPortalIds.Contains(article.NewsPortalId)) &&
                        (
                            searchTerms == null ||
                            searchTerms
                                .All(searchTerm => article
                                    .Title
                                    .ReplaceCroatianCharacters()
                                    .Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase)
                                )
                        )
                );

            return filteredArticles;
        }

        public static IEnumerable<Article> FilterArticles_2_0(
            this IEnumerable<Article> articles,
            int categoryId,
            IEnumerable<int>? newsPortalIds,
            string? titleSearchTerm,
            DateTime? articleCreateDateTime
        )
        {
            var categoryIds = new[] { categoryId };

            var filteredArticles = articles.FilterArticles_2_0(
                categoryIds: categoryIds,
                newsPortalIds: newsPortalIds,
                titleSearchTerm: titleSearchTerm,
                articleCreateDateTime: articleCreateDateTime
            );

            return filteredArticles;
        }

        public static IEnumerable<Article> FiltereFeaturedArticles(
            this IEnumerable<Article> articles,
            IEnumerable<int>? categoryIds,
            IEnumerable<int>? newsPortalIds,
            TimeSpan maxAgeOfFeaturedArticle,
            DateTime? articleCreateDateTime
        )
        {
            var articleMinimumAge = articleCreateDateTime ?? DateTime.UtcNow;
            var maxDateTimeOfFeaturedArticle = DateTime.UtcNow - maxAgeOfFeaturedArticle;

            var filteredArticles = articles
                .Where(
                    article =>
                        !article.EditorConfiguration?.IsHidden == true &&
                        article.EditorConfiguration?.IsFeatured == true &&
                        article.CreateDateTime <= articleMinimumAge &&
                        article.PublishDateTime > maxDateTimeOfFeaturedArticle &&
                        (categoryIds == null || article
                            .ArticleCategories
                            .Any(articleCategory => categoryIds.Contains(articleCategory.CategoryId))) &&
                        (newsPortalIds == null || newsPortalIds.Contains(article.NewsPortalId))
                );

            return filteredArticles;
        }


        public static IEnumerable<Article> FiltereTrendingArticles(
            this IEnumerable<Article> articles,
            TimeSpan maxAgeOfTrendingArticle,
            DateTime? articleCreateDateTime
        )
        {
            var maxTrendingDateTime = DateTime.UtcNow - maxAgeOfTrendingArticle;

            var articleMinimumAge = articleCreateDateTime ?? DateTime.UtcNow;

            var filteredArticles = articles
                .Where(
                    article =>
                        !article.EditorConfiguration?.IsHidden == true &&
                        article.EditorConfiguration?.IsFeatured != false &&
                        article.CreateDateTime <= articleMinimumAge &&
                        article.PublishDateTime > maxTrendingDateTime &&
                        !article.ArticleCategories.Any(articleCategory => articleCategory.Category!.CategoryType == CategoryType.Local)
                );

            return filteredArticles;
        }
        #endregion
    }
}