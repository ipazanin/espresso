using FluentValidation;

namespace Espresso.WebApi.Application.CQRS.Articles.Queries.GetLatestArticles
{
    public class GetLatestArticleQueryValidator : AbstractValidator<GetLatestArticlesQuery>
    {
        public GetLatestArticleQueryValidator()
        {
            _ = RuleFor(query => query.Take).GreaterThan(0).LessThan(100);
            _ = RuleFor(query => query.Skip).GreaterThanOrEqualTo(0);
            _ = RuleForEach(query => query.CategoryIds).Must(categoryId => categoryId != default);
            _ = RuleForEach(query => query.NewsPortalIds).Must(newsPortalId => newsPortalId != default);
        }
    }
}
