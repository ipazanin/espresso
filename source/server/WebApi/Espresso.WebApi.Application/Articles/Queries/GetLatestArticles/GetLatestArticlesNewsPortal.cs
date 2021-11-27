// GetLatestArticlesNewsPortal.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using System.Linq.Expressions;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles;

public record GetLatestArticlesNewsPortal
{
    /// <summary>
    /// Gets news Portal ID.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Gets news Portal Name.
    /// </summary>
    public string Name { get; private set; } = string.Empty;

    public string IconUrl { get; private set; } = string.Empty;

    private GetLatestArticlesNewsPortal()
    {
    }

    public static Expression<Func<NewsPortal, GetLatestArticlesNewsPortal>> GetProjection()
    {
        return newsPortal => new GetLatestArticlesNewsPortal
        {
            Id = newsPortal.Id,
            Name = newsPortal.Name,
            IconUrl = newsPortal.IconUrl,
        };
    }
}
