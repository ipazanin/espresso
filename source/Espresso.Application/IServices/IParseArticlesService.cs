using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Espresso.Domain.Entities;
using Espresso.Domain.Records;

namespace Espresso.Application.IServices
{
    public interface IParseArticlesService
    {
        public Task<Article> CreateArticleAsync(
            RssFeedItem rssFeedItem,
            IEnumerable<Category> categories,
            TimeSpan maxAgeOfArticle,
            CancellationToken cancellationToken
        );
    }
}
