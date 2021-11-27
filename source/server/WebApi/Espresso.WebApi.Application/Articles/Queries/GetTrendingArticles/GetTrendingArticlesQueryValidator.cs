// GetTrendingArticlesQueryValidator.cs
//
// © 2021 Espresso News. All rights reserved.

using FluentValidation;

namespace Espresso.WebApi.Application.Articles.Queries.GetTrendingArticles;

public class GetTrendingArticlesQueryValidator : AbstractValidator<GetTrendingArticlesQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetTrendingArticlesQueryValidator"/> class.
    /// </summary>
    public GetTrendingArticlesQueryValidator()
    {
        RuleFor(query => query.Take).GreaterThan(0).LessThan(100);
        RuleFor(query => query.Skip).GreaterThanOrEqualTo(0);
    }
}
