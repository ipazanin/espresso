using FluentValidation;

namespace Espresso.Application.CQRS.Articles.Queries.GetCategoryArticles
{
    public class GetcategoryArticlesQueryValidator : AbstractValidator<GetCategoryArticlesQuery>
    {
        public GetcategoryArticlesQueryValidator()
        {
            RuleFor(query => query.Take).GreaterThan(0).LessThan(100);
            RuleFor(query => query.Skip).GreaterThanOrEqualTo(0);
            RuleFor(query => query.CategoryId).NotEmpty();
            RuleForEach(query => query.NewsPortalIds).Must(newsPortalId => newsPortalId != 0);
        }
    }
}
