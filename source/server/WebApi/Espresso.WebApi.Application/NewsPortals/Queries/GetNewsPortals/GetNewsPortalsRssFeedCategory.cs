// GetNewsPortalsRssFeedCategory.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using System.Linq.Expressions;

namespace Espresso.Application.NewsPortals
{
    public class GetNewsPortalsRssFeedCategory
    {
        public int Id { get; private set; }

        public string UrlRegex { get; private set; } = string.Empty;

        public int UrlSegmentIndex { get; private set; }

#pragma warning disable SA1201 // Elements should appear in the correct order
        private GetNewsPortalsRssFeedCategory()
#pragma warning restore SA1201 // Elements should appear in the correct order
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
