// CategoryParseConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.Infrastructure;

#pragma warning disable RCS1170

namespace Espresso.Domain.ValueObjects.RssFeedValueObjects;

/// <summary>
/// <see cref="RssFeed"/> category parse configuration.
/// </summary>
public class CategoryParseConfiguration : ValueObject
{
    /// <summary>
    /// Default <see cref="CategoryParseStrategy"/> value.
    /// </summary>
    public const CategoryParseStrategy CategoryParseStrategyDefaultValue = CategoryParseStrategy.FromRssFeed;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryParseConfiguration"/> class.
    /// </summary>
    /// <param name="categoryParseStrategy">Category parse strategy.</param>
    public CategoryParseConfiguration(CategoryParseStrategy categoryParseStrategy)
    {
        CategoryParseStrategy = categoryParseStrategy;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryParseConfiguration"/> class.
    /// ORM Constructor.
    /// </summary>
    private CategoryParseConfiguration()
    {
    }

    /// <summary>
    /// Gets <see cref="Enums.RssFeedEnums.CategoryParseStrategy"/>.
    /// </summary>
    public CategoryParseStrategy CategoryParseStrategy { get; private set; }

    /// <inheritdoc/>
    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return CategoryParseStrategy;
    }
}
