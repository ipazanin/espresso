// GetConfigurationCategory_1_3.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using System.Linq.Expressions;

namespace Espresso.WebApi.Application.Configuration.Queries.GetConfiguration_1_3;
#pragma warning disable S101 // Types should be named in PascalCase
public record GetConfigurationCategory_1_3
#pragma warning restore S101 // Types should be named in PascalCase
{
    /// <summary>
    /// Gets category ID.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Gets category Name.
    /// </summary>
    public string Name { get; private set; } = string.Empty;

    public string Color { get; private set; } = string.Empty;

    private GetConfigurationCategory_1_3()
    {
    }

    public static Expression<Func<Category, GetConfigurationCategory_1_3>> GetProjection()
    {
        return category => new GetConfigurationCategory_1_3
        {
            Id = category.Id,
            Name = category.Name,
            Color = category.Color,
        };
    }
}
