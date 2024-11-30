// GetTrendingArticlesQueryValidator.cs
//
// © 2022 Espresso News. All rights reserved.

using FluentValidation;

namespace Espresso.WebApi.Application.Articles.Queries.GetTrendingArticles;

public class GetTrendingArticlesQueryValidator : AbstractValidator<GetTrendingArticlesQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetTrendingArticlesQueryValidator"/> class.
    /// </summary>
    public GetTrendingArticlesQueryValidator()
    {
        _ = RuleFor(query => query.Take).GreaterThan(0).LessThan(100);
        _ = RuleFor(query => query.Skip).GreaterThanOrEqualTo(0);
    }
}
