using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Espresso.Application.NewsPortals;
using Espresso.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Espresso.WebApi.Application.NewsPortals.Queries.GetNewsPortals
{
    public class GetNewsPortalsNewsPortal
    {
        #region Properties
        /// <summary>
        /// News Portal ID
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// News Portal Name
        /// </summary>
        public string Name { get; private set; } = "";

        public string IconUrl { get; private set; } = "";

        public GetNewsPortalsCategory Category { get; private set; } = null!;

        public GetNewsPortalsRegion Region { get; private set; } = null!;

        public IEnumerable<GetNewsPortalsRssFeed> RssFeeds { get; private set; } = new List<GetNewsPortalsRssFeed>();
        #endregion

        #region Constructors
        private GetNewsPortalsNewsPortal()
        {
        }
        #endregion

        #region Methods
        public static Expression<Func<NewsPortal, GetNewsPortalsNewsPortal>> GetProjection()
        {
            var categoryProjection = GetNewsPortalsCategory.GetProjection().Compile();
            var regionProjection = GetNewsPortalsRegion.GetProjection().Compile();
            var rssFeedProjection = GetNewsPortalsRssFeed.GetProjection();
            return newsPortal => new GetNewsPortalsNewsPortal
            {
                Id = newsPortal.Id,
                Name = newsPortal.Name,
                IconUrl = newsPortal.IconUrl,
                Category = categoryProjection.Invoke(newsPortal.Category!),
                RssFeeds = newsPortal
                    .RssFeeds
                    .AsQueryable()
                    .Select(rssFeedProjection),
                Region = regionProjection.Invoke(newsPortal.Region!)
            };
        }

        public static IQueryable<NewsPortal> Include(IQueryable<NewsPortal> newsPortals)
        {
            return newsPortals.Include(newsPortal => newsPortal.Region)
                .Include(newsPortal => newsPortal.Category)
                .Include(newsPortal => newsPortal.RssFeeds)
                .ThenInclude(rssFeed => rssFeed.RssFeedCategories)
                .Include(newsPortal => newsPortal.RssFeeds)
                .ThenInclude(rssFeed => rssFeed.RssFeedContentModifiers);
        }
        #endregion
    }
}
