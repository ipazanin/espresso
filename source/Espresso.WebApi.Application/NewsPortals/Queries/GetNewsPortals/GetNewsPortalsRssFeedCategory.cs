using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.Application.NewsPortals
{
    public class GetNewsPortalsRssFeedCategory
    {

        #region Properties
        public int Id { get; private set; }

        public string UrlRegex { get; private set; }

        public int UrlSegmentIndex { get; private set; }
        #endregion

        #region Constructors
        private GetNewsPortalsRssFeedCategory()
        {
            UrlRegex = null!;
        }
        #endregion

        #region Methods
        public static Expression<Func<RssFeedCategory, GetNewsPortalsRssFeedCategory>> GetProjection()
        {
            return rssFeedCategory => new GetNewsPortalsRssFeedCategory
            {
                Id = rssFeedCategory.Id,
                UrlRegex = rssFeedCategory.UrlRegex,
                UrlSegmentIndex = rssFeedCategory.UrlSegmentIndex,
            };
        }
        #endregion
    }
}