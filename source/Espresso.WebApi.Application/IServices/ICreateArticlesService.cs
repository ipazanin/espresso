using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Espresso.Domain.Entities;
using Espresso.Domain.Records;

namespace Espresso.WebApi.Application.IServices
{
    public interface ICreateArticlesService
    {
        public Task<(Article? article, bool isValid)> CreateArticleAsync(
            RssFeedItem rssFeedItem,
            IEnumerable<Category> categories,
            TimeSpan maxAgeOfArticle,
            CancellationToken cancellationToken
        );
    }
}
