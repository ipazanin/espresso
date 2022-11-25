// GetArticlesArticle.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.Dashboard.Application.Articles.Queries.GetArticles;

public class GetArticlesArticle
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
            Created = article.CreateDateTime,
        };
    }

    public Guid Id { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public DateTimeOffset Created { get; private set; }
}
