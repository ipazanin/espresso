using System.Collections.Generic;

namespace Espresso.WebApi.Application.Articles.AutoCompleteArticle
{
    public record AutoCompleteArticleQueryResponse
    {
        public IEnumerable<AutoCompleteArticleArticle> AutoCompleteArticles { get; init; }
            = new List<AutoCompleteArticleArticle>();
    }
}