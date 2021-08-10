// GetLatestArticlesQueryValidator_1_4.cs
//
// © 2021 Espresso News. All rights reserved.

using FluentValidation;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_1_4
{
    public class GetLatestArticlesQueryValidator_1_4 : AbstractValidator<GetLatestArticlesQuery_1_4>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetLatestArticlesQueryValidator_1_4"/> class.
        /// </summary>
        public GetLatestArticlesQueryValidator_1_4()
        {
            RuleFor(query => query.Take).GreaterThan(0).LessThan(100);
            RuleFor(query => query.Skip).GreaterThanOrEqualTo(0);
            RuleForEach(query => query.CategoryIds).Must(categoryId => categoryId != default);
            RuleForEach(query => query.NewsPortalIds).Must(newsPortalId => newsPortalId != default);
        }
    }
}
