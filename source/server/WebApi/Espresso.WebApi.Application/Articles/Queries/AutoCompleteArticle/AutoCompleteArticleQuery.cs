// AutoCompleteArticleQuery.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.Infrastructure.MediatorInfrastructure;

namespace Espresso.WebApi.Application.Articles.Queries.AutoCompleteArticle;

public record AutoCompleteArticleQuery : Request<AutoCompleteArticleQueryResponse>
{
    public string? TitleSearchQuery { get; init; }

    public int Take { get; init; }

    public int Skip { get; init; }

    public string? KeyWordsToFilterOut { get; init; }
}
