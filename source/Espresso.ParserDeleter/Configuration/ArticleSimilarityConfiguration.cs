using System;
using Microsoft.Extensions.Configuration;

namespace Espresso.ParserDeleter.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class ArticleSimilarityConfiguration
    {
        #region Fields
        private readonly IConfigurationSection _configuration;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double SimilarityScoreThreshold => _configuration.GetValue<double>("SimilarityScoreThreshold");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TimeSpan ArticlePublishDateTimeDiferenceThreshold =>
            TimeSpan.FromHours(
                value: _configuration.GetValue<int>("ArticlePublishDateTimeDiferenceThresholdInHours")
            );

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TimeSpan MaxAgeOfSimilarArticleChecking =>
            TimeSpan.FromHours(
                value: _configuration.GetValue<int>("MaxAgeOfSimilarArticleCheckingInHours")
            );

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GroupSimilarArticlesBatchSize => _configuration.GetValue<int>("GroupSimilarArticlesBatchSize");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int MinimalNumberOfWordsForArticleToBeComparable => _configuration.GetValue<int>("MinimalNumberOfWordsForArticleToBeComparable");
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public ArticleSimilarityConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }
        #endregion
    }
}
