using FluentValidation;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_1_4
{
    public class GetLatestArticlesQueryValidator_1_4 : AbstractValidator<GetLatestArticlesQuery_1_4>
    {
        public GetLatestArticlesQueryValidator_1_4()
        {
            RuleFor(query => query.Take).GreaterThan(0).LessThan(100);
            RuleFor(query => query.Skip).GreaterThanOrEqualTo(0);
            RuleForEach(query => query.CategoryIds).Must(categoryId => categoryId != default);
            RuleForEach(query => query.NewsPortalIds).Must(newsPortalId => newsPortalId != default);
        }
    }
}
