using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Domain.Entities;
using Espresso.Domain.Records;

namespace Espresso.ParserDeleter.Application.IServices
{
    public interface ICreateArticleService
    {
        public Task<(Article? article, bool isValid)> CreateArticleAsync(
            RssFeedItem rssFeedItem,
            IEnumerable<Category> categories,
            TimeSpan maxAgeOfArticle,
            CancellationToken cancellationToken
        );
    }
}
