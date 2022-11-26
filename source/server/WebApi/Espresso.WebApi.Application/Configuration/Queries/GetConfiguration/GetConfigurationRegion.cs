// GetConfigurationRegion.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.WebApi.Application.Configuration.Queries.GetConfiguration;

public record GetConfigurationRegion
{
    public int Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Subtitle { get; private set; } = string.Empty;

    public IEnumerable<GetConfigurationNewsPortal> NewsPortals { get; private set; } = new List<GetConfigurationNewsPortal>();

    private GetConfigurationRegion()
    {
    }

    public static Expression<Func<Region, GetConfigurationRegion>> GetProjection(TimeSpan maxAgeOfNewNewsPortal)
    {
        return region => new GetConfigurationRegion
        {
            Id = region.Id,
            Name = region.Name,
            Subtitle = region.Subtitle,
            NewsPortals = region
                .NewsPortals
                .Where(newsPortal => newsPortal.IsEnabled)
                .Select(
                    GetConfigurationNewsPortal
                        .GetProjection(maxAgeOfNewNewsPortal)
                        .Compile()),
        };
    }
}
