// GetLatestArticlesArticle_2_0.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using System.Linq.Expressions;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_2_0;
#pragma warning disable S101 // Types should be named in PascalCase
public record GetLatestArticlesArticle_2_0
#pragma warning restore S101 // Types should be named in PascalCase
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
    /// Gets article Url provided by RSS Feed.
    /// </summary>
    public string WebUrl { get; private set; } = string.Empty;

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
    public GetLatestArticlesNewsPortal_2_0? NewsPortal { get; private set; }

    /// <summary>
    /// Gets list Of Categories article belongs to.
    /// </summary>
    public IEnumerable<GetLatestArticlesCategory_2_0> Categories { get; private set; } = new List<GetLatestArticlesCategory_2_0>();

    private GetLatestArticlesArticle_2_0()
    {
    }

    public static Expression<Func<Article, GetLatestArticlesArticle_2_0>> GetProjection()
    {
        return article => new GetLatestArticlesArticle_2_0
        {
            Id = article.Id,
            Title = article.Title,
            ImageUrl = article.ImageUrl,
            Url = article.Url,
            WebUrl = article.WebUrl,
            PublishDateTime = article.PublishDateTime.ToString(DateTimeConstants.MobileAppDateTimeFormat),
            NumberOfClicks = article.NumberOfClicks,
            NewsPortal = GetLatestArticlesNewsPortal_2_0.GetProjection()
                .Compile()
                .Invoke(article.NewsPortal!),
            Categories = article.ArticleCategories
                .AsQueryable()
                .Select(articleCategory => articleCategory.Category)
                .Select(GetLatestArticlesCategory_2_0.GetProjection()!),
        };
    }
}
