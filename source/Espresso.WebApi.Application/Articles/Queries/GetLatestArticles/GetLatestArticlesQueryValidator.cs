using FluentValidation;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles
{
    public class GetLatestArticlesQueryValidator : AbstractValidator<GetLatestArticlesQuery>
    {
        public GetLatestArticlesQueryValidator()
        {
            _ = RuleFor(query => query.Take).GreaterThan(0).LessThan(100);
            _ = RuleFor(query => query.Skip).GreaterThanOrEqualTo(0);
            _ = RuleForEach(query => query.CategoryIds).Must(categoryId => categoryId != default);
            _ = RuleForEach(query => query.NewsPortalIds).Must(newsPortalId => newsPortalId != default);
        }
    }
}
