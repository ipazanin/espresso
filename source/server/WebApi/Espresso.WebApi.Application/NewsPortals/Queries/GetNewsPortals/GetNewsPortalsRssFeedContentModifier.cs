// GetNewsPortalsRssFeedContentModifier.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using System.Linq.Expressions;

namespace Espresso.Application.NewsPortals
{
    public class GetNewsPortalsRssFeedContentModifier
    {
        public int Id { get; private set; }

        public string SourceValue { get; private set; } = string.Empty;

        public string ReplacementValue { get; private set; } = string.Empty;

#pragma warning disable SA1201 // Elements should appear in the correct order
        private GetNewsPortalsRssFeedContentModifier()
#pragma warning restore SA1201 // Elements should appear in the correct order
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
