using FluentValidation;

namespace Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles_2_0
{
    public class GetcategoryArticlesQueryValidator_2_0 : AbstractValidator<GetCategoryArticlesQuery_2_0>
    {
        public GetcategoryArticlesQueryValidator_2_0()
        {
            RuleFor(query => query.Take).GreaterThan(0).LessThan(100);
            RuleFor(query => query.Skip).GreaterThanOrEqualTo(0);
            RuleFor(query => query.CategoryId).NotEmpty();
            RuleForEach(query => query.NewsPortalIds).Must(newsPortalId => newsPortalId != 0);
        }
    }
}
