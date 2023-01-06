// ArticleSettingDto.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Domain.ValueObjects.SettingsValueObjects;

namespace Espresso.Application.DataTransferObjects.SettingDataTransferObjects;

public class ArticleSettingDto
{
    public ArticleSettingDto(
        double maxAgeOfTrendingArticleInHours,
        double maxAgeOfFeaturedArticleInHours,
        double maxAgeOfArticleInDays,
        int featuredArticlesTake)
    {
        MaxAgeOfTrendingArticleInHours = maxAgeOfTrendingArticleInHours;
        MaxAgeOfFeaturedArticleInHours = maxAgeOfFeaturedArticleInHours;
        MaxAgeOfArticleInDays = maxAgeOfArticleInDays;
        FeaturedArticlesTake = featuredArticlesTake;
    }

    public static Expression<Func<ArticleSetting, ArticleSettingDto>> Projection
    {
        get => articleSetting => new ArticleSettingDto(
            articleSetting.MaxAgeOfTrendingArticle.TotalHours,
            articleSetting.MaxAgeOfFeaturedArticle.TotalHours,
            articleSetting.MaxAgeOfArticle.TotalDays,
            articleSetting.FeaturedArticlesTake);
    }

    public double MaxAgeOfTrendingArticleInHours { get; set; }

    public double MaxAgeOfFeaturedArticleInHours { get; set; }

    public double MaxAgeOfArticleInDays { get; set; }

    public int FeaturedArticlesTake { get; set; }

    public ArticleSetting CreateArticleSetting()
    {
        return new ArticleSetting(
            maxAgeOfTrendingArticleInMiliseconds: (long)TimeSpan.FromHours(MaxAgeOfTrendingArticleInHours).TotalMilliseconds,
            maxAgeOfFeaturedArticleInMiliseconds: (long)TimeSpan.FromHours(MaxAgeOfFeaturedArticleInHours).TotalMilliseconds,
            maxAgeOfArticleInMiliseconds: (long)TimeSpan.FromDays(MaxAgeOfArticleInDays).TotalMilliseconds,
            featuredArticlesTake: FeaturedArticlesTake);
    }
}
