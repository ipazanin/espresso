using System.Collections.Generic;

using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.Infrastructure;

namespace Espresso.Domain.ValueObjects.RssFeedValueObjects
{
    public class CategoryParseConfiguration : ValueObject
    {
        #region Properties
        public CategoryParseStrategy CategoryParseStrategy { get; private set; }

        public int? UrlSegmentIndex { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// ORM Constructor
        /// </summary>
        private CategoryParseConfiguration()
        {
        }

        public CategoryParseConfiguration(CategoryParseStrategy categoryParseStrategy, int? urlSegmentIndex)
        {
            CategoryParseStrategy = categoryParseStrategy;
            UrlSegmentIndex = urlSegmentIndex;
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
