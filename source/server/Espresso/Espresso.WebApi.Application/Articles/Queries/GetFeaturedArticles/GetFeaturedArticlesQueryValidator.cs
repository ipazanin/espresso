using FluentValidation;

namespace Espresso.WebApi.Application.Articles.Queries.GetFeaturedArticles
{
    public class GetFeaturedArticlesQueryValidator : AbstractValidator<GetFeaturedArticlesQuery>
    {
        public GetFeaturedArticlesQueryValidator()
        {
        }
    }
}
