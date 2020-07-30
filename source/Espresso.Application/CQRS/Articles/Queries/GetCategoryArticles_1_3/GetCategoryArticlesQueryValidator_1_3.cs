﻿using FluentValidation;

namespace Espresso.Application.CQRS.Articles.Queries.GetCategoryArticles_1_3
{
    public class GetcategoryArticlesQueryValidator_1_3 : AbstractValidator<GetCategoryArticlesQuery_1_3>
    {
        public GetcategoryArticlesQueryValidator_1_3()
        {
            RuleFor(query => query.Take).GreaterThan(0).LessThan(100);
            RuleFor(query => query.Skip).GreaterThanOrEqualTo(0);
            RuleFor(query => query.CategoryId).NotEmpty();
            RuleForEach(query => query.NewsPortalIds).Must(newsPortalId => newsPortalId != 0);
        }
    }
}