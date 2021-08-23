// GetNewsPortalsRssFeedContentModifier.cs
//
// © 2021 Espresso News. All rights reserved.

using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.Application.NewsPortals
{
    public class GetNewsPortalsRssFeedContentModifier
    {
        public int Id { get; private set; }

        public string SourceValue { get; private set; } = string.Empty;

        public string ReplacementValue { get; private set; } = string.Empty;

        private GetNewsPortalsRssFeedContentModifier()
        {
        }

        public static Expression<Func<RssFeedContentModifier, GetNewsPortalsRssFeedContentModifier>> GetProjection()
        {
            return rssFeedCategory => new GetNewsPortalsRssFeedContentModifier
            {
                Id = rssFeedCategory.Id,
                SourceValue = rssFeedCategory.SourceValue,
                ReplacementValue = rssFeedCategory.ReplacementValue,
            };
        }
    }
}
