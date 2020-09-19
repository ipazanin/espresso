using System.Collections.Generic;
using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.Infrastructure;
using Espresso.Domain.ValueObjects.RssFeedValueObjects;

namespace Espresso.Domain.Entities
{
    public class RssFeed : IEntity<int, RssFeed>
    {
        #region Properties
        public int Id { get; private set; }

        public string Url { get; private set; }

        public RequestType RequestType { get; private set; }

        #region Value Objects
        public AmpConfiguration? AmpConfiguration { get; private set; }

        public CategoryParseConfiguration CategoryParseConfiguration { get; private set; }

        public ImageUrlParseConfiguration ImageUrlParseConfiguration { get; private set; }

        public SkipParseConfiguration? SkipParseConfiguration { get; private set; }

        public ICollection<RssFeedContentModifier> RssFeedContentModifiers { get; private set; } = new List<RssFeedContentModifier>();
        #endregion

        #region Relations
        public ICollection<RssFeedCategory> RssFeedCategories { get; private set; } = new List<RssFeedCategory>();

        public int NewsPortalId { get; private set; }

        public NewsPortal? NewsPortal { get; private set; }

        public int CategoryId { get; private set; }

        public Category? Category { get; private set; }

        public ICollection<Article> Articles { get; private set; } = new List<Article>();
        #endregion

        #endregion

        #region Constructors
        /// <summary>
        /// ORM Constructor
        /// </summary>
        private RssFeed()
        {
            Url = null!;
            CategoryParseConfiguration = null!;
            ImageUrlParseConfiguration = null!;
        }

        public RssFeed(
            int id,
            string url,
            int newsPortalId,
            int categoryId,
            RequestType requestType
        )
        {
            Id = id;
            Url = url;
            NewsPortalId = newsPortalId;
            CategoryId = categoryId;
            RequestType = requestType;
            CategoryParseConfiguration = null!;
            ImageUrlParseConfiguration = null!;
        }
        #endregion

        #region Methods
        public bool ShouldParse()
        {
            return SkipParseConfiguration?.ShouldParse() != false;
        }

        public string ModifyContent(string feedContent)
        {
            foreach (var rssFeedContentModifier in RssFeedContentModifiers)
            {
                feedContent = feedContent.Replace(rssFeedContentModifier.SourceValue, rssFeedContentModifier.ReplacementValue);
            }

            return feedContent;
        }
        #endregion
    }
}
