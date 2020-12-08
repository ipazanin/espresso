using FluentValidation;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_2_0
{
    public class GetLatestArticlesQueryValidator_2_0 : AbstractValidator<GetLatestArticlesQuery_2_0>
    {
        public GetLatestArticlesQueryValidator_2_0()
        {
            _ = RuleFor(query => query.Take).GreaterThan(0).LessThan(100);
            _ = RuleFor(query => query.Skip).GreaterThanOrEqualTo(0);
            _ = RuleForEach(query => query.CategoryIds).Must(categoryId => categoryId != default);
            _ = RuleForEach(query => query.NewsPortalIds).Must(newsPortalId => newsPortalId != default);
        }
    }
}
