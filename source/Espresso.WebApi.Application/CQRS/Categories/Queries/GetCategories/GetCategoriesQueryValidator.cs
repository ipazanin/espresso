using FluentValidation;

namespace Espresso.WebApi.Application.CQRS.Categories.Queries.GetCategories
{
    public class GetCategoriesQueryValidator : AbstractValidator<GetCategoriesQuery>
    {
        public GetCategoriesQueryValidator()
        {
        }
    }
}
