using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Espresso.Domain.Entities;

namespace Espresso.Domain.IServices
{
    public interface IArticleParserService
    {
        public Task<Article> CreateArticleAsync(
            RssFeed rssFeed,
            IEnumerable<Category> categories,
            string? itemId,
            IEnumerable<Uri?>? itemLinks,
            string? itemTitle,
            string? itemSummary,
            string? itemContent,
            DateTimeOffset itemPublishDateTime,
            TimeSpan maxAgeOfArticle,
            IEnumerable<string?>? elementExtensions,
            CancellationToken cancellationToken
        );
    }
}
