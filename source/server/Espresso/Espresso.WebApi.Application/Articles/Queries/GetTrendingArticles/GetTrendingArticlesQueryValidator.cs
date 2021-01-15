using FluentValidation;

namespace Espresso.WebApi.Application.Articles.Queries.GetTrendingArticles
{
    public class GetTrendingArticlesQueryValidator : AbstractValidator<GetTrendingArticlesQuery>
    {
        public GetTrendingArticlesQueryValidator()
        {
            RuleFor(query => query.Take).GreaterThan(0).LessThan(100);
            RuleFor(query => query.Skip).GreaterThanOrEqualTo(0);
        }
    }
}
