// GroupSimilarArticlesService.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Collections.Concurrent;
using System.Globalization;
using Espresso.Domain.Entities;
using Espresso.Domain.Infrastructure;
using Espresso.Domain.IServices;
using Espresso.Domain.Utilities;
using Microsoft.Extensions.Logging;

namespace Espresso.Domain.Services;

/// <inheritdoc/>
public class GroupSimilarArticlesService : IGroupSimilarArticlesService
{
    private const double MaxSimilarityScore = 1d;
    private readonly ISettingProvider _settingProvider;
    private readonly ILoggerService<GroupSimilarArticlesService> _loggerService;

    /// <summary>
    /// Initializes a new instance of the <see cref="GroupSimilarArticlesService"/> class.
    /// </summary>
    /// <param name="settingProvider">Setting provider.</param>
    /// <param name="loggerService">Logger service.</param>
    public GroupSimilarArticlesService(
        ISettingProvider settingProvider,
        ILoggerService<GroupSimilarArticlesService> loggerService)
    {
        _settingProvider = settingProvider;
        _loggerService = loggerService;
    }

    /// <inheritdoc/>
    public IEnumerable<SimilarArticle> GroupSimilarArticles(
        IEnumerable<Article> articles,
        DateTimeOffset lastSimilarityGroupingTime)
    {
        var similarArticles = new ConcurrentQueue<SimilarArticle>();

        var maxAgeOfSimilarArticleCheckingDateTime = DateTimeOffset.UtcNow - _settingProvider
            .LatestSetting
            .SimilarArticleSetting
            .MaxAgeOfSimilarArticleChecking;

        var articlesWithFittingCriteria = articles
            .Where(article =>
            {
                if (article.PublishDateTime < maxAgeOfSimilarArticleCheckingDateTime)
                {
                    return false;
                }

                var numberOfComparableArticleTitleWords = LanguageUtility
                    .SeparateWords(article.Title)
                    .RemoveUnImpactfulCroatianWords()
                    .Count();

                var minimalNumberOfWordsForArticleToBeComparable = _settingProvider
                    .LatestSetting
                    .SimilarArticleSetting
                    .MinimalNumberOfWordsForArticleToBeComparable;

                return numberOfComparableArticleTitleWords >= minimalNumberOfWordsForArticleToBeComparable;
            })
            .OrderBy(article => article.PublishDateTime)
            .ToArray();

        var newArticles = articlesWithFittingCriteria
            .Where(article => article.CreateDateTime > lastSimilarityGroupingTime)
            .ToArray();

        var count = 0;
        var groupedArticleIds = new HashSet<(Guid firstArticleId, Guid secondArticleId)>();

        foreach (var article in newArticles)
        {
            var articlesSimilarArticles = GetSimilarArticlesForArticle(
                newArticle: article,
                articlesWithFittingCriteria: articlesWithFittingCriteria,
                groupedArticleIds: groupedArticleIds);

            foreach (var similarArticle in articlesSimilarArticles)
            {
                similarArticles.Enqueue(similarArticle);
                groupedArticleIds.Add((similarArticle.FirstArticleId, similarArticle.SecondArticleId));
            }

            var namedArguments = new (string argumentName, object argumentValue)[]
            {
                    ("Total Number Of Articles", articlesWithFittingCriteria.Length),
                    ("New Articles Count", newArticles.Length),
                    ("Current Batch", count++),
            };

            _loggerService.Log(
                eventName: "GroupSimilarArticles Batch",
                logLevel: LogLevel.Trace,
                namedArguments: namedArguments);
        }

        return similarArticles;
    }

    private static double CalculateSimilarityScore(
        string firstArticleTitle,
        string secondArticleTitle)
    {
        var firstArticleWords = LanguageUtility
            .SeparateWords(firstArticleTitle)
            .RemoveUnImpactfulCroatianWords()
            .RemoveWordsWithLessThanThreeLetters()
            .ToArray();

        var secondArticleWords = LanguageUtility
            .SeparateWords(secondArticleTitle)
            .RemoveUnImpactfulCroatianWords()
            .RemoveWordsWithLessThanThreeLetters()
            .ToArray();

        var similarityScore = CalculateSimilarityScoreBetweenWords(
            firstArticleWords,
            secondArticleWords);

        return similarityScore;
    }

    private static double CalculateSimilarityScoreBetweenWords(
        IReadOnlyList<string> firstArticleWords,
        IReadOnlyList<string> secondArticleWords)
    {
        var totalSimilarityScore = 0.0d;
        foreach (var firstArticleWord in firstArticleWords)
        {
            var bestSecondArticleTitleMatchScore = 0.0d;

            foreach (var secondArticleWord in secondArticleWords)
            {
                var currentArticleTitleSimilarityScore = CalculateSimilarityScoreBetweenTwoWords(firstArticleWord, secondArticleWord);
                if (currentArticleTitleSimilarityScore == MaxSimilarityScore)
                {
                    bestSecondArticleTitleMatchScore = MaxSimilarityScore;
                    break;
                }

                if (currentArticleTitleSimilarityScore > bestSecondArticleTitleMatchScore)
                {
                    bestSecondArticleTitleMatchScore = currentArticleTitleSimilarityScore;
                }
            }

            totalSimilarityScore += bestSecondArticleTitleMatchScore;
        }

        var averageSimilarityScore = totalSimilarityScore / firstArticleWords.Count;

        return averageSimilarityScore;
    }

    private static double CalculateSimilarityScoreBetweenTwoWords(
        string firstWord,
        string secondWord)
    {
        var length = firstWord.Length < secondWord.Length ? firstWord.Length : secondWord.Length;
        var matchedCases = 0;

        var upperCaseFirstWord = firstWord.ToUpperInvariant();
        var upperCaseSecondWord = secondWord.ToUpperInvariant();

        for (var i = 0; i < length; i++)
        {
            if (upperCaseFirstWord[i].Equals(upperCaseSecondWord[i]))
            {
                matchedCases++;
            }
        }

        return matchedCases / (double)length;
    }

    private IEnumerable<SimilarArticle> GetSimilarArticlesForArticle(
        Article newArticle,
        IEnumerable<Article> articlesWithFittingCriteria,
        ISet<(Guid firstArticleId, Guid secondArticleId)> groupedArticleIds)
    {
        var possibleSimilarArticles = GetPossibleArticleMatches(
            newArticle: newArticle,
            articlesWithFittingCriteria: articlesWithFittingCriteria,
            groupedArticleIds: groupedArticleIds);

        foreach (var possibleSimilarArticle in possibleSimilarArticles)
        {
            var similarityScore = CalculateSimilarityScore(
                firstArticleTitle: newArticle.Title,
                secondArticleTitle: possibleSimilarArticle.Title);

            if (similarityScore < _settingProvider.LatestSetting.SimilarArticleSetting.SimilarityScoreThreshold)
            {
                continue;
            }

            var similarArticle = new SimilarArticle(
                id: Guid.NewGuid(),
                similarityScore: similarityScore,
                mainArticleId: newArticle.Id,
                mainArticle: null,
                subordinateArticleId: possibleSimilarArticle.Id,
                subordinateArticle: null);

            var namedArguments = new (string argumentName, object argumentValue)[]
            {
                        ("Similarity Score", similarityScore),
                        ("Main Article Title", newArticle.Title),
                        ("Subordinate Article Title", possibleSimilarArticle.Title),
                        ("Main Article Source", newArticle.NewsPortal?.Name ?? newArticle.NewsPortalId.ToString(CultureInfo.CurrentCulture)),
                        ("Subordinate Article Source", possibleSimilarArticle.NewsPortal?.Name ?? possibleSimilarArticle.NewsPortalId.ToString(CultureInfo.CurrentCulture)),
            };

            _loggerService.Log(
                eventName: "Similar Articles Found",
                logLevel: LogLevel.Information,
                namedArguments: namedArguments);

            yield return similarArticle;
        }
    }

    private IEnumerable<Article> GetPossibleArticleMatches(
        Article newArticle,
        IEnumerable<Article> articlesWithFittingCriteria,
        ISet<(Guid firstArticleId, Guid secondArticleId)> groupedArticleIds)
    {
        foreach (var articleWithFittingCriteria in articlesWithFittingCriteria)
        {
            if (newArticle.Id == articleWithFittingCriteria.Id)
            {
                continue;
            }

            if (newArticle.NewsPortalId == articleWithFittingCriteria.NewsPortalId)
            {
                continue;
            }

            if (groupedArticleIds.Contains((newArticle.Id, articleWithFittingCriteria.Id)) ||
                groupedArticleIds.Contains((articleWithFittingCriteria.Id, newArticle.Id)))
            {
                continue;
            }

            var timeDifference = newArticle.PublishDateTime > articleWithFittingCriteria.PublishDateTime ?
                newArticle.PublishDateTime - articleWithFittingCriteria.PublishDateTime :
                articleWithFittingCriteria.PublishDateTime - newArticle.PublishDateTime;

            if (timeDifference > _settingProvider.LatestSetting.SimilarArticleSetting.ArticlePublishDateTimeDifferenceThreshold)
            {
                continue;
            }

            yield return articleWithFittingCriteria;
        }
    }
}
