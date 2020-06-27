using FluentValidation;

namespace Espresso.Application.CQRS.NewsPortals.Queries.GetNewsPortals
{
    public class GetNewsPortalsQueryValidator : AbstractValidator<GetNewsPortalsQuery>
    {
        public GetNewsPortalsQueryValidator()
        {
        }
    }
}
