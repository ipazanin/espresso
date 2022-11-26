// ArticleSettingState.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.Dashboard.Pages.Settings.EditSettings.State;

public class ArticleSettingState
{
    public int MaxAgeOfTrendingArticleInHours { get; set; }

    public int MaxAgeOfFeaturedArticleInHours { get; set; }

    public int MaxAgeOfArticleInDays { get; set; }

    public int FeaturedArticlesTake { get; set; }
}
