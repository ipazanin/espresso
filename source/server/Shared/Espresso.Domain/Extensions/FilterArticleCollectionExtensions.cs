// FilterArticleCollectionExtensions.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Enums;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;
using Espresso.Domain.Extensions;
using Espresso.Domain.Utilities;

namespace Espresso.Domain.Extensions;

public static class FilterArticleCollectionExtensions
{
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
                "ambulant",
            };

    public static IEnumerable<Article> FilterArticlesWithCoronaVirusContentForIosRelease(
        this IEnumerable<Article> articles,
        DeviceType deviceType,
        string targetedApiVersion)
    {
        if (!(deviceType == DeviceType.Ios && targetedApiVersion == "2.0"))
        {
            return articles;
        }

        var filteredArticles = articles.Where(
            article => !BannedKeywords.Any(
                bannedKeyword => article.Title.Contains(bannedKeyword, StringComparison.InvariantCultureIgnoreCase) ||
                    article.Summary.Contains(bannedKeyword, StringComparison.InvariantCultureIgnoreCase)));

        return filteredArticles;
    }

    public static IEnumerable<Article> FilterArticlesContainingKeyWords(this IEnumerable<Article> articles, IEnumerable<string> keywords)
    {
        return articles
            .Where(article => !keywords.Any(keyword => article.Title.Contains(keyword, StringComparison.InvariantCultureIgnoreCase)));
    }

    public static IEnumerable<Article> FilterArticles(
        this IEnumerable<Article> articles,
        IEnumerable<int>? categoryIds,
        IEnumerable<int>? newsPortalIds,
        string? titleSearchTerm,
        DateTimeOffset? articleCreateDateTime)
    {
        var articleMinimumAge = articleCreateDateTime ?? DateTimeOffset.UtcNow;
        var searchTerms = LanguageUtility.GetSearchTerms(titleSearchTerm);

        var filteredArticles = articles
            .Where(
                article =>
                    !article.EditorConfiguration.IsHidden &&
                    article.CreateDateTime <= articleMinimumAge &&
                    (categoryIds == null || article
                        .ArticleCategories
                        .Any(articleCategory => categoryIds.Contains(articleCategory.CategoryId))) &&
                    (newsPortalIds?.Contains(article.NewsPortalId) != false) &&
                    (
                        searchTerms?
                            .All(searchTerm => article
                                .Title
                                .ReplaceCroatianCharacters()
                                .Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase)) != false));

        return filteredArticles;
    }

    public static IEnumerable<Article> FilterArticles(
        this IEnumerable<Article> articles,
        int categoryId,
        IEnumerable<int>? newsPortalIds,
        string? titleSearchTerm,
        DateTimeOffset? articleCreateDateTime)
    {
        var categoryIds = new[] { categoryId };

        var filteredArticles = articles.FilterArticles(
            categoryIds: categoryIds,
            newsPortalIds: newsPortalIds,
            titleSearchTerm: titleSearchTerm,
            articleCreateDateTime: articleCreateDateTime);

        return filteredArticles;
    }

    public static IEnumerable<Article> FilterFeaturedArticles(
        this IEnumerable<Article> articles,
        IEnumerable<int>? categoryIds,
        IEnumerable<int>? newsPortalIds,
        TimeSpan maxAgeOfFeaturedArticle,
        DateTimeOffset? articleCreateDateTime)
    {
        var articleMinimumAge = articleCreateDateTime ?? DateTimeOffset.UtcNow;
        var maxDateTimeOfFeaturedArticle = DateTimeOffset.UtcNow - maxAgeOfFeaturedArticle;

        var filteredArticles = articles
            .Where(
                article =>
                    article.EditorConfiguration?.IsHidden == false &&
                    article.EditorConfiguration?.IsFeatured == true &&
                    article.CreateDateTime <= articleMinimumAge &&
                    article.PublishDateTime > maxDateTimeOfFeaturedArticle &&
                    (categoryIds == null || article
                        .ArticleCategories
                        .Any(articleCategory => categoryIds.Contains(articleCategory.CategoryId))) &&
                    (newsPortalIds?.Contains(article.NewsPortalId) != false));

        return filteredArticles;
    }

    public static IEnumerable<Article> FilterTrendingArticles(
        this IEnumerable<Article> articles,
        TimeSpan maxAgeOfTrendingArticle,
        DateTimeOffset? articleCreateDateTime)
    {
        var maxTrendingDateTime = DateTimeOffset.UtcNow - maxAgeOfTrendingArticle;

        var articleMinimumAge = articleCreateDateTime ?? DateTimeOffset.UtcNow;

        var filteredArticles = articles
            .Where(
                article =>
                    article.EditorConfiguration?.IsHidden == false &&
                    article.EditorConfiguration?.IsFeatured != false &&
                    article.CreateDateTime <= articleMinimumAge &&
                    article.PublishDateTime > maxTrendingDateTime &&
                    !article.ArticleCategories.Any(articleCategory => articleCategory.Category!.CategoryType == CategoryType.Local));

        return filteredArticles;
    }

    public static IEnumerable<IEnumerable<Article>> GetGroupedArticlesBySimilarity(
        this IEnumerable<Article> savedArticles,
        DateTimeOffset? firstArticleCreateDateTime,
        IEnumerable<int>? categoryIds,
        IEnumerable<int>? newsPortalIds,
        IEnumerable<string> keyWordsToFilterOut,
        string? titleSearchTerm)
    {
        var filteredArticlesDictionary = savedArticles
            .OrderArticlesByPublishDate()
            .FilterArticles(
                categoryIds: categoryIds,
                newsPortalIds: newsPortalIds,
                titleSearchTerm: titleSearchTerm,
                articleCreateDateTime: firstArticleCreateDateTime)
            .FilterArticlesContainingKeyWords(keyWordsToFilterOut)
            .ToDictionary(article => article.Id);

        var groupedArticles = new List<IEnumerable<Article>>();

        foreach (var article in filteredArticlesDictionary.Values)
        {
            if (!filteredArticlesDictionary.ContainsKey(article.Id))
            {
                continue;
            }

            var articles = new List<Article> { article };
            foreach (var firstSimilarArticle in article.FirstSimilarArticles)
            {
                if (!filteredArticlesDictionary.TryGetValue(firstSimilarArticle.FirstArticleId, out var firstArticle))
                {
                    continue;
                }

                articles.Add(firstArticle);
                filteredArticlesDictionary.Remove(firstSimilarArticle.FirstArticleId);
            }

            foreach (var secondSimilarArticle in article.SecondSimilarArticles)
            {
                if (!filteredArticlesDictionary.TryGetValue(secondSimilarArticle.SecondArticleId, out var secondArticle))
                {
                    continue;
                }

                articles.Add(secondArticle);
                filteredArticlesDictionary.Remove(secondSimilarArticle.SecondArticleId);
            }

            groupedArticles.Add(articles);
        }

        return groupedArticles;
    }
}
