// NewsPortal.cs
//
// © 2021 Espresso News. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Espresso.Domain.Infrastructure;

namespace Espresso.Domain.Entities
{
    /// <summary>
    /// Represents news source, for example www.index.hr.
    /// </summary>
    public class NewsPortal : IEntity<int, NewsPortal>
    {
        public const bool IsEnabledDefaultValue = true;

        public const int NameMaxLength = 100;

        public const int BaseUrlMaxLength = 100;

        public const int IconUrlMaxlength = 100;

#pragma warning disable RCS1170 // Use read-only auto-implemented property.
        public int Id { get; private set; }

        public string Name { get; set; }

        public string BaseUrl { get; private set; }

        public string IconUrl { get; private set; }

        public bool? IsNewOverride { get; private set; }

        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Gets a value indicating whether allows news portal to be saved in database but not used in application.
        /// </summary>
        public bool IsEnabled { get; private set; }

        public int RegionId { get; private set; }

        public Region? Region { get; private set; }

        public int CategoryId { get; private set; }

        public Category? Category { get; private set; }

        public ICollection<RssFeed> RssFeeds { get; private set; } = new List<RssFeed>();

        public ICollection<Article> Articles { get; private set; } = new List<Article>();
#pragma warning restore RCS1170 // Use read-only auto-implemented property.

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsPortal"/> class.
        /// </summary>
        /// <remarks>
        /// ORM Constructor.
        /// </remarks>
        private NewsPortal()
        {
            Name = null!;
            BaseUrl = null!;
            IconUrl = null!;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsPortal"/> class.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="baseUrl"></param>
        /// <param name="iconUrl"></param>
        /// <param name="isNewOverride"></param>
        /// <param name="createdAt"></param>
        /// <param name="categoryId"></param>
        /// <param name="regionId"></param>
        /// <param name="isEnabled"></param>
        public NewsPortal(
            int id,
            string name,
            string baseUrl,
            string iconUrl,
            bool? isNewOverride,
            DateTime createdAt,
            int categoryId,
            int regionId,
            bool isEnabled
        )
        {
            Id = id;
            Name = name;
            BaseUrl = baseUrl;
            IconUrl = iconUrl;
            IsNewOverride = isNewOverride;
            CreatedAt = createdAt;
            CategoryId = categoryId;
            RegionId = regionId;
            IsEnabled = isEnabled;
        }

        public static Expression<Func<NewsPortal, bool>> GetCategorySugestedNewsPortalsPredicate(
            IEnumerable<int>? newsPortalIds,
            int categoryId,
            int? regionId,
            TimeSpan maxAgeOfNewNewsPortal
        )
        {
            var newNewsPortalMinDate = DateTime.UtcNow - maxAgeOfNewNewsPortal;
            return newsPortal =>
                newsPortalIds != null && !newsPortalIds.Contains(newsPortal.Id) &&
                categoryId.Equals(newsPortal.CategoryId) &&
                (regionId == null || newsPortal.RegionId == regionId) &&
                (
                    newsPortal.IsNewOverride != null ?
                    newsPortal.IsNewOverride.Value :
                    newsPortal.CreatedAt > newNewsPortalMinDate
                );
        }

        public static Expression<Func<NewsPortal, bool>> GetLatestSugestedNewsPortalsPredicate(
            IEnumerable<int>? newsPortalIds,
            IEnumerable<int>? categoryIds,
            TimeSpan maxAgeOfNewNewsPortal
        )
        {
            var newNewsPortalMinDate = DateTime.UtcNow - maxAgeOfNewNewsPortal;

            return newsPortal =>
                newsPortalIds != null && !newsPortalIds.Contains(newsPortal.Id) &&
                (categoryIds == null || categoryIds.Contains(newsPortal.CategoryId)) &&
                (!newsPortal.CategoryId.Equals((int)Enums.CategoryEnums.CategoryId.Local)) &&
                (
                    newsPortal.IsNewOverride != null ?
                    newsPortal.IsNewOverride.Value :
                    newsPortal.CreatedAt > newNewsPortalMinDate
                );
        }
    }
}
