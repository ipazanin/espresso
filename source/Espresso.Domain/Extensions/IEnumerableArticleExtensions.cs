using System;
using System.Collections.Generic;
using System.Linq;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.Common.Extensions
{
    public static class IEnumerableArticleExtensions
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

        #region Where
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
        #endregion

        #region Order
        public static IEnumerable<Article> OrderFeaturedArticles(
            this IEnumerable<Article> articles,
            IEnumerable<int>? categoryIds
        )
        {
            var categoriesWithOrderIndex = categoryIds
                ?.Select((category, index) => (category, index))
                .ToDictionary(
                    categoryWithOrderIndex => categoryWithOrderIndex.category,
                    categoryWithOrderIndex => categoryWithOrderIndex.index
                );

            var halfOfMaxValue = int.MaxValue / 2;

            var orderedArticles = articles
                .OrderBy(
                    article => article.EditorConfiguration.FeaturedPosition == null ?
                        (
                            categoriesWithOrderIndex == null ?
                                halfOfMaxValue :
                                halfOfMaxValue + categoriesWithOrderIndex[article.ArticleCategories.First().CategoryId]
                        ) :
                        article.EditorConfiguration.FeaturedPosition
                )
                .OrderArticlesByTrendingScore();

            return orderedArticles;
        }

        public static IEnumerable<Article> OrderArticlesByCategory(
            this IEnumerable<Article> articles,
            IEnumerable<int>? categoryIds
        )
        {
            if (categoryIds is null)
            {
                return articles;
            }

            var categoriesWithOrderIndex = categoryIds
                .Select((category, index) => (category, index))
                .ToDictionary(
                    categoryWithOrderIndex => categoryWithOrderIndex.category,
                    categoryWithOrderIndex => categoryWithOrderIndex.index
                );

            var orderedArticles = articles
                .OrderBy(article => categoriesWithOrderIndex[article.ArticleCategories.First().CategoryId])
                .OrderArticlesByTrendingScore();

            return orderedArticles;
        }

        public static IEnumerable<Article> OrderArticlesByPublishDate(
            this IEnumerable<Article> articles
        )
        {
            var orderedArticles = articles.OrderByDescending(article => article.PublishDateTime);

            return orderedArticles;
        }

        public static IEnumerable<Article> OrderArticlesByTrendingScore(
            this IEnumerable<Article> articles
        )
        {
            var orderedArticles = articles.OrderByDescending(article => article.TrendingScore);

            return orderedArticles;
        }
        #endregion
    }
}