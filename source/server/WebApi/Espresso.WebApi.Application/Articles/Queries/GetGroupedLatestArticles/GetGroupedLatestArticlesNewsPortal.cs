// GetGroupedLatestArticlesNewsPortal.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Entities;

namespace Espresso.WebApi.Application.Articles.Queries.GetGroupedLatestArticles;

public record GetGroupedLatestArticlesNewsPortal
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

    private GetGroupedLatestArticlesNewsPortal()
    {
    }

    public static Expression<Func<NewsPortal, GetGroupedLatestArticlesNewsPortal>> GetProjection()
    {
        return newsPortal => new GetGroupedLatestArticlesNewsPortal
        {
            Id = newsPortal.Id,
            Name = newsPortal.Name,
            IconUrl = newsPortal.IconUrl,
        };
    }
}
