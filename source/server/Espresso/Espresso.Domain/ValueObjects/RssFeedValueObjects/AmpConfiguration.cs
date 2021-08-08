// AmpConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Collections.Generic;
using Espresso.Domain.Infrastructure;

namespace Espresso.Domain.ValueObjects.RssFeedValueObjects
{
    public class AmpConfiguration : ValueObject
    {
        public bool HasAmpArticles { get; private set; }

        /// <summary>
        /// Gets {0} = ArticleId
        /// {1} = Third article segment
        /// {2} = Second Article Segment
        /// {1} = First Article Segment.
        /// </summary>
        public string? TemplateUrl { get; private set; }

        /// <summary>
        /// ORM Constructor.
        /// </summary>
        private AmpConfiguration()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AmpConfiguration"/> class.
        /// </summary>
        /// <param name="hasAmpArticles"></param>
        /// <param name="templateUrl"></param>
        public AmpConfiguration(bool hasAmpArticles, string templateUrl)
        {
            HasAmpArticles = hasAmpArticles;
            TemplateUrl = templateUrl;
        }

        protected override IEnumerable<object?> GetAtomicValues()
        {
            yield return HasAmpArticles;
            yield return TemplateUrl;
        }
    }
}
