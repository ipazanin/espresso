// GetLatestArticlesQueryValidator_2_0.cs
//
// © 2021 Espresso News. All rights reserved.

using FluentValidation;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_2_0;
#pragma warning disable S101 // Types should be named in PascalCase
public class GetLatestArticlesQueryValidator_2_0 : AbstractValidator<GetLatestArticlesQuery_2_0>
#pragma warning restore S101 // Types should be named in PascalCase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetLatestArticlesQueryValidator_2_0"/> class.
    /// </summary>
    public GetLatestArticlesQueryValidator_2_0()
    {
        RuleFor(query => query.Take).GreaterThan(0).LessThan(100);
        RuleFor(query => query.Skip).GreaterThanOrEqualTo(0);
        RuleForEach(query => query.CategoryIds).Must(categoryId => categoryId != default);
        RuleForEach(query => query.NewsPortalIds).Must(newsPortalId => newsPortalId != default);
    }
}
