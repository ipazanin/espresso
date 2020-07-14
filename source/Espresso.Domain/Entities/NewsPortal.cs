using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Espresso.Common.Constants;
using Espresso.Domain.Infrastructure;

namespace Espresso.Domain.Entities
{
    public class NewsPortal : IEntity<int, NewsPortal>
    {
        #region Properties
        public int Id { get; private set; }

        public string Name { get; set; }

        public string BaseUrl { get; private set; }

        public string IconUrl { get; private set; }

        public bool? IsNewOverride { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public int CategoryId { get; private set; }

        public Category? Category { get; private set; }

        public ICollection<RssFeed> RssFeeds { get; private set; } = new List<RssFeed>();

        public ICollection<Article> Articles { get; private set; } = new List<Article>();
        #endregion

        #region Constructors
        /// <summary>
        /// ORM Constructor
        /// </summary>
        public NewsPortal()
        {
            Name = null!;
            BaseUrl = null!;
            IconUrl = null!;
        }

        public NewsPortal(
            int id,
            string name,
            string baseUrl,
            string iconUrl,
            bool? isNewOverride,
            DateTime createdAt,
            int categoryId
        )
        {
            Id = id;
            Name = name;
            BaseUrl = baseUrl;
            IconUrl = iconUrl;
            IsNewOverride = isNewOverride;
            CreatedAt = createdAt;
            CategoryId = categoryId;
        }
        #endregion

        #region Methods        
        public static Expression<Func<NewsPortal, bool>> GetIsNewExpression(
            IEnumerable<int>? newsPortalIds,
            IEnumerable<int>? categoryIds
        )
        {
            return newsPortal =>
                newsPortalIds != null && !newsPortalIds.Contains(newsPortal.Id) &&
                (categoryIds == null || categoryIds.Contains(newsPortal.CategoryId)) &&
                (newsPortal.IsNewOverride != null ?
                    newsPortal.IsNewOverride.Value :
                    newsPortal.CreatedAt > (DateTime.UtcNow - DateTimeConstants.MaxAgeOfNewNewsPortal)
                );
        }
        #endregion
    }
}
