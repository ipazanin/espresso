﻿// GetLatestArticlesQueryValidator_1_4.cs
//
// © 2022 Espresso News. All rights reserved.

using FluentValidation;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_1_4;
#pragma warning disable S101 // Types should be named in PascalCase
public class GetLatestArticlesQueryValidator_1_4 : AbstractValidator<GetLatestArticlesQuery_1_4>
#pragma warning restore S101 // Types should be named in PascalCase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetLatestArticlesQueryValidator_1_4"/> class.
    /// </summary>
    public GetLatestArticlesQueryValidator_1_4()
    {
        _ = RuleFor(query => query.Take).GreaterThan(0).LessThan(100);
        _ = RuleFor(query => query.Skip).GreaterThanOrEqualTo(0);
        _ = RuleForEach(query => query.CategoryIds).Must(categoryId => categoryId != default);
        _ = RuleForEach(query => query.NewsPortalIds).Must(newsPortalId => newsPortalId != default);
    }
}
