using FluentValidation;

namespace Espresso.WebApi.Application.CQRS.Articles.Queries.GetCategoryArticles_1_3
{
    public class GetcategoryArticlesQueryValidator_1_3 : AbstractValidator<GetCategoryArticlesQuery_1_3>
    {
        public GetcategoryArticlesQueryValidator_1_3()
        {
            _ = RuleFor(query => query.Take).GreaterThan(0).LessThan(100);
            _ = RuleFor(query => query.Skip).GreaterThanOrEqualTo(0);
            _ = RuleFor(query => query.CategoryId).NotEmpty();
            _ = RuleForEach(query => query.NewsPortalIds).Must(newsPortalId => newsPortalId != 0);
        }
    }
}
