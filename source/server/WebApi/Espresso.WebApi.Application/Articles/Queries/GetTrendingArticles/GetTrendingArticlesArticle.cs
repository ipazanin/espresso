// GetTrendingArticlesArticle.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Domain.Entities;

namespace Espresso.WebApi.Application.Articles.Queries.GetTrendingArticles;

public record GetTrendingArticlesArticle
{
    /// <summary>
    /// Gets iD created by app.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets article Url provided by RSS Feed.
    /// </summary>
    public string Url { get; private set; } = string.Empty;

    /// <summary>
    /// Gets article Title Parsed from RSS Feed.
    /// </summary>
    public string Title { get; private set; } = string.Empty;

    /// <summary>
    /// Gets image URL parsed from src attribute of first img element or second rss feed link, first is.
    /// </summary>
    public string? ImageUrl { get; private set; }

    /// <summary>
    /// Gets article Publish time provided by RSS Feed.
    /// </summary>
    public string PublishDateTime { get; private set; } = string.Empty;

    /// <summary>
    /// Gets trending Score.
    /// </summary>
    public int TrendingScore { get; private set; }

    /// <summary>
    /// Gets news Portal.
    /// </summary>
    public GetTrendingArticlesNewsPortal? NewsPortal { get; private set; }

    /// <summary>
    /// Gets list Of Categories article belongs to.
    /// </summary>
    public IEnumerable<GetTrendingArticlesCategory> Categories { get; private set; } = [];

    private GetTrendingArticlesArticle()
    {
    }

    public static Expression<Func<Article, GetTrendingArticlesArticle>> GetProjection()
    {
        return article => new GetTrendingArticlesArticle
        {
            Id = article.Id,
            Title = article.Title,
            ImageUrl = article.ImageUrl,
            Url = article.Url,
            PublishDateTime = article.PublishDateTime.ToString(DateTimeConstants.MobileAppDateTimeFormat, CultureInfo.InvariantCulture),
            NewsPortal = GetTrendingArticlesNewsPortal.GetProjection()
                .Compile()
                .Invoke(article.NewsPortal!),
            Categories = article.ArticleCategories
                .Select(articleCategory => articleCategory.Category)
                .Select(GetTrendingArticlesCategory.GetProjection().Compile()!),
            TrendingScore = (int)article.TrendingScore,
        };
    }
}
