using FluentValidation;

namespace Espresso.WebApi.Application.CQRS.NewsPortals.Queries.GetNewsPortals
{
    public class GetNewsPortalsQueryValidator : AbstractValidator<GetNewsPortalsQuery>
    {
        public GetNewsPortalsQueryValidator()
        {
        }
    }
}
