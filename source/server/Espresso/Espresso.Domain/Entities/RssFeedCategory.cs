// RssFeedCategory.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Infrastructure;

namespace Espresso.Domain.Entities
{
    public class RssFeedCategory : IEntity<int, RssFeedCategory>
    {
        public const int UrlRegexMaxLength = 100;

        public int Id { get; private set; }

        public string UrlRegex { get; private set; }

        public int UrlSegmentIndex { get; private set; }

        public int CategoryId { get; private set; }

        public Category? Category { get; private set; }

        public int RssFeedId { get; private set; }

        public RssFeed? RssFeed { get; private set; }

        /// <summary>
        /// ORM Constructor.
        /// </summary>
        public RssFeedCategory()
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
