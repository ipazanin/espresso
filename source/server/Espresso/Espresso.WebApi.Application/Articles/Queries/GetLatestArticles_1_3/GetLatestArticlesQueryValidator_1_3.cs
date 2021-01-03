using FluentValidation;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_1_3
{
    public class GetLatestArticlesQueryValidator_1_3 : AbstractValidator<GetLatestArticlesQuery_1_3>
    {
        public GetLatestArticlesQueryValidator_1_3()
        {
            _ = RuleFor(query => query.Take).GreaterThan(0).LessThan(100);
            _ = RuleFor(query => query.Skip).GreaterThanOrEqualTo(0);
            _ = RuleForEach(query => query.CategoryIds).Must(categoryId => categoryId != default);
            _ = RuleForEach(query => query.NewsPortalIds).Must(newsPortalId => newsPortalId != default);
        }
    }
}
