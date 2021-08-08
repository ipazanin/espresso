// AutoCompleteArticleQueryResponse.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Collections.Generic;

namespace Espresso.WebApi.Application.Articles.AutoCompleteArticle
{
    public record AutoCompleteArticleQueryResponse
    {
        public IEnumerable<string> MatchedWords { get; init; } = new List<string>();
    }
}
