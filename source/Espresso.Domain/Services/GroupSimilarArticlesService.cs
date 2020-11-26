using System;
using System.Collections.Generic;
using System.Linq;
using Espresso.Domain.Entities;
using Espresso.Domain.IServices;
using Espresso.Domain.Utilities;
using Microsoft.Extensions.Logging;

namespace Espresso.Domain.Services
{
    public class GroupSimilarArticlesService : IGroupSimilarArticlesService
    {
        #region Constants
        private const double MaxSimilarityScore = 1;
        #endregion

        #region Fields
        private readonly double _similarityScoreThreshold;
        private readonly TimeSpan _articlePublishDateTimeDiferenceThreshold;
        private readonly ILoggerService<GroupSimilarArticlesService> _loggerService;
        private readonly TimeSpan _maxAgeOfSimilarArticleChecking;
        private readonly int _minimalNumberOfWordsForArticleToBeComparable;
        #endregion

        #region Constructors
        public GroupSimilarArticlesService(
            double similarityScoreThreshold,
            TimeSpan articlePublishDateTimeDiferenceThreshold,
            ILoggerService<GroupSimilarArticlesService> loggerService,
            TimeSpan maxAgeOfSimilarArticleChecking,
            int minimalNumberOfWordsForArticleToBeComparable
        )
        {
            _similarityScoreThreshold = similarityScoreThreshold;
            _articlePublishDateTimeDiferenceThreshold = articlePublishDateTimeDiferenceThreshold;
            _loggerService = loggerService;
            _maxAgeOfSimilarArticleChecking = maxAgeOfSimilarArticleChecking;
            _minimalNumberOfWordsForArticleToBeComparable = minimalNumberOfWordsForArticleToBeComparable;
        }
        #endregion

        #region Methods
        public IEnumerable<SimilarArticle> GroupSimilarArticles(
            IEnumerable<Article> articles,
            DateTime lastSimilarityGroupingTime
        )
        {
            var maxAgeOfSimilarArticleCheckingDateTime = DateTime.UtcNow - _maxAgeOfSimilarArticleChecking;
            var orderedArticles = articles
                .Where(
                    article => article.PublishDateTime > maxAgeOfSimilarArticleCheckingDateTime &&
                        article.MainArticle is null &&
                        LanguageUtility
                            .SeparateWords(article.Title)
                            .RemoveUnImpactfulCroatianWords()
                            .Count() >= _minimalNumberOfWordsForArticleToBeComparable
                )
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
                    lastSimilarityGroupingTime: lastSimilarityGroupingTime
                );

                similarArticles.AddRange(articlesSimilarArticles);
                notMatchedArticles.Remove(article);

                foreach (var articlesSimilarArticle in articlesSimilarArticles)
                {
                    notMatchedArticles.Remove(articlesSimilarArticle.SubordinateArticle!);
                }

                _loggerService.Log(
                    eventName: "GroupSimilarArticles Batch",
                    logLevel: LogLevel.Trace,
                    namedArguments: new (string argumentName, object argumentValue)[]
                    {
                        ("Total Number Of Articles", totalCount),
                        ("Not Matched Articles Count", notMatchedArticles.Count),
                        ("Current Batch", count++),
                    }
                );
            }
            return similarArticles;
        }

        private IEnumerable<SimilarArticle> GetSimilarArticlesForArticle(
            Article possibleMainArticle,
            IEnumerable<Article> notMatchedArticles,
            DateTime lastSimilarityGroupingTime
        )
        {
            var similarArticles = new List<SimilarArticle>();
            var possibleSimilarArticles = notMatchedArticles
                .Where(
                    notMatchedArticle =>
                        possibleMainArticle.Id != notMatchedArticle.Id &&
                        possibleMainArticle.NewsPortalId != notMatchedArticle.NewsPortalId &&
                        (
                            possibleMainArticle.PublishDateTime > notMatchedArticle.PublishDateTime ?
                                (possibleMainArticle.PublishDateTime - notMatchedArticle.PublishDateTime < _articlePublishDateTimeDiferenceThreshold) :
                                (notMatchedArticle.PublishDateTime - possibleMainArticle.PublishDateTime < _articlePublishDateTimeDiferenceThreshold)
                        ) &&
                        (possibleMainArticle.CreateDateTime > lastSimilarityGroupingTime || notMatchedArticle.CreateDateTime > lastSimilarityGroupingTime)
                );

            foreach (var possibleSimilarArticle in possibleSimilarArticles)
            {
                var similarityScore = CalculateSimilarityScore(
                    possibleMainArticle: possibleMainArticle,
                    possibleSubordinateArticle: possibleSimilarArticle
                );

                if (similarityScore > _similarityScoreThreshold)
                {
                    var similarArticle = new SimilarArticle(
                        id: Guid.NewGuid(),
                        similarityScore: similarityScore,
                        mainArticleId: possibleMainArticle.Id,
                        mainArticle: possibleMainArticle,
                        subordinateArticleId: possibleSimilarArticle.Id,
                        subordinateArticle: possibleSimilarArticle
                    );
                    similarArticles.Add(similarArticle);

                    _loggerService.Log(
                        eventName: "Similar Articles Found",
                        logLevel: LogLevel.Information,
                        namedArguments: new (string argumentName, object argumentValue)[]
                        {
                            ("Similarity Score", similarityScore),
                            ("Main Article Title", possibleMainArticle.Title),
                            ("Subordinate Article Title", possibleSimilarArticle.Title),
                            ("Main Article Source", possibleMainArticle.NewsPortal!.Name),
                            ("Subordinate Article Source", possibleSimilarArticle.NewsPortal!.Name),
                            ("Main Article Category", possibleMainArticle.ArticleCategories.FirstOrDefault()?.Category?.Name!),
                            ("Subordinate Article Category", possibleSimilarArticle.ArticleCategories.FirstOrDefault()?.Category?.Name!),
                        }
                    );
                }
            }

            return similarArticles;
        }

        private static double CalculateSimilarityScore(
            Article possibleMainArticle,
            Article possibleSubordinateArticle
        )
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
                possibleSubordinateArticleWords
            );

            return similarityScore;
        }

        private static double CalculateSimilarityScoreBetweenWords(
            IEnumerable<string> firstArticleWords,
            IEnumerable<string> secondArticleWords
        )
        {
            var totalSimilarityScore = 0.0d;
            foreach (var firstArticleWord in firstArticleWords)
            {
                var bestSecondArticleMatchScore = 0.0d;

                foreach (var secondArticleWord in secondArticleWords)
                {
                    var currentSimilarityScore = CalculateSimilarityScoreBetweenTwoWords(firstArticleWord, secondArticleWord);
                    if (currentSimilarityScore == MaxSimilarityScore)
                    {
                        bestSecondArticleMatchScore = MaxSimilarityScore;
                        break;
                    }
                    if (currentSimilarityScore > bestSecondArticleMatchScore)
                    {
                        bestSecondArticleMatchScore = currentSimilarityScore;
                    }
                }
                totalSimilarityScore += bestSecondArticleMatchScore;
            }

            var averageSimilarityScore = totalSimilarityScore / firstArticleWords.Count();

            return averageSimilarityScore;
        }

        private static double CalculateSimilarityScoreBetweenTwoWords(
            string firstWord,
            string secondWord
        )
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
            return matchedCases / length;
        }
        #endregion
    }
}