using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly int _groupSimilarArticlesBatchSize;
        private readonly int _minimalNumberOfWordsForArticleToBeComparable;
        #endregion

        #region Constructors
        public GroupSimilarArticlesService(
            double similarityScoreThreshold,
            TimeSpan articlePublishDateTimeDiferenceThreshold,
            ILoggerService<GroupSimilarArticlesService> loggerService,
            TimeSpan maxAgeOfSimilarArticleChecking,
            int groupSimilarArticlesBatchSize,
            int minimalNumberOfWordsForArticleToBeComparable
        )
        {
            _similarityScoreThreshold = similarityScoreThreshold;
            _articlePublishDateTimeDiferenceThreshold = articlePublishDateTimeDiferenceThreshold;
            _loggerService = loggerService;
            _maxAgeOfSimilarArticleChecking = maxAgeOfSimilarArticleChecking;
            _groupSimilarArticlesBatchSize = groupSimilarArticlesBatchSize;
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
                        LanguageUtility.SeparateWords(article.Title).Count() >= _minimalNumberOfWordsForArticleToBeComparable
                )
                .OrderBy(article => article.PublishDateTime)
                .Take(_groupSimilarArticlesBatchSize);

            var notMatchedArticles = orderedArticles.ToList();
            var similarArticles = new List<SimilarArticle>();

            var totalCount = orderedArticles.Count();
            var count = 0;

            foreach (var article in orderedArticles)
            {
                var articlesSimilarArticles = GetSimilarArticlesForArticle(
                    mainArticle: article,
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
                        ("Current Batch", count++),
                    }
                );
            }
            return similarArticles;
        }

        private IEnumerable<SimilarArticle> GetSimilarArticlesForArticle(
            Article mainArticle,
            IEnumerable<Article> notMatchedArticles,
            DateTime lastSimilarityGroupingTime
        )
        {
            var similarArticles = new List<SimilarArticle>();
            var possibleSimilarArticles = notMatchedArticles
                .Where(
                    notMatchedArticle =>
                        mainArticle.Id != notMatchedArticle.Id &&
                        mainArticle.NewsPortalId != notMatchedArticle.NewsPortalId &&
                        (
                            mainArticle.PublishDateTime > notMatchedArticle.PublishDateTime ?
                                (mainArticle.PublishDateTime - notMatchedArticle.PublishDateTime < _articlePublishDateTimeDiferenceThreshold) :
                                (notMatchedArticle.PublishDateTime - mainArticle.PublishDateTime < _articlePublishDateTimeDiferenceThreshold)
                        ) &&
                        (mainArticle.CreateDateTime > lastSimilarityGroupingTime || notMatchedArticle.CreateDateTime > lastSimilarityGroupingTime)
                );

            foreach (var possibleSimilarArticle in possibleSimilarArticles)
            {
                var similarityScore = CalculateSimilarityScore(
                    mainArticle: mainArticle,
                    possibleSubordinateArticle: possibleSimilarArticle
                );

                if (similarityScore > _similarityScoreThreshold)
                {
                    var similarArticle = new SimilarArticle(
                        id: Guid.NewGuid(),
                        similarityScore: similarityScore,
                        mainArticleId: mainArticle.Id,
                        mainArticle: mainArticle,
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
                            ("Main Article Title", mainArticle.Title),
                            ("Subordinate Article Title", possibleSimilarArticle.Title),
                            ("Main Article Source", mainArticle.NewsPortal!.Name),
                            ("Subordinate Article Source", possibleSimilarArticle.NewsPortal!.Name),
                            ("Main Article Category", mainArticle.ArticleCategories.FirstOrDefault()?.Category?.Name!),
                            ("Subordinate Article Category", possibleSimilarArticle.ArticleCategories.FirstOrDefault()?.Category?.Name!),
                        }
                    );
                }
            }

            return similarArticles;
        }

        private static double CalculateSimilarityScore(
            Article mainArticle,
            Article possibleSubordinateArticle
        )
        {
            var mainArticleWords = LanguageUtility
                .SeparateWords(mainArticle.Title)
                .RemoveUnimpactfulCroatianWords();

            var possibleSubordinateArticleWords = LanguageUtility
                .SeparateWords(possibleSubordinateArticle.Title)
                .RemoveUnimpactfulCroatianWords();


            var similarityScore = CalculateSimilarityScoreBetweenWords(
                mainArticleWords,
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