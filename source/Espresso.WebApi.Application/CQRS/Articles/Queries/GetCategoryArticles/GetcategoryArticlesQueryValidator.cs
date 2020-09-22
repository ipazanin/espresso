using FluentValidation;

namespace Espresso.WebApi.Application.CQRS.Articles.Queries.GetCategoryArticles
{
    public class GetcategoryArticlesQueryValidator : AbstractValidator<GetCategoryArticlesQuery>
    {
        public GetcategoryArticlesQueryValidator()
        {
            _ = RuleFor(query => query.Take).GreaterThan(0).LessThan(100);
            _ = RuleFor(query => query.Skip).GreaterThanOrEqualTo(0);
            _ = RuleFor(query => query.CategoryId).NotEmpty();
            _ = RuleForEach(query => query.NewsPortalIds).Must(newsPortalId => newsPortalId != 0);
        }
    }
}
