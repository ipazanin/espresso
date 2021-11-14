// GetNewsPortalsRegion.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Espresso.Application.NewsPortals
{
    public class GetNewsPortalsRegion
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public string Subtitle { get; private set; } = string.Empty;

#pragma warning disable SA1201 // Elements should appear in the correct order
        private GetNewsPortalsRegion()
#pragma warning restore SA1201 // Elements should appear in the correct order
        {
        }

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
}
