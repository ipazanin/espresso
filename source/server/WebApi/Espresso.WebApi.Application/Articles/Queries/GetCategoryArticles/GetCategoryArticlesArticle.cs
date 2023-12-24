// GetCategoryArticlesArticle.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Domain.Entities;

namespace Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles;

public record GetCategoryArticlesArticle
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
    ///
    /// </summary>
    public int NumberOfClicks { get; private set; }

    /// <summary>
    /// Gets news Portal ID.
    /// </summary>
    public GetCategoryArticlesNewsPortal? NewsPortal { get; private set; }

    /// <summary>
    /// Gets list Of Categories article belongs to.
    /// </summary>
    public IEnumerable<GetCategoryArticlesCategory> Categories { get; private set; } = new List<GetCategoryArticlesCategory>();

    private GetCategoryArticlesArticle()
    {
    }

    public static Expression<Func<Article, GetCategoryArticlesArticle>> GetProjection()
    {
        return article => new GetCategoryArticlesArticle
        {
            Id = article.Id,
            Title = article.Title,
            ImageUrl = article.ImageUrl,
            Url = article.Url,
            PublishDateTime = article.PublishDateTime.ToString(DateTimeConstants.MobileAppDateTimeFormat, CultureInfo.InvariantCulture),
            NumberOfClicks = article.NumberOfClicks,
            NewsPortal = GetCategoryArticlesNewsPortal.GetProjection()
                .Compile()
                .Invoke(article.NewsPortal!),
            Categories = article.ArticleCategories
                .Select(articleCategory => articleCategory.Category)
                .Select(GetCategoryArticlesCategory.GetProjection().Compile()!),
        };
    }
}
