using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Domain.Entities;
using Espresso.Domain.Records;

namespace Espresso.Domain.IServices
{
    public interface ICreateArticleService
    {

        public Task<IEnumerable<Article>> CreateArticlesFromRssFeedItems(
            IEnumerable<RssFeedItem> rssFeedItems,
            IEnumerable<Category> categories,
            CancellationToken cancellationToken
        );
    }
}
