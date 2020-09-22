using System.Collections.Generic;

namespace Espresso.WebApi.Application.DataTransferObjects
{
    public class ArticlesRequestObjectDto
    {
        public IEnumerable<ArticleDto> CreatedArticles { get; set; } = new List<ArticleDto>();

        public IEnumerable<ArticleDto> UpdatedArticles { get; set; } = new List<ArticleDto>();
    }
}
