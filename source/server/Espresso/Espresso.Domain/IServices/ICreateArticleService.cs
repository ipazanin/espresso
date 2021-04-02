﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Domain.Entities;
using Espresso.Domain.Records;

namespace Espresso.Domain.IServices
{
    public interface ICreateArticleService
    {
        public Task<(Article? article, bool isValid)> CreateArticleAsync(
            RssFeedItem rssFeedItem,
            IEnumerable<Category> categories,
            CancellationToken cancellationToken
        );
    }
}