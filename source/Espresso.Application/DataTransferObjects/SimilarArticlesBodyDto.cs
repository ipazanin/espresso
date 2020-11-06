using System.Collections;
using System.Collections.Generic;

namespace Espresso.Application.DataTransferObjects
{
    public record SimilarArticlesBodyDto
    {
        public IEnumerable<SimilarArticleDto> SimilarArticles { get; init; } = new List<SimilarArticleDto>();
    }
}