// GetLatestArticlesQueryValidator_1_3.cs
//
// © 2022 Espresso News. All rights reserved.

using FluentValidation;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_1_3;
#pragma warning disable S101 // Types should be named in PascalCase
public class GetLatestArticlesQueryValidator_1_3 : AbstractValidator<GetLatestArticlesQuery_1_3>
#pragma warning restore S101 // Types should be named in PascalCase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetLatestArticlesQueryValidator_1_3"/> class.
    /// </summary>
    public GetLatestArticlesQueryValidator_1_3()
    {
        RuleFor(query => query.Take).GreaterThan(0).LessThan(100);
        RuleFor(query => query.Skip).GreaterThanOrEqualTo(0);
        RuleForEach(query => query.CategoryIds).Must(categoryId => categoryId != default);
        RuleForEach(query => query.NewsPortalIds).Must(newsPortalId => newsPortalId != default);
    }
}
