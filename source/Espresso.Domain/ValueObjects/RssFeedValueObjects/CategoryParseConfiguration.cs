using System.Collections.Generic;

using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.Infrastructure;

namespace Espresso.Domain.ValueObjects.RssFeedValueObjects
{
    public class CategoryParseConfiguration : ValueObject
    {
        #region Properties
        public CategoryParseStrategy CategoryParseStrategy { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// ORM Constructor
        /// </summary>
        private CategoryParseConfiguration()
        {
        }

        public CategoryParseConfiguration(CategoryParseStrategy categoryParseStrategy)
        {
            CategoryParseStrategy = categoryParseStrategy;
        }
        #endregion

        #region Methods
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return CategoryParseStrategy;
        }
        #endregion
    }
}
