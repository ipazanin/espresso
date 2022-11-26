// GetNewsPortalsRegion.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.WebApi.Application.NewsPortals.Queries.GetNewsPortals;

public sealed class GetNewsPortalsRegion
{
    private GetNewsPortalsRegion()
    {
    }

    public int Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Subtitle { get; private set; } = string.Empty;

    public static Expression<Func<Region, GetNewsPortalsRegion>> GetProjection()
    {
        return region => new GetNewsPortalsRegion
        {
            Id = region.Id,
            Name = region.Name,
            Subtitle = region.Subtitle,
        };
    }
}
