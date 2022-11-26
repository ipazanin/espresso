// GetNewsPortalsCategory.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.Dashboard.Application.NewsPortals.GetNewsPortals;

public sealed class GetNewsPortalsCategory
{
    private GetNewsPortalsCategory()
    {
        Name = null!;
        Color = null!;
    }

    public int Id { get; private set; }

    public string Name { get; private set; }

    public string Color { get; private set; }

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
