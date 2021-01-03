using System.Collections.Generic;

using Espresso.Domain.Infrastructure;

namespace Espresso.Domain.ValueObjects.RssFeedValueObjects
{
    public class AmpConfiguration : ValueObject
    {
        #region Properties
        public bool HasAmpArticles { get; private set; }

        /// <summary>
        /// {0} = ArticleId
        /// {1} = Third article segment
        /// {2} = Second Article Segment
        /// {1} = First Article Segment
        /// </summary>
        public string? TemplateUrl { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// ORM Constructor
        /// </summary>
        private AmpConfiguration()
        {
        }

        public AmpConfiguration(bool hasAmpArticles, string templateUrl)
        {
            HasAmpArticles = hasAmpArticles;
            TemplateUrl = templateUrl;
        }
        #endregion

        #region Methods
        protected override IEnumerable<object?> GetAtomicValues()
        {
            yield return HasAmpArticles;
            yield return TemplateUrl;
        }
        #endregion
    }
}
