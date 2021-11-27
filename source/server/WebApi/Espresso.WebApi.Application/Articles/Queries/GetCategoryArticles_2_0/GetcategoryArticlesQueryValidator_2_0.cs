// GetcategoryArticlesQueryValidator_2_0.cs
//
// © 2021 Espresso News. All rights reserved.

using FluentValidation;

namespace Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles_2_0;
#pragma warning disable S101 // Types should be named in PascalCase
public class GetcategoryArticlesQueryValidator_2_0 : AbstractValidator<GetCategoryArticlesQuery_2_0>
#pragma warning restore S101 // Types should be named in PascalCase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetcategoryArticlesQueryValidator_2_0"/> class.
    /// </summary>
    public GetcategoryArticlesQueryValidator_2_0()
    {
        RuleFor(query => query.Take).GreaterThan(0).LessThan(100);
        RuleFor(query => query.Skip).GreaterThanOrEqualTo(0);
        RuleFor(query => query.CategoryId).NotEmpty();
        RuleForEach(query => query.NewsPortalIds).Must(newsPortalId => newsPortalId != 0);
    }
}
