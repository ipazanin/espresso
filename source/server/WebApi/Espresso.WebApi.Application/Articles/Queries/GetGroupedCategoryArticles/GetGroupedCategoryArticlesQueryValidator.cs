﻿// GetGroupedCategoryArticlesQueryValidator.cs
//
// © 2022 Espresso News. All rights reserved.

using FluentValidation;

namespace Espresso.WebApi.Application.Articles.Queries.GetGroupedCategoryArticles;

public class GetGroupedCategoryArticlesQueryValidator : AbstractValidator<GetGroupedCategoryArticlesQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetGroupedCategoryArticlesQueryValidator"/> class.
    /// </summary>
    public GetGroupedCategoryArticlesQueryValidator()
    {
        _ = RuleFor(query => query.Take).GreaterThan(0).LessThan(100);
        _ = RuleFor(query => query.Skip).GreaterThanOrEqualTo(0);
        _ = RuleFor(query => query.CategoryId).NotEmpty();
        _ = RuleForEach(query => query.NewsPortalIds).Must(newsPortalId => newsPortalId != 0);
    }
}
