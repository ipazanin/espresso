// SimilarArticlesSettingDto.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Domain.ValueObjects.SettingsValueObjects;

namespace Espresso.Application.DataTransferObjects.SettingDataTransferObjects;

public class SimilarArticlesSettingDto
{
    public SimilarArticlesSettingDto(
        double maxDurationBetweenTwoSimilarArticlesInHours,
        double maxArticleAgeToParseInSimilarArticlesInHours,
        int minimalNumberOfWordsRequirement,
        double similarityScoreThreshold)
    {
        MaxDurationBetweenTwoSimilarArticlesInHours = maxDurationBetweenTwoSimilarArticlesInHours;
        MaxArticleAgeToParseInSimilarArticlesInHours = maxArticleAgeToParseInSimilarArticlesInHours;
        MinimalNumberOfWordsRequirement = minimalNumberOfWordsRequirement;
        SimilarityScoreThreshold = similarityScoreThreshold;
    }

    public static Expression<Func<SimilarArticleSetting, SimilarArticlesSettingDto>> Projection
    {
        get => similarArticleSetting => new SimilarArticlesSettingDto(
            similarArticleSetting.ArticlePublishDateTimeDifferenceThreshold.TotalHours,
            similarArticleSetting.MaxAgeOfSimilarArticleChecking.TotalHours,
            similarArticleSetting.MinimalNumberOfWordsForArticleToBeComparable,
            similarArticleSetting.SimilarityScoreThreshold);
    }

    public double MaxDurationBetweenTwoSimilarArticlesInHours { get; set; }

    public double MaxArticleAgeToParseInSimilarArticlesInHours { get; set; }

    public int MinimalNumberOfWordsRequirement { get; set; }

    public double SimilarityScoreThreshold { get; set; }

    public SimilarArticleSetting CreateSimilarArticleSetting()
    {
        return new SimilarArticleSetting(
            SimilarityScoreThreshold,
            (long)TimeSpan.FromHours(MaxDurationBetweenTwoSimilarArticlesInHours).TotalMilliseconds,
            (long)TimeSpan.FromHours(MaxArticleAgeToParseInSimilarArticlesInHours).TotalMilliseconds,
            MinimalNumberOfWordsRequirement);
    }
}
