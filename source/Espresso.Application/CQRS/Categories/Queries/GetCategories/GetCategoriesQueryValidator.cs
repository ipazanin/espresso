using FluentValidation;

namespace Espresso.Application.CQRS.Categories.Queries.GetCategories
{
    public class GetCategoriesQueryValidator : AbstractValidator<GetCategoriesQuery>
    {
        public GetCategoriesQueryValidator()
        {
        }
    }
}
