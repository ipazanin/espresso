// CategoryParseConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Collections.Generic;
using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.Infrastructure;

namespace Espresso.Domain.ValueObjects.RssFeedValueObjects
{
    public class CategoryParseConfiguration : ValueObject
    {
        public const CategoryParseStrategy CategoryParseStrategyDefaultValue = CategoryParseStrategy.FromRssFeed;

        public CategoryParseStrategy CategoryParseStrategy { get; private set; }

        /// <summary>
        /// ORM Constructor.
        /// </summary>
        private CategoryParseConfiguration()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryParseConfiguration"/> class.
        /// </summary>
        /// <param name="categoryParseStrategy"></param>
        public CategoryParseConfiguration(CategoryParseStrategy categoryParseStrategy)
        {
            CategoryParseStrategy = categoryParseStrategy;
        }

        protected override IEnumerable<object?> GetAtomicValues()
        {
            yield return CategoryParseStrategy;
        }
    }
}
