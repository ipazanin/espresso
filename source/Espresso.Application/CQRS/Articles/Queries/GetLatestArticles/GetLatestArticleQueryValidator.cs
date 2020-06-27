using FluentValidation;

namespace Espresso.Application.CQRS.Articles.Queries.GetLatestArticles
{
    public class GetLatestArticleQueryValidator : AbstractValidator<GetLatestArticlesQuery>
    {
        public GetLatestArticleQueryValidator()
        {
            RuleFor(query => query.Take).GreaterThan(0).LessThan(100);
            RuleFor(query => query.Skip).GreaterThanOrEqualTo(0);
            RuleForEach(query => query.CategoryIds).Must(categoryId => categoryId != default);
            RuleForEach(query => query.NewsPortalIds).Must(newsPortalId => newsPortalId != default);
        }
    }
}
