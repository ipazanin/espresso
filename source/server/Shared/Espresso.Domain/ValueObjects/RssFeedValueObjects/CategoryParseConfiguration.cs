// CategoryParseConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.Infrastructure;

namespace Espresso.Domain.ValueObjects.RssFeedValueObjects
{
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
        /// Gets <see cref="Enums.RssFeedEnums.CategoryParseStrategy"/>.
        /// </summary>
        public CategoryParseStrategy CategoryParseStrategy { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryParseConfiguration"/> class.
        /// ORM Constructor.
        /// </summary>
#pragma warning disable SA1201 // Elements should appear in the correct order
        private CategoryParseConfiguration()
#pragma warning restore SA1201 // Elements should appear in the correct order
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryParseConfiguration"/> class.
        /// </summary>
        /// <param name="categoryParseStrategy">Category parse strategy.</param>
        public CategoryParseConfiguration(CategoryParseStrategy categoryParseStrategy)
        {
            CategoryParseStrategy = categoryParseStrategy;
        }

        /// <inheritdoc/>
        protected override IEnumerable<object?> GetAtomicValues()
        {
            yield return CategoryParseStrategy;
        }
    }
}
