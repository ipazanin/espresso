// GetGroupedCategoryArticlesArticle.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Domain.Entities;

namespace Espresso.WebApi.Application.Articles.Queries.GetGroupedCategoryArticles;

public record GetGroupedCategoryArticlesArticle
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
    public GetGroupedCategoryArticlesNewsPortal? NewsPortal { get; private set; }

    /// <summary>
    /// Gets list Of Categories article belongs to.
    /// </summary>
    public IEnumerable<GetGroupedCategoryArticlesCategory> Categories { get; private set; } = new List<GetGroupedCategoryArticlesCategory>();

    private GetGroupedCategoryArticlesArticle()
    {
    }

    public static Expression<Func<Article, GetGroupedCategoryArticlesArticle>> GetProjection()
    {
        return article => new GetGroupedCategoryArticlesArticle
        {
            Id = article.Id,
            Title = article.Title,
            ImageUrl = article.ImageUrl,
            Url = article.Url,
            PublishDateTime = article.PublishDateTime.ToString(DateTimeConstants.MobileAppDateTimeFormat, CultureInfo.InvariantCulture),
            NumberOfClicks = article.NumberOfClicks,
            NewsPortal = GetGroupedCategoryArticlesNewsPortal.GetProjection()
                .Compile()
                .Invoke(article.NewsPortal!),
            Categories = article.ArticleCategories
                .AsQueryable()
                .Select(articleCategory => articleCategory.Category)
                .Select(GetGroupedCategoryArticlesCategory.GetProjection()!),
        };
    }
}
