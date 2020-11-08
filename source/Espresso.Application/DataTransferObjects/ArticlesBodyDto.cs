using System;
using System.Collections.Generic;

namespace Espresso.Application.DataTransferObjects
{
    public record ArticlesBodyDto
    {
        public IEnumerable<Guid> CreatedArticles { get; init; } = new List<Guid>();

        public IEnumerable<Guid> UpdatedArticleIds { get; init; } = new List<Guid>();
    }
}