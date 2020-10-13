using System.Collections.Generic;

namespace Espresso.WebApi.Application.Articles.AutoCompleteArticle
{
    public record AutoCompleteArticleQueryResponse
    {
        public IEnumerable<string> ArticleTitles { get; init; } = new List<string>();
    }
}