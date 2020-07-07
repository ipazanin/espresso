using FluentValidation;

namespace Espresso.Application.CQRS.NewsPortals.Queries.GetNewNewsportals
{
    public class GetNewNewsPortalsQueryValidator : AbstractValidator<GetNewNewsportalsQuery>
    {
        public GetNewNewsPortalsQueryValidator()
        {
        }
    }
}
