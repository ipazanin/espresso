using System.Collections.Generic;
using System.Linq;
using Espresso.Domain.Entities;

namespace Espresso.Common.Extensions
{
    public static class OrderArticleCollectionExtensions
    {
        #region Methods
        public static IOrderedEnumerable<Article> OrderFeaturedArticles(
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
                                (
                                    categoriesWithOrderIndex.ContainsKey(article.ArticleCategories.First().CategoryId) ?
                                        halfOfMaxValue + categoriesWithOrderIndex[article.ArticleCategories.First().CategoryId] :
                                        int.MaxValue
                                )
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

            var halfOfMaxValue = int.MaxValue / 2;

            var orderedArticles = articles
                .OrderBy(
                    article => categoriesWithOrderIndex == null ?
                        halfOfMaxValue :
                        (
                            categoriesWithOrderIndex.ContainsKey(article.ArticleCategories.FirstOrDefault()?.CategoryId ?? 0) ?
                                halfOfMaxValue + categoriesWithOrderIndex[article.ArticleCategories.First().CategoryId] :
                                int.MaxValue
                        )
                )
                .OrderArticlesByTrendingScore();

            return orderedArticles;
        }

        public static IOrderedEnumerable<Article> OrderArticlesByPublishDate(
            this IEnumerable<Article> articles
        )
        {
            var orderedArticles = articles.OrderByDescending(article => article.PublishDateTime);

            return orderedArticles;
        }

        public static IOrderedEnumerable<Article> OrderArticlesByTrendingScore(
            this IEnumerable<Article> articles
        )
        {
            var orderedArticles = articles.OrderByDescending(article => article.TrendingScore);

            return orderedArticles;
        }

        public static IOrderedEnumerable<Article> OrderArticlesByTrendingScore(
            this IOrderedEnumerable<Article> articles
        )
        {
            var orderedArticles = articles.ThenByDescending(article => article.TrendingScore);

            return orderedArticles;
        }
        #endregion
    }
}
