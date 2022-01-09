// AutoCompleteArticleQueryResponse.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.WebApi.Application.Articles.Queries.AutoCompleteArticle;

public record AutoCompleteArticleQueryResponse
{
    public IEnumerable<string> MatchedWords { get; init; } = new List<string>();
}
