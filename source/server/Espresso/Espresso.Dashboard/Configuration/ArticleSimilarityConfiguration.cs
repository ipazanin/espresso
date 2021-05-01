using System;
using Microsoft.Extensions.Configuration;

namespace Espresso.Dashboard.Configuration
{
    /// <summary>
    /// Article Similarity Configuration
    /// </summary>
    public class ArticleSimilarityConfiguration
    {
        #region Fields
        private readonly IConfigurationSection _configuration;
        #endregion

        #region Properties
        /// <summary>
        /// Similarity Score Threshold
        /// </summary>
        public double SimilarityScoreThreshold => _configuration.GetValue<double>("SimilarityScoreThreshold");

        /// <summary>
        /// Article Publish DateTime Difference Threshold
        /// </summary>
        public TimeSpan ArticlePublishDateTimeDifferenceThreshold =>
            TimeSpan.FromHours(
                value: _configuration.GetValue<int>("ArticlePublishDateTimeDiferenceThresholdInHours")
            );

        /// <summary>
        /// Max Age Of Similar Article Checking
        /// </summary>
        /// <returns></returns>
        public TimeSpan MaxAgeOfSimilarArticleChecking =>
            TimeSpan.FromHours(
                value: _configuration.GetValue<int>("MaxAgeOfSimilarArticleCheckingInHours")
            );

        /// <summary>
        /// Minimal Number Of Words For Article To Be Comparable
        /// </summary>
        /// <returns></returns>
        public int MinimalNumberOfWordsForArticleToBeComparable => _configuration.GetValue<int>("MinimalNumberOfWordsForArticleToBeComparable");
        #endregion

        #region Constructors
        /// <summary>
        /// ArticleSimilarityConfiguration Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public ArticleSimilarityConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }
        #endregion
    }
}
