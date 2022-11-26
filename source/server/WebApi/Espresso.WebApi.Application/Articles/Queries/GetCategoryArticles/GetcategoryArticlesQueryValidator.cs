// GetcategoryArticlesQueryValidator.cs
//
// © 2022 Espresso News. All rights reserved.

using FluentValidation;

namespace Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles;

public class GetcategoryArticlesQueryValidator : AbstractValidator<GetCategoryArticlesQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetcategoryArticlesQueryValidator"/> class.
    /// </summary>
    public GetcategoryArticlesQueryValidator()
    {
        RuleFor(query => query.Take).GreaterThan(0).LessThan(100);
        RuleFor(query => query.Skip).GreaterThanOrEqualTo(0);
        RuleFor(query => query.CategoryId).NotEmpty();
        RuleForEach(query => query.NewsPortalIds).Must(newsPortalId => newsPortalId != 0);
    }
}
