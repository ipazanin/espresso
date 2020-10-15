using System.Collections.Generic;

namespace Espresso.WebApi.Application.Articles.AutoCompleteArticle
{
    public record AutoCompleteArticleQueryResponse
    {
        public IEnumerable<AutoCompleteArticleResult> AutoCompleteArticleResults { get; init; }
            = new List<AutoCompleteArticleResult>();
    }
}