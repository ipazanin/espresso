// GroupSimilarArticlesService.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Espresso.Domain.Infrastructure;
using Espresso.Domain.IServices;
using Espresso.Domain.Utilities;
using Microsoft.Extensions.Logging;

namespace Espresso.Domain.Services
{
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
            ISet<Guid> subordinateArticleIds,
            DateTimeOffset lastSimilarityGroupingTime)
        {
            var maxAgeOfSimilarArticleCheckingDateTime = DateTimeOffset.UtcNow - _settingProvider
                .LatestSetting
                .SimilarArticleSetting
                .MaxAgeOfSimilarArticleChecking;

            var orderedArticles = articles
                .Where(
                    article => article.PublishDateTime > maxAgeOfSimilarArticleCheckingDateTime &&
                        !subordinateArticleIds.Contains(article.Id) &&
                        LanguageUtility
                            .SeparateWords(article.Title)
                            .RemoveUnImpactfulCroatianWords()
                            .Count() >= _settingProvider.LatestSetting.SimilarArticleSetting.MinimalNumberOfWordsForArticleToBeComparable)
                .OrderBy(article => article.PublishDateTime);

            var notMatchedArticles = orderedArticles.ToList();

            var similarArticles = new List<SimilarArticle>();

            var totalCount = orderedArticles.Count();
            var count = 0;

            foreach (var article in orderedArticles)
            {
                var articlesSimilarArticles = GetSimilarArticlesForArticle(
                    possibleMainArticle: article,
                    notMatchedArticles: notMatchedArticles,
                    subordinateArticleIds: subordinateArticleIds,
                    lastSimilarityGroupingTime: lastSimilarityGroupingTime);

                similarArticles.AddRange(articlesSimilarArticles);
                _ = notMatchedArticles.Remove(article);

                var namedArguments = new (string argumentName, object argumentValue)[]
                {
                    ("Total Number Of Articles", totalCount),
                    ("Not Matched Articles Count", notMatchedArticles.Count),
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
            Article possibleMainArticle,
            Article possibleSubordinateArticle)
        {
            var possibleMainArticleWords = LanguageUtility
                .SeparateWords(possibleMainArticle.Title)
                .RemoveUnImpactfulCroatianWords()
                .RemoveWordsWithLessThanThreeLetters();

            var possibleSubordinateArticleWords = LanguageUtility
                .SeparateWords(possibleSubordinateArticle.Title)
                .RemoveUnImpactfulCroatianWords()
                .RemoveWordsWithLessThanThreeLetters();

            var similarityScore = CalculateSimilarityScoreBetweenWords(
                possibleMainArticleWords,
                possibleSubordinateArticleWords);

            return similarityScore;
        }

        private static double CalculateSimilarityScoreBetweenWords(
            IEnumerable<string> firstArticleWords,
            IEnumerable<string> secondArticleWords)
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

            var averageSimilarityScore = totalSimilarityScore / firstArticleWords.Count();

            return averageSimilarityScore;
        }

        private static double CalculateSimilarityScoreBetweenTwoWords(
            string firstWord,
            string secondWord)
        {
            var length = firstWord.Length < secondWord.Length ? firstWord.Length : secondWord.Length;
            var matchedCases = 0;

            var lowerCaseFirstWord = firstWord.ToLower();
            var lowerCaseSecondWord = secondWord.ToLower();

            for (var i = 0; i < length; i++)
            {
                if (lowerCaseFirstWord[i].Equals(lowerCaseSecondWord[i]))
                {
                    matchedCases++;
                }
            }

            return matchedCases / (double)length;
        }

        private IEnumerable<SimilarArticle> GetSimilarArticlesForArticle(
            Article possibleMainArticle,
            IList<Article> notMatchedArticles,
            ISet<Guid> subordinateArticleIds,
            DateTimeOffset lastSimilarityGroupingTime)
        {
            var similarArticles = new List<SimilarArticle>();
            var possibleSimilarArticles = notMatchedArticles
                .Where(
                    notMatchedArticle =>
                        possibleMainArticle.Id != notMatchedArticle.Id &&
                        possibleMainArticle.NewsPortalId != notMatchedArticle.NewsPortalId &&
                        (possibleMainArticle.PublishDateTime > notMatchedArticle.PublishDateTime ?
                                (possibleMainArticle.PublishDateTime - notMatchedArticle.PublishDateTime < _settingProvider.LatestSetting.SimilarArticleSetting.ArticlePublishDateTimeDifferenceThreshold) :
                                (notMatchedArticle.PublishDateTime - possibleMainArticle.PublishDateTime < _settingProvider.LatestSetting.SimilarArticleSetting.ArticlePublishDateTimeDifferenceThreshold)) &&
                        (possibleMainArticle.CreateDateTime > lastSimilarityGroupingTime || notMatchedArticle.CreateDateTime > lastSimilarityGroupingTime))
                .ToList();

            foreach (var possibleSimilarArticle in possibleSimilarArticles)
            {
                var similarityScore = CalculateSimilarityScore(
                    possibleMainArticle: possibleMainArticle,
                    possibleSubordinateArticle: possibleSimilarArticle);

                if (similarityScore > _settingProvider.LatestSetting.SimilarArticleSetting.SimilarityScoreThreshold)
                {
                    var similarArticle = new SimilarArticle(
                        id: Guid.NewGuid(),
                        similarityScore: similarityScore,
                        mainArticleId: possibleMainArticle.Id,
                        mainArticle: null,
                        subordinateArticleId: possibleSimilarArticle.Id,
                        subordinateArticle: null);
                    similarArticles.Add(similarArticle);
                    _ = subordinateArticleIds.Add(similarArticle.SubordinateArticleId);
                    _ = notMatchedArticles.Remove(possibleSimilarArticle);

                    var namedArguments = new (string argumentName, object argumentValue)[]
                    {
                        ("Similarity Score", similarityScore),
                        ("Main Article Title", possibleMainArticle.Title),
                        ("Subordinate Article Title", possibleSimilarArticle.Title),
                        ("Main Article Source", possibleMainArticle.NewsPortal?.Name ?? possibleMainArticle.NewsPortalId.ToString()),
                        ("Subordinate Article Source", possibleSimilarArticle.NewsPortal?.Name ?? possibleSimilarArticle.NewsPortalId.ToString()),
                    };

                    _loggerService.Log(
                        eventName: "Similar Articles Found",
                        logLevel: LogLevel.Information,
                        namedArguments: namedArguments);
                }
            }

            return similarArticles;
        }
    }
}
