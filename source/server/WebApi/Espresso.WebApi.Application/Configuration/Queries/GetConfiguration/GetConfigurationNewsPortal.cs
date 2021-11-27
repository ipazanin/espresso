// GetConfigurationNewsPortal.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using System.Linq.Expressions;

namespace Espresso.WebApi.Application.Configuration.Queries.GetConfiguration;

public record GetConfigurationNewsPortal
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

    public bool IsNew { get; private set; }

    public int CategoryId { get; private set; }

    public int RegionId { get; private set; }

    private GetConfigurationNewsPortal()
    {
    }

    public static Expression<Func<NewsPortal, GetConfigurationNewsPortal>> GetProjection(TimeSpan maxAgeOfNewNewsPortal)
    {
        var newNewsPortalMinDate = DateTimeOffset.UtcNow - maxAgeOfNewNewsPortal;
        return newsPortal => new GetConfigurationNewsPortal
        {
            Id = newsPortal.Id,
            Name = newsPortal.Name,
            IconUrl = newsPortal.IconUrl,
            IsNew = newsPortal.IsNewOverride ?? newsPortal.CreatedAt > newNewsPortalMinDate,
            CategoryId = newsPortal.CategoryId,
            RegionId = newsPortal.RegionId,
        };
    }
}
