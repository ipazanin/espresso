// ArticleSettings.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using System;

namespace Espresso.Domain.ValueObjects.AppSettingsValueObjects
{
    /// <summary>
    /// <see cref="Article"/> settings.
    /// </summary>
    public class ArticleSettings
    {
        /// <summary>
        /// Gets max age of <see cref="Article"/> that can be considered trending in miliseconds.
        /// </summary>
        public long MaxAgeOfTrendingArticleInMiliseconds { get; private set; }

        /// <summary>
        /// Gets max age of <see cref="Article"/> that can be considered featured in miliseconds.
        /// </summary>
        public long MaxAgeOfFeaturedArticleInMiliseconds { get; private set; }

        /// <summary>
        /// Gets max age of <see cref="Article"/> that can be saved in application.
        /// </summary>
        public TimeSpan MaxAgeOfArticle { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleSettings"/> class.
        /// Ef Core constructor.
        /// </summary>
        private ArticleSettings()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleSettings"/> class.
        /// </summary>
        /// <param name="maxAgeOfTrendingArticleInMiliseconds">Gets max age of <see cref="Article"/> that can be considered trending in miliseconds</param>
        /// <param name="maxAgeOfFeaturedArticleInMiliseconds">Gets max age of <see cref="Article"/> that can be considered featured in miliseconds.</param>
        /// <param name="maxAgeOfArticle">Gets max age of <see cref="Article"/> that can be saved in application.</param>
        public ArticleSettings(
            long maxAgeOfTrendingArticleInMiliseconds,
            long maxAgeOfFeaturedArticleInMiliseconds,
            TimeSpan maxAgeOfArticle
        )
        {
            MaxAgeOfTrendingArticleInMiliseconds = maxAgeOfTrendingArticleInMiliseconds;
            MaxAgeOfFeaturedArticleInMiliseconds = maxAgeOfFeaturedArticleInMiliseconds;
            MaxAgeOfArticle = maxAgeOfArticle;
        }
    }
}
