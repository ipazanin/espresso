// OrderArticleCollectionExtensions.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;

namespace Espresso.Common.Extensions;

public static class OrderArticleCollectionExtensions
{
    public static IOrderedEnumerable<Article> OrderFeaturedArticles(
        this IEnumerable<Article> articles,
        IEnumerable<int>? categoryIds)
    {
        var categoriesWithOrderIndex = categoryIds
            ?.Select((category, index) => (category, index))
            .ToDictionary(
                categoryWithOrderIndex => categoryWithOrderIndex.category,
                categoryWithOrderIndex => categoryWithOrderIndex.index);

        const int HalfOfMaxValue = int.MaxValue / 2;

        var orderedArticles = articles
            .OrderBy(
                article =>
                {
                    if (article.EditorConfiguration.FeaturedPosition is not null)
                    {
                        return article.EditorConfiguration.FeaturedPosition.Value;
                    }

                    if (categoriesWithOrderIndex is null)
                    {
                        return HalfOfMaxValue;
                    }

                    var categoryId = article.ArticleCategories.First().CategoryId;
                    if (categoriesWithOrderIndex.TryGetValue(categoryId, out var orderIndex))
                    {
                        return HalfOfMaxValue + orderIndex;
                    }

                    return int.MaxValue;
                })
            .OrderArticlesByTrendingScore();

        return orderedArticles;
    }

    public static IEnumerable<Article> OrderArticlesByCategory(
        this IEnumerable<Article> articles,
        IEnumerable<int>? categoryIds)
    {
        if (categoryIds is null)
        {
            return articles;
        }

        var categoriesWithOrderIndex = categoryIds
            .Select((category, index) => (category, index))
            .ToDictionary(
                categoryWithOrderIndex => categoryWithOrderIndex.category,
                categoryWithOrderIndex => categoryWithOrderIndex.index);

        const int HalfOfMaxValue = int.MaxValue / 2;

        var orderedArticles = articles
            .OrderBy(
                article =>
                {
                    if (categoriesWithOrderIndex is null)
                    {
                        return HalfOfMaxValue;
                    }

                    var categoryId = article.ArticleCategories.First().CategoryId;
                    if (categoriesWithOrderIndex.TryGetValue(categoryId, out var orderIndex))
                    {
                        return HalfOfMaxValue + orderIndex;
                    }

                    return int.MaxValue;
                })
            .OrderArticlesByTrendingScore();

        return orderedArticles;
    }

    public static IOrderedEnumerable<Article> OrderArticlesByPublishDate(
        this IEnumerable<Article> articles)
    {
        var orderedArticles = articles.OrderByDescending(article => article.PublishDateTime);

        return orderedArticles;
    }

    public static IOrderedEnumerable<Article> OrderArticlesByTrendingScore(
        this IEnumerable<Article> articles)
    {
        var orderedArticles = articles.OrderByDescending(article => article.TrendingScore);

        return orderedArticles;
    }

    public static IOrderedEnumerable<Article> OrderArticlesByTrendingScore(
        this IOrderedEnumerable<Article> articles)
    {
        var orderedArticles = articles.ThenByDescending(article => article.TrendingScore);

        return orderedArticles;
    }
}
