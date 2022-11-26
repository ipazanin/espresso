// GetWebConfigurationCategory.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;

namespace Espresso.WebApi.Application.Configuration.Queries.GetWebConfiguration;

public record GetWebConfigurationCategory
{
    /// <summary>
    /// Gets category ID.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets category Name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    public string Color { get; init; } = string.Empty;

    public int? Position { get; init; }

    public CategoryType CategoryType { get; init; }

    public string Url { get; init; } = string.Empty;

    public static Expression<Func<Category, GetWebConfigurationCategory>> GetProjection()
    {
        return category => new GetWebConfigurationCategory
        {
            Id = category.Id,
            Name = category.Name,
            Color = category.Color,
            Position = category.Position,
            CategoryType = category.CategoryType,
            Url = category.Url,
        };
    }
}
