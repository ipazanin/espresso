// RssFeedCategoryItem.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Espresso.Dashboard.Application.NewsPortals.GetNewsPortalDetails
{
    /// <summary>
    /// <see cref="NewsPortal"/> details <see cref="RssFeedCategory"/> item.
    /// </summary>
    public class RssFeedCategoryItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RssFeedCategoryItem"/> class.
        /// </summary>
        private RssFeedCategoryItem()
        {
            UrlRegex = null!;
            Category = null!;
        }

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

        /// <summary>
        /// Gets <see cref="RssFeedCategory"/> url regex.
        /// </summary>
        public string Category { get; private set; }

        /// <summary>
        /// Returns <see cref="RssFeedCategory"/> to <see cref="RssFeedCategoryItem"/> projection.
        /// </summary>
        /// <returns><see cref="RssFeedCategory"/> to <see cref="RssFeedCategoryItem"/> projection.</returns>
        public static Expression<Func<RssFeedCategory, RssFeedCategoryItem>> GetProjection()
        {
            return rssFeedCategory => new RssFeedCategoryItem
            {
                Id = rssFeedCategory.Id,
                UrlRegex = rssFeedCategory.UrlRegex,
                UrlSegmentIndex = rssFeedCategory.UrlSegmentIndex,
                Category = rssFeedCategory.Category!.Name,
            };
        }
    }
}
