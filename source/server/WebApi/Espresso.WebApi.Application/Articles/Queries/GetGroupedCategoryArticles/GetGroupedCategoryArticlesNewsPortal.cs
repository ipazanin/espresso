// GetGroupedCategoryArticlesNewsPortal.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.WebApi.Application.Articles.Queries.GetGroupedCategoryArticles;

public record GetGroupedCategoryArticlesNewsPortal
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

    private GetGroupedCategoryArticlesNewsPortal()
    {
    }

    public static Expression<Func<NewsPortal, GetGroupedCategoryArticlesNewsPortal>> GetProjection()
    {
        return newsPortal => new GetGroupedCategoryArticlesNewsPortal
        {
            Id = newsPortal.Id,
            Name = newsPortal.Name,
            IconUrl = newsPortal.IconUrl,
        };
    }
}
