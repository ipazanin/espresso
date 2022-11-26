// SimilarArticleSetting.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Espresso.Domain.Infrastructure;

#pragma warning disable RCS1170 // Use read-only auto-implemented property.

namespace Espresso.Domain.ValueObjects.SettingsValueObjects;

/// <summary>
/// <see cref="SimilarArticle"/> setting.
/// </summary>
public class SimilarArticleSetting : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SimilarArticleSetting"/> class.
    /// </summary>
    /// <param name="similarityScoreThreshold">Similarity Score Threshold.</param>
    /// <param name="articlePublishDateTimeDifferenceThresholdInMiliseconds">Article Publish DateTime Difference Threshold.</param>
    /// <param name="maxAgeOfSimilarArticleCheckingInMiliseconds">Max Age Of Similar Article Checking.</param>
    /// <param name="minimalNumberOfWordsForArticleToBeComparable">Minimal Number Of Words For Article To Be Comparable.</param>
    public SimilarArticleSetting(
        double similarityScoreThreshold,
        long articlePublishDateTimeDifferenceThresholdInMiliseconds,
        long maxAgeOfSimilarArticleCheckingInMiliseconds,
        int minimalNumberOfWordsForArticleToBeComparable)
    {
        SimilarityScoreThreshold = similarityScoreThreshold;
        ArticlePublishDateTimeDifferenceThresholdInMiliseconds = articlePublishDateTimeDifferenceThresholdInMiliseconds;
        MaxAgeOfSimilarArticleCheckingInMiliseconds = maxAgeOfSimilarArticleCheckingInMiliseconds;
        MinimalNumberOfWordsForArticleToBeComparable = minimalNumberOfWordsForArticleToBeComparable;
    }

    /// <summary>
    /// Gets similarity Score Threshold.
    /// </summary>
    public double SimilarityScoreThreshold { get; private set; }

    /// <summary>
    /// Gets article Publish DateTime Difference Threshold.
    /// </summary>
    public long ArticlePublishDateTimeDifferenceThresholdInMiliseconds { get; private set; }

    /// <summary>
    /// Gets max Age Of Similar Article Checking.
    /// </summary>
    public long MaxAgeOfSimilarArticleCheckingInMiliseconds { get; private set; }

    /// <summary>
    /// Gets minimal Number Of Words For Article To Be Comparable.
    /// </summary>
    public int MinimalNumberOfWordsForArticleToBeComparable { get; private set; }

    /// <summary>
    /// Gets article Publish DateTime Difference Threshold.
    /// </summary>
    public TimeSpan ArticlePublishDateTimeDifferenceThreshold => TimeSpan.FromMilliseconds(ArticlePublishDateTimeDifferenceThresholdInMiliseconds);

    /// <summary>
    /// Gets minimal Number Of Words For Article To Be Comparable.
    /// </summary>
    public TimeSpan MaxAgeOfSimilarArticleChecking => TimeSpan.FromMilliseconds(MaxAgeOfSimilarArticleCheckingInMiliseconds);

    /// <inheritdoc/>
    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return SimilarityScoreThreshold;
        yield return ArticlePublishDateTimeDifferenceThresholdInMiliseconds;
        yield return MaxAgeOfSimilarArticleCheckingInMiliseconds;
        yield return MinimalNumberOfWordsForArticleToBeComparable;
    }
}
