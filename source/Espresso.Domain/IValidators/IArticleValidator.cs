using System;
using System.Collections.Generic;

using Espresso.Domain.Entities;

namespace Espresso.Domain.IValidators
{
    public interface IArticleValidator
    {
        public Article Validate(
            Guid id,
            string? articleId,
            string? url,
            string? summary,
            string? title,
            string? imageUrl,
            DateTime createDateTime,
            DateTime updateDateTime,
            DateTime? publishDateTime,
            int numberOfClicks,
            decimal trendingScore,
            int newsPortalId,
            int rssFeedId,
            IEnumerable<ArticleCategory> articleCategories,
            NewsPortal? newsPortal,
            RssFeed rssFeed
        );
    }
}
