// ArticleSetting.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Espresso.Domain.Infrastructure;

namespace Espresso.Domain.ValueObjects.SettingsValueObjects
{
    /// <summary>
    /// <see cref="Article"/> settings.
    /// </summary>
    public class ArticleSetting : ValueObject
    {
#pragma warning disable RCS1170 // Use read-only auto-implemented property.

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
        public long MaxAgeOfArticleInMiliseconds { get; private set; }

        /// <summary>
        /// Gets number of featured articles to fetch on request.
        /// </summary>
        public int FeaturedArticlesTake { get; private set; }

#pragma warning restore RCS1170 // Use read-only auto-implemented property.

        /// <summary>
        ///  Gets max age of <see cref="Article"/> that can be saved in application.
        /// </summary>
        public TimeSpan MaxAgeOfArticle => TimeSpan.FromMilliseconds(MaxAgeOfArticleInMiliseconds);

        /// <summary>
        /// Gets max age of <see cref="Article"/> that can be considered trending in miliseconds.
        /// </summary>
        public TimeSpan MaxAgeOfTrendingArticle => TimeSpan.FromMilliseconds(MaxAgeOfTrendingArticleInMiliseconds);

        /// <summary>
        /// Gets max age of <see cref="Article"/> that can be considered featured in miliseconds.
        /// </summary>
        public TimeSpan MaxAgeOfFeaturedArticle => TimeSpan.FromMilliseconds(MaxAgeOfFeaturedArticleInMiliseconds);

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleSetting"/> class.
        /// </summary>
        /// <param name="maxAgeOfTrendingArticleInMiliseconds">Gets max age of <see cref="Article"/> that can be considered trending in miliseconds.</param>
        /// <param name="maxAgeOfFeaturedArticleInMiliseconds">Gets max age of <see cref="Article"/> that can be considered featured in miliseconds.</param>
        /// <param name="maxAgeOfArticleInMiliseconds">Gets max age of <see cref="Article"/> that can be saved in application.</param>
        /// <param name="featuredArticlesTake">Number of featured articles to fetch on request.</param>
#pragma warning disable SA1201 // Elements should appear in the correct order
        public ArticleSetting(
#pragma warning restore SA1201 // Elements should appear in the correct order
            long maxAgeOfTrendingArticleInMiliseconds,
            long maxAgeOfFeaturedArticleInMiliseconds,
            long maxAgeOfArticleInMiliseconds,
            int featuredArticlesTake)
        {
            MaxAgeOfTrendingArticleInMiliseconds = maxAgeOfTrendingArticleInMiliseconds;
            MaxAgeOfFeaturedArticleInMiliseconds = maxAgeOfFeaturedArticleInMiliseconds;
            MaxAgeOfArticleInMiliseconds = maxAgeOfArticleInMiliseconds;
            FeaturedArticlesTake = featuredArticlesTake;
        }

        /// <inheritdoc/>
        protected override IEnumerable<object?> GetAtomicValues()
        {
            yield return MaxAgeOfTrendingArticleInMiliseconds;
            yield return MaxAgeOfFeaturedArticleInMiliseconds;
            yield return MaxAgeOfArticleInMiliseconds;
            yield return FeaturedArticlesTake;
        }
    }
}
