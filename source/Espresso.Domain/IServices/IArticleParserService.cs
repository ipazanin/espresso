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
            string? summary,
            string? itemContent,
            DateTimeOffset publishDateTime,
            TimeSpan maxAgeOfArticle,
            CancellationToken cancellationToken
        );
    }
}
