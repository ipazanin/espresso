// GetNewsPortalsRssFeedCategory.cs
//
// © 2021 Espresso News. All rights reserved.

using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.Application.NewsPortals
{
    public class GetNewsPortalsRssFeedCategory
    {
        public int Id { get; private set; }

        public string UrlRegex { get; private set; } = string.Empty;

        public int UrlSegmentIndex { get; private set; }

        private GetNewsPortalsRssFeedCategory()
        {
        }

        public static Expression<Func<RssFeedCategory, GetNewsPortalsRssFeedCategory>> GetProjection()
        {
            return rssFeedCategory => new GetNewsPortalsRssFeedCategory
            {
                Id = rssFeedCategory.Id,
                UrlRegex = rssFeedCategory.UrlRegex,
                UrlSegmentIndex = rssFeedCategory.UrlSegmentIndex,
            };
        }
    }
}
