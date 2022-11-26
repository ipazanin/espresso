// GetFeaturedArticlesQueryResponse.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.WebApi.Application.Articles.Queries.GetFeaturedArticles;

public record GetFeaturedArticlesQueryResponse
{
    public IEnumerable<GetFeaturedArticlesArticle> Articles { get; init; } = new List<GetFeaturedArticlesArticle>();
}
