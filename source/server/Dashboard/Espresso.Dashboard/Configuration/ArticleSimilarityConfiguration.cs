// ArticleSimilarityConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.Extensions.Configuration;
using System;

namespace Espresso.Dashboard.Configuration
{
    /// <summary>
    /// Article Similarity Configuration.
    /// </summary>
    public class ArticleSimilarityConfiguration
    {
        private readonly IConfigurationSection _configuration;

        /// <summary>
        /// Gets similarity Score Threshold.
        /// </summary>
        public double SimilarityScoreThreshold => _configuration.GetValue<double>("SimilarityScoreThreshold");

        /// <summary>
        /// Gets article Publish DateTime Difference Threshold.
        /// </summary>
        public TimeSpan ArticlePublishDateTimeDifferenceThreshold =>
            TimeSpan.FromHours(
                value: _configuration.GetValue<int>("ArticlePublishDateTimeDiferenceThresholdInHours")
            );

        /// <summary>
        /// Gets max Age Of Similar Article Checking.
        /// </summary>
        /// <returns></returns>
        public TimeSpan MaxAgeOfSimilarArticleChecking =>
            TimeSpan.FromHours(
                value: _configuration.GetValue<int>("MaxAgeOfSimilarArticleCheckingInHours")
            );

        /// <summary>
        /// Gets minimal Number Of Words For Article To Be Comparable.
        /// </summary>
        /// <returns></returns>
        public int MinimalNumberOfWordsForArticleToBeComparable => _configuration.GetValue<int>("MinimalNumberOfWordsForArticleToBeComparable");

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleSimilarityConfiguration"/> class.
        /// ArticleSimilarityConfiguration Constructor.
        /// </summary>
        /// <param name="configuration"></param>
        public ArticleSimilarityConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }
    }
}
