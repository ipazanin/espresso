using FluentValidation;

namespace Espresso.WebApi.Application.CQRS.Articles.Queries.GetTrendingArticles
{
    public class GetTrendingArticlesQueryValidator : AbstractValidator<GetTrendingArticlesQuery>
    {
        public GetTrendingArticlesQueryValidator()
        {
            _ = RuleFor(query => query.Take).GreaterThan(0).LessThan(100);
            _ = RuleFor(query => query.Skip).GreaterThanOrEqualTo(0);
        }
    }
}
