// RssFeedCategory.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Infrastructure;

namespace Espresso.Domain.Entities
{
    /// <summary>
    /// Represents way to convert <see cref="RssFeed"/> articles url to category.
    /// </summary>
    public class RssFeedCategory : IEntity<int, RssFeedCategory>
    {
        public const int UrlRegexMaxLength = 100;

        /// <summary>
        /// Gets <see cref="RssFeedCategory"/> id.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets <see cref="RssFeedCategory"/> url regex.
        /// </summary>
        public string UrlRegex { get; private set; }

        /// <summary>
        /// Gets <see cref="RssFeedCategory"/> url segment index.
        /// </summary>
        public int UrlSegmentIndex { get; private set; }

        public int CategoryId { get; private set; }

        public Category? Category { get; private set; }

        public int RssFeedId { get; private set; }

        public RssFeed? RssFeed { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RssFeedCategory"/> class.
        /// ORM Constructor.
        /// </summary>
#pragma warning disable SA1201 // Elements should appear in the correct order
        public RssFeedCategory()
#pragma warning restore SA1201 // Elements should appear in the correct order
        {
            UrlRegex = null!;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RssFeedCategory"/> class.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="urlRegex"></param>
        /// <param name="urlSegmentIndex"></param>
        /// <param name="categoryId"></param>
        /// <param name="rssFeedId"></param>
        public RssFeedCategory(int id, string urlRegex, int urlSegmentIndex, int categoryId, int rssFeedId)
        {
            Id = id;
            UrlRegex = urlRegex;
            UrlSegmentIndex = urlSegmentIndex;
            RssFeedId = rssFeedId;
            CategoryId = categoryId;
        }
    }
}
