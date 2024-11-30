// GetArticlesArticle.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.Dashboard.Application.Articles.Queries.GetArticles;

public sealed class GetArticlesArticle
{
    private GetArticlesArticle()
    {
    }

    public static Expression<Func<Article, GetArticlesArticle>> Projection
    {
        get => article => new GetArticlesArticle
        {
            Id = article.Id,
            Title = article.Title,
            NewsPortal = article.NewsPortal!.Name,
            NewsPortalId = article.NewsPortalId,
            Categories = article.ArticleCategories.Select(articleCategory => new ValueTuple<string, string>(articleCategory.Category!.Name, articleCategory.Category!.Color)),
            ArticleUrl = article.Url,
            ImageUrl = article.ImageUrl ?? string.Empty,
            Created = article.CreateDateTime,
        };
    }

    public Guid Id { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public string NewsPortal { get; private set; } = string.Empty;

    public int NewsPortalId { get; private set; }

    public IEnumerable<(string categoryName, string categoryColor)> Categories { get; private set; } = [];

    public string ArticleUrl { get; private set; } = string.Empty;

    public string ImageUrl { get; private set; } = string.Empty;

    public DateTimeOffset Created { get; private set; }
}
