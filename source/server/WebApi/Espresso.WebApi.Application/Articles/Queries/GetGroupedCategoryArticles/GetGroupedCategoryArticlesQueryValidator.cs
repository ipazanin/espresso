// GetGroupedCategoryArticlesQueryValidator.cs
//
// © 2021 Espresso News. All rights reserved.

using FluentValidation;

namespace Espresso.WebApi.Application.Articles.Queries.GetGroupedCategoryArticles;

public class GetGroupedCategoryArticlesQueryValidator : AbstractValidator<GetGroupedCategoryArticlesQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetGroupedCategoryArticlesQueryValidator"/> class.
    /// </summary>
    public GetGroupedCategoryArticlesQueryValidator()
    {
        RuleFor(query => query.Take).GreaterThan(0).LessThan(100);
        RuleFor(query => query.Skip).GreaterThanOrEqualTo(0);
        RuleFor(query => query.CategoryId).NotEmpty();
        RuleForEach(query => query.NewsPortalIds).Must(newsPortalId => newsPortalId != 0);
    }
}
