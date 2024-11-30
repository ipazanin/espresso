// AutoCompleteArticleQueryResponse.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.WebApi.Application.Articles.Queries.AutoCompleteArticle;

public record AutoCompleteArticleQueryResponse
{
    public IEnumerable<string> MatchedWords { get; init; } = [];
}
