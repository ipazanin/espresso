// GetCategoryArticlesQueryValidator_1_3.cs
//
// © 2021 Espresso News. All rights reserved.

using FluentValidation;

namespace Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles_1_3
{
    public class GetcategoryArticlesQueryValidator_1_3 : AbstractValidator<GetCategoryArticlesQuery_1_3>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetcategoryArticlesQueryValidator_1_3"/> class.
        /// </summary>
        public GetcategoryArticlesQueryValidator_1_3()
        {
            RuleFor(query => query.Take).GreaterThan(0).LessThan(100);
            RuleFor(query => query.Skip).GreaterThanOrEqualTo(0);
            RuleFor(query => query.CategoryId).NotEmpty();
            RuleForEach(query => query.NewsPortalIds).Must(newsPortalId => newsPortalId != 0);
        }
    }
}
