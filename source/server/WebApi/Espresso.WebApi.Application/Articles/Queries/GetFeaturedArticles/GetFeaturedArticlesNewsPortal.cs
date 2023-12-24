// GetFeaturedArticlesNewsPortal.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Entities;

namespace Espresso.WebApi.Application.Articles.Queries.GetFeaturedArticles;

public record GetFeaturedArticlesNewsPortal
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

    private GetFeaturedArticlesNewsPortal()
    {
    }

    public static Expression<Func<NewsPortal, GetFeaturedArticlesNewsPortal>> GetProjection()
    {
        return newsPortal => new GetFeaturedArticlesNewsPortal
        {
            Id = newsPortal.Id,
            Name = newsPortal.Name,
            IconUrl = newsPortal.IconUrl,
        };
    }
}
