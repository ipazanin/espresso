using System;
using System.Collections.Generic;

namespace Espresso.Application.DataTransferObjects
{
    public record ArticlesBodyDto
    {
        public IEnumerable<Guid> CreatedArticleIds { get; init; } = new List<Guid>();

        public IEnumerable<Guid> UpdatedArticleIds { get; init; } = new List<Guid>();
    }
}