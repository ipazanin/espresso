// GetLatestArticlesQueryValidator.cs
//
// © 2021 Espresso News. All rights reserved.

using FluentValidation;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles
{
    public class GetLatestArticlesQueryValidator : AbstractValidator<GetLatestArticlesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetLatestArticlesQueryValidator"/> class.
        /// </summary>
        public GetLatestArticlesQueryValidator()
        {
            RuleFor(query => query.Take).GreaterThan(0).LessThan(100);
            RuleFor(query => query.Skip).GreaterThanOrEqualTo(0);
            RuleForEach(query => query.CategoryIds).Must(categoryId => categoryId != default);
            RuleForEach(query => query.NewsPortalIds).Must(newsPortalId => newsPortalId != default);
        }
    }
}
