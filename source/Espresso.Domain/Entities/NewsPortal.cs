using System.Collections.Generic;

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

        public NewsPortal(int id, string name, string baseUrl, string iconUrl)
        {
            Id = id;
            Name = name;
            BaseUrl = baseUrl;
            IconUrl = iconUrl;
        }
        #endregion
    }
}
