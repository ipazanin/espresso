using Espresso.Domain.Infrastructure;

namespace Espresso.Domain.Entities
{
    public class RssFeedCategory : IEntity<int, RssFeedCategory>
    {
        #region Properties
        public int Id { get; private set; }

        public string UrlRegex { get; private set; }

        public int UrlSegmentIndex { get; private set; }

        #region Relations
        public int CategoryId { get; private set; }

        public Category? Category { get; private set; }

        public int RssFeedId { get; private set; }

        public RssFeed? RssFeed { get; private set; }
        #endregion

        #endregion

        #region Constructors
        /// <summary>
        /// ORM Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="urlRegex"></param>
        public RssFeedCategory()
        {
            UrlRegex = null!;
        }

        public RssFeedCategory(int id, string urlRegex, int urlSegmentIndex, int categoryId, int rssFeedId)
        {
            Id = id;
            UrlRegex = urlRegex;
            UrlSegmentIndex = urlSegmentIndex;
            RssFeedId = rssFeedId;
            CategoryId = categoryId;
        }
        #endregion
    }
}
