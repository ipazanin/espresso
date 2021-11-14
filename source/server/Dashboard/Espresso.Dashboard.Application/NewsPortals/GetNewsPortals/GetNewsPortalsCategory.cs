// GetNewsPortalsCategory.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Espresso.Dashboard.Application.NewsPortals.GetNewsPortals
{
    public class GetNewsPortalsCategory
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Color { get; private set; }

#pragma warning disable SA1201 // Elements should appear in the correct order
        private GetNewsPortalsCategory()
#pragma warning restore SA1201 // Elements should appear in the correct order
        {
            Name = null!;
            Color = null!;
        }

        public static Expression<Func<Category, GetNewsPortalsCategory>> GetProjection()
        {
            return category => new GetNewsPortalsCategory
            {
                Id = category.Id,
                Name = category.Name,
                Color = category.Color,
            };
        }
    }
}
